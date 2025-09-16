using System;
using System.Collections.Generic;
using System.Linq;
using ApiVela.Data;
using ApiVela.Models;
using AutoMapper;

namespace ApiVela.Repositories
{
    public class RepositoryVelaPigmento
    {
        private readonly Contexto context;
        private readonly IMapper mapper;

        public RepositoryVelaPigmento(Contexto context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // Insertar relación Vela-Pigmento
        public CustomApiResponse<Pigmento> InsertarVelaPigmento(Guid idVela, Guid idPig)
        {
            var response = new CustomApiResponse<Pigmento>();

            try
            {
                var vp = new VelaPigmento { IDVela = idVela, IDPig = idPig };
                context.VelaPigmento.Add(vp);
                context.SaveChanges();

                response.Object = mapper.Map<Pigmento>(vp);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        // Eliminar relaciones de pigmentos por Vela
        public CustomApiResponse<bool> EliminarRelacionesPigmentos(Guid idVela)
        {
            var response = new CustomApiResponse<bool>();

            try
            {
                var rels = context.VelaPigmento.Where(vp => vp.IDVela == idVela).ToList();
                context.VelaPigmento.RemoveRange(rels);
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

        // Obtener pigmentos por Vela con DTO genérico
        public CustomApiResponse<List<Pigmento>> GetPigmentosPorVela(Guid idVela) 
        {
            var response = new CustomApiResponse<List<Pigmento>>();

            try
            {
                var pigmentos = context.VelaPigmento
                    .Where(vp => vp.IDVela == idVela)
                    .Select(vp => vp.Pigmento)
                    .ToList();

                response.Object = mapper.Map<List<Pigmento>>(pigmentos);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }
    }
}
