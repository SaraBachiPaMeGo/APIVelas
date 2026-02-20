using ApiVela.Data;
using ApiVela.Models;
using ApiVela.Models.DTO;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVela.Repositories
{
    public class RepositoryVelaFragancia
    {
        private readonly Contexto context;
        private readonly IMapper mapper;

        public RepositoryVelaFragancia(Contexto context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CustomApiResponse<VelaFragancia>> BuscarVelaFragancia(Guid idVela)
        {
            var response = new CustomApiResponse<VelaFragancia>();

            try
            {
                var vela = await context.VelaFragancia
                    .FirstOrDefaultAsync(x => x.IDVela == idVela);

                if (vela == null)
                    throw new Exception("VelaFragancia no encontrada");

                response.Object = vela;
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }


        // ---------------------------- INSERTAR RELACIÓN ----------------------------
        public async Task<CustomApiResponse<VelaFragancia>> InsertarVelaFragancia(Guid idVela, Guid idFrag) 
            {
                var response = new CustomApiResponse<VelaFragancia>();

                try
                {
                    var vf = new VelaFragancia { IDVela = idVela, IDFrag = idFrag };
                    context.VelaFragancia.Add(vf);
                    await context.SaveChangesAsync();

                    // Mapear el objeto agregado, si quieres devolver la entidad de unión
                    response.Object = mapper.Map<VelaFragancia>(vf);
                }
                catch (Exception ex)
                {
                    response.Error = new ErrorViewModel { Mensaje = ex.Message };
                }

                return response;
            }

        //    // ---------------------------- ELIMINAR RELACIONES EXISTENTES ----------------------------
        public async Task<CustomApiResponse<bool>> EliminarRelacionesFragancias(Guid idVela)
        {
            var response = new CustomApiResponse<bool>();

            try
            {
                var rels = context.VelaFragancia.Where(vf => vf.IDVela == idVela).ToList();
                context.VelaFragancia.RemoveRange(rels);
                await context.SaveChangesAsync();

                response.Object = true;
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
                response.Object = false;
            }

            return response;
        }

        public async Task<CustomApiResponse<VelaFragancia>> ActualizarVelaFragancia(Guid idVela,
             List<VelaFragancia> nuevasFragancias)
        {
            var response = new CustomApiResponse<VelaFragancia>();

            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                // 1️⃣ Eliminar relaciones existentes
                var relacionesActuales = await context.VelaFragancia
                    .Where(vf => vf.IDVela == idVela)
                    .ToListAsync();

                context.VelaFragancia.RemoveRange(relacionesActuales);

                // 2️⃣ Insertar nuevas relaciones
                var nuevasRelaciones = nuevasFragancias.Select(f => new VelaFragancia
                {
                    IDVela = idVela,
                    IDFrag = f.IDFrag,
                    Cantidad = f.Cantidad,
                    Coste = f.Coste
                }).ToList();

               // response = await context.VelaFragancia.AddRangeAsync(nuevasRelaciones);

                // 3️⃣ Guardar cambios
                await context.SaveChangesAsync();

                await transaction.CommitAsync();

                //response.Object = mapper.Map<VelaFragancia>(fragan);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }


            return response;
        }

        //    // ---------------------------- OBTENER FRAGANCIAS POR VELA ----------------------------
        public async Task<CustomApiResponse<List<Fragancia>>> GetFraganciasPorVela(Guid idVela)
            {
                var response = new CustomApiResponse<List<Fragancia>>();

                try
                {
                    var fragancias = await context.VelaFragancia
                        .Where(vf => vf.IDVela == idVela)
                        .Select(vf => vf.Fragancia)
                        .ToListAsync();

                    response.Object = fragancias;
                }
                catch (Exception ex)
                {
                    response.Error = new ErrorViewModel { Mensaje = ex.Message };
                }

                return response;
            }

    }
}
