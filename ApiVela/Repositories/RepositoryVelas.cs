using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ApiVela.Data;
using ApiVela.Models;
using ApiVela.Models.DTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiVela.Repository
{
    public class RepositoryVelas
    {
        private readonly Contexto context;
        private readonly IMapper mapper;
        private readonly RepositoryCeras _repocera;
        private readonly RepositoryFragancias _repovf;
        private readonly RepositoryPigmentos _repovp;

        public RepositoryVelas(Contexto context, IMapper mapper, RepositoryCeras repocera,
            RepositoryFragancias repovf, RepositoryPigmentos repovp)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._repocera = repocera ?? throw new ArgumentNullException(nameof(_repocera));
            this._repovf = repovf ?? throw new ArgumentNullException(nameof(_repovf));
            this._repovp = repovp ?? throw new ArgumentNullException(nameof(_repovp));
        }

        public async Task<CustomApiResponse<List<VelaDTO>>> GetVelas()
        {
            var response = new CustomApiResponse<List<VelaDTO>>();

            try
            {
                var velas = await context.Vela
                        .Include(v => v.Cera)
                    .Include(v => v.VelaPigmentos)
                        .ThenInclude(vp => vp.Pigmento)
                    .Include(v => v.VelaFragancias)
                        .ThenInclude(vf => vf.Fragancia)    
                    .Select(v => new VelaDTO
                    {
                        IDVela = v.IDVela,
                        VelaNombre = v.VelaNombre,
                        Image = v.Image,
                        FechaReal = v.FechaReal,
                        Coste = v.Coste,
                        NombreCera = v.Cera.Firma,
                        CantidadCera = v.CantidadCera,
                        CantidadEnd = v.CantidadEnd,
                        VelaFragancias = v.VelaFragancias.Select(vf => new VelaFraganciaDTO
                        {
                            IDFrag = vf.IDFrag,
                            NombreFragancia = vf.Fragancia != null ? vf.Fragancia.FragNombre : null,
                            Cantidad = vf.Cantidad,
                            Coste = vf.Coste
                        }).ToList(),

                        // 🔥 PIGMENTOS
                        VelaPigmentos = v.VelaPigmentos.Select(vp => new VelaPigmentoDTO
                        {
                            IDPig = vp.IDPig,
                            NombrePigmento = vp.Pigmento != null ? vp.Pigmento.ColorNombre : null,
                            Cantidad = vp.Cantidad,
                            Coste = vp.Coste
                        }).ToList()
                    }).ToListAsync();

                response.Object = velas;
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        public async Task<CustomApiResponse<VelaDTO>> BuscarVela(Guid idVela)
        {
            var response = new CustomApiResponse<VelaDTO>();
            try
            {
                var vela = await context.Vela
                .Where(x => x.IDVela == idVela)
                .ProjectTo<VelaDTO>(mapper.ConfigurationProvider)
                .AsNoTracking()
                .FirstOrDefaultAsync();

                if (vela == null) throw new Exception("Vela no encontrada");
                response.Object = vela;
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public async Task<CustomApiResponse<VelaDTO>> InsertarVela(Vela vel)
        {
            var response = new CustomApiResponse<VelaDTO>();
            try
            {
                vel.IDVela = Guid.NewGuid();
                vel.FechaReal = DateTime.Now;

                if (vel.VelaFragancias != null)
                {
                    foreach (var vf in vel.VelaFragancias)
                    {
                        vf.IDVela = vel.IDVela;

                    }
                }

                if (vel.VelaPigmentos != null)
                {
                    foreach (var vp in vel.VelaPigmentos)
                    {
                        vp.IDVela = vel.IDVela;
                    }
                }

                context.Vela.Add(vel);
                await context.SaveChangesAsync();

                var costeTotal = await CalcularCosteVelaAsync(vel.IDVela);

                var velaCompleta = await context.Vela
                   .Include(v => v.VelaFragancias)
                       .ThenInclude(vf => vf.Fragancia)
                   .Include(v => v.VelaPigmentos)
                       .ThenInclude(vp => vp.Pigmento)
                   .FirstOrDefaultAsync(v => v.IDVela == vel.IDVela);

                response.Object = mapper.Map<VelaDTO>(velaCompleta);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public async Task<CustomApiResponse<VelaDTO>> ActualizarVela(Vela vel)
        {
            var response = new CustomApiResponse<VelaDTO>();

            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                var existing = await context.Vela
                    .Include(v => v.VelaPigmentos)
                    .Include(v => v.VelaFragancias)
                    .FirstOrDefaultAsync(v => v.IDVela == vel.IDVela);

                if (existing == null)
                    throw new Exception("La vela no existe");

                // 🔹 Actualizar propiedades simples dinámicamente
                context.Entry(existing).CurrentValues.SetValues(vel);

                // 🔥 ---------- PIGMENTOS ----------
                ActualizarRelacion(
                    existing.VelaPigmentos,
                    vel.VelaPigmentos,
                    p => p.IDPig,
                    (dest, src) =>
                    {
                        dest.Cantidad = src.Cantidad;
                        dest.Coste = src.Coste;
                    },
                    (nuevo) => nuevo.IDVela = existing.IDVela
                );

                // 🔥 ---------- FRAGANCIAS ----------
                ActualizarRelacion(
                    existing.VelaFragancias,
                    vel.VelaFragancias,
                    f => f.IDFrag,
                    (dest, src) =>
                    {
                        dest.Cantidad = src.Cantidad;
                        dest.Coste = src.Coste;
                    },
                    (nuevo) => nuevo.IDVela = existing.IDVela
                );
                
                await CalcularCosteVelaAsync(vel.IDVela);

                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                response.Object = mapper.Map<VelaDTO>(existing);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        private void ActualizarRelacion<T, TKey>(ICollection<T> actuales, ICollection<T> nuevas,
        Func<T, TKey> keySelector, Action<T, T> actualizarCampos, Action<T> prepararNuevo) where T : class
        {
            nuevas ??= new List<T>();

            // Eliminar los que ya no existen
            var eliminar = actuales
                .Where(a => !nuevas.Any(n => keySelector(n).Equals(keySelector(a))))
                .ToList();

            foreach (var item in eliminar)
                actuales.Remove(item);

            // Agregar o actualizar
            foreach (var nuevo in nuevas)
            {
                var existente = actuales
                    .FirstOrDefault(a => keySelector(a).Equals(keySelector(nuevo)));

                if (existente != null)
                {
                    actualizarCampos(existente, nuevo);
                }
                else
                {
                    prepararNuevo(nuevo);
                    actuales.Add(nuevo);
                }
            }
        }


        public async Task<CustomApiResponse<bool>> EliminarVela(Guid idVela)
        {
            var response = new CustomApiResponse<bool>();

            try
            {
                var vela = await context.Vela
                    .Include(v => v.VelaPigmentos)
                    .Include(v => v.VelaFragancias)
                    .FirstOrDefaultAsync(v => v.IDVela == idVela);

                if (vela == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "Vela no encontrado" };
                    response.Object = false;
                }
                else
                {
                    context.Set<Vela>().Remove(vela);
                    await context.SaveChangesAsync();
                    response.Object = true;

                }
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        public async Task<CustomApiResponse<decimal>> CalcularCosteVelaAsync(Guid idVela)
        {
            var costeTotal = new CustomApiResponse<decimal>();

            try
            {
                using (var connection = new SqlConnection("Data Source=LAPTOP-SLC643FH;Initial Catalog=ProyektVelas;Persist Security Info=True;User ID=SA;Password=P@ssw0rdVelas1"))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("sp_CalcularCoste_Vela", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // 🔹 INPUT
                        command.Parameters.Add(new SqlParameter("@IDVela", SqlDbType.UniqueIdentifier)
                        {
                            Value = idVela
                        });

                        // 🔹 OUTPUT
                        var outputParam = new SqlParameter
                        {
                            ParameterName = "@CosteOut",
                            SqlDbType = SqlDbType.Decimal,
                            Precision = 10,
                            Scale = 2,
                            Direction = ParameterDirection.Output,
                            Value = DBNull.Value
                        };

                        command.Parameters.Add(outputParam);

                        try
                        {

                            // 🔥 EJECUTAR PRIMERO
                            var sp = await command.ExecuteNonQueryAsync();
                        }
                        catch (Exception ex)
                        {
                            costeTotal.Error = new ErrorViewModel { Mensaje = ex.Message };
                            return costeTotal;
                        }
                        Console.WriteLine($"OUTPUT RAW: {outputParam.Value}");

                        // 🔥 LUEGO LEER OUTPUT
                        if (outputParam.Value != DBNull.Value)
                        {
                            costeTotal.Object = Convert.ToDecimal(outputParam.Value);
                        }
                        else
                        {
                            costeTotal.Object = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                costeTotal.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return costeTotal;
        }
    }
}
