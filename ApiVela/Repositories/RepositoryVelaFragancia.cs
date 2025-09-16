using System;
using System.Collections.Generic;
using System.Linq;
using ApiVela.Data;
using ApiVela.Models;
using AutoMapper;

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

        // ---------------------------- INSERTAR RELACIÓN ----------------------------
        public CustomApiResponse<VelaFragancia> InsertarVelaFragancia(Guid idVela, Guid idFrag) 
        {
            var response = new CustomApiResponse<VelaFragancia>();

            try
            {
                var vf = new VelaFragancia { IDVela = idVela, IDFrag = idFrag };
                context.VelaFragancia.Add(vf);
                context.SaveChanges();

                // Mapear el objeto agregado, si quieres devolver la entidad de unión
                response.Object = mapper.Map<VelaFragancia>(vf);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        // ---------------------------- ELIMINAR RELACIONES EXISTENTES ----------------------------
        public CustomApiResponse<bool> EliminarRelacionesFragancias(Guid idVela)
        {
            var response = new CustomApiResponse<bool>();

            try
            {
                var rels = context.VelaFragancia.Where(vf => vf.IDVela == idVela).ToList();
                context.VelaFragancia.RemoveRange(rels);
                context.SaveChanges();

                response.Object = true;
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
                response.Object = false;
            }

            return response;
        }

        // ---------------------------- OBTENER FRAGANCIAS POR VELA ----------------------------
        public CustomApiResponse<List<Fragancia>> GetFraganciasPorVela(Guid idVela)
        {
            var response = new CustomApiResponse<List<Fragancia>>();

            try
            {
                var fragancias = context.VelaFragancia
                    .Where(vf => vf.IDVela == idVela)
                    .Select(vf => vf.Fragancia)
                    .ToList();

                response.Object = mapper.Map<List<Fragancia>>(fragancias);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }
    }
}
