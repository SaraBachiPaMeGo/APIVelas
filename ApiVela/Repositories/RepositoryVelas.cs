using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVela.Data;
using ApiVela.Models;
using ApiVela.Models.DTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace ApiVela.Repository
{
    public class RepositoryVelas
    {
        private readonly Contexto context;
        private readonly IMapper mapper;
        private readonly RepositoryCeras _repocera;

        public RepositoryVelas(Contexto context, IMapper mapper, RepositoryCeras repocera)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._repocera = repocera ?? throw new ArgumentNullException(nameof(_repocera));
        }

        public async Task<CustomApiResponse<List<VelaDTO>>> GetVelas1()
        {
            var response = new CustomApiResponse<List<VelaDTO>>();
            try
            {
                var velas = context.Vela.ToList();
                response.Object = mapper.Map<List<VelaDTO>>(velas);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public async Task<CustomApiResponse<List<VelaDTO>>> GetVelas()
        {
            var response = new CustomApiResponse<List<VelaDTO>>();

            try
            {
                var velas = context.Vela
                    .Select(v => new VelaDTO
                    {
                        IDVela = v.IDVela,
                        VelaNombre = v.VelaNombre,
                        Image = v.Image,
                        FechaReal = v.FechaReal,
                        Coste = v.Coste,
                        NombreCera = "_repocera.BuscarCera(v.IDCera).Result.Object.Firma",
                        CantidadCera = v.CantidadCera,
                        CantidadEnd = v.CantidadEnd,

                        VelaPigmentos = v.VelaPigmentos
                            .Select(vp => new VelaPigmentoDTO
                            {
                                IDPig = vp.IDPig,
                                NombrePigmento = vp.Pigmento.ColorNombre, // 👈 NOMBRE
                                Cantidad = vp.Cantidad,
                                Coste = vp.Coste
                            }).ToList(),

                        VelaFragancias = v.VelaFragancias
                            .Select(vf => new VelaFraganciaDTO
                            {
                                IDFrag = vf.IDFrag,
                                NombreFragancia = vf.Fragancia.FragNombre, // 👈 NOMBRE
                        Cantidad = vf.Cantidad,
                                Coste = vf.Coste
                            }).ToList()
                    })
                    .ToList();

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
                .FirstOrDefaultAsync();

                if (vela == null) throw new Exception("Vela no encontrada");
                response.Object = mapper.Map<VelaDTO>(vela);
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


                        // ✔ Solo asigna la FK
                        vf.Fragancia = null;
                    }
                }

                if (vel.VelaPigmentos != null)
                {
                    foreach (var vp in vel.VelaPigmentos)
                    {
                        vp.IDVela = vel.IDVela;
                        vp.Pigmento = null;
                    }
                }

                context.Vela.Add(vel);
                await context.SaveChangesAsync();

                response.Object = mapper.Map<VelaDTO>(vel);
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
    }
}
