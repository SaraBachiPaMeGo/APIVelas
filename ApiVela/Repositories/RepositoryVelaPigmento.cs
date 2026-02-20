using System;
using System.Collections.Generic;
using System.Linq;
using ApiVela.Data;
using ApiVela.Models;
using AutoMapper;
using System.Threading.Tasks;


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

        public async Task<CustomApiResponse<VelaPigmento>> BuscarVelaPigmento(Guid idVela)
        {
            var response = new CustomApiResponse<VelaPigmento>();
            try
            {
                var vela = context.VelaPigmento.FirstOrDefault(x => x.IDVela == idVela);
                if (vela == null) throw new Exception("VelaPigmento no encontrada");
                response.Object = mapper.Map<VelaPigmento>(vela);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        //// Insertar relación Vela-Pigmento
        public async Task<CustomApiResponse<Pigmento>> InsertarVelaPigmento(Guid idVela, Guid idPig)
        {
            var response = new CustomApiResponse<Pigmento>();

            try
            {
                var vp = new VelaPigmento { IDVela = idVela, IDPig = idPig };
                context.VelaPigmento.Add(vp);
                await context.SaveChangesAsync();

                response.Object = mapper.Map<Pigmento>(vp);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        //// Eliminar relaciones de pigmentos por Vela
        public async Task<CustomApiResponse<bool>> EliminarRelacionesPigmentos(Guid idVela)
        {
            var response = new CustomApiResponse<bool>();

            try
            {
                var rels = context.VelaPigmento.Where(vp => vp.IDVela == idVela).ToList();
                context.VelaPigmento.RemoveRange(rels);
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
        public async Task<CustomApiResponse<VelaPigmento>> ActualizarVelaPigmento(VelaPigmento fragan)
        {
            var response = new CustomApiResponse<VelaPigmento>();

            try
            {
                var frag = context.VelaPigmento.SingleOrDefault(x => x.IDPig == fragan.IDPig);
                if (frag == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "VelaPigmento no encontrada" };
                    return response;
                }

                frag.IDVela = fragan.IDVela != Guid.Empty ? fragan.IDVela : frag.IDVela;
                frag.Coste = fragan.Coste;
                frag.Cantidad = fragan.Cantidad;

                await context.SaveChangesAsync();
                response.Object = mapper.Map<VelaPigmento>(frag);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }
        //// Obtener pigmentos por Vela con DTO genérico
        public async Task<CustomApiResponse<List<Pigmento>>> GetPigmentosPorVela(Guid idVela) 
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
