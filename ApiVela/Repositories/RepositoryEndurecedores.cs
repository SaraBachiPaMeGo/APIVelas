using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVela.Data;
using ApiVela.Models;
using AutoMapper;

namespace ApiVela.Repository
{
    public class RepositoryEndurecedores
    {
        private readonly Contexto context;
        private readonly IMapper mapper;

        public RepositoryEndurecedores(Contexto context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // Insertar Endurecedor
        public async Task<CustomApiResponse<Endurecedor>> InsertarEndurecedor(Endurecedor end) 
        {
            var response = new CustomApiResponse<Endurecedor>();

            try
            {
                var enda = new Endurecedor
                {
                    IDEndurecedor = Guid.NewGuid(),
                    Tipo = end.Tipo,
                    CompradoEn = end.CompradoEn,
                    Firma = end.Firma,
                    Cantidad = end.Cantidad,
                    Coste = end.Coste,
                    IDVela = end.IDVela
                };

                context.Endurecedor.Add(enda);
                await context.SaveChangesAsync();

                response.Object = mapper.Map<Endurecedor>(enda);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        // Actualizar Endurecedor
        public async Task<CustomApiResponse<Endurecedor>> ActualizarEndurecedor(Endurecedor end) 
        {
            var response = new CustomApiResponse<Endurecedor>();

            try
            {
                var enda = context.Endurecedor.SingleOrDefault(x => x.IDEndurecedor == end.IDEndurecedor);

                if (enda == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "Endurecedor no encontrado" };
                    return response;
                }

                enda.Firma = end.Firma ?? enda.Firma;
                enda.Tipo = end.Tipo ?? enda.Tipo;
                enda.CompradoEn = end.CompradoEn ?? enda.CompradoEn;
                enda.IDVela = end.IDVela != Guid.Empty ? end.IDVela : enda.IDVela;
                enda.Coste = end.Coste;
                enda.Cantidad = end.Cantidad;

                await context.SaveChangesAsync();
                response.Object = mapper.Map<Endurecedor>(enda);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        // Obtener todos los endurecedores
        public async Task<CustomApiResponse<List<Endurecedor>>> GetEndurecedor<Endurecedor>() 
        {
            var response = new CustomApiResponse<List<Endurecedor>>();

            try
            {
                var datos = context.Endurecedor.ToList();
                response.Object = mapper.Map<List<Endurecedor>>(datos);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        // Buscar uno por ID
        public async Task<CustomApiResponse<Endurecedor>> BuscarEndurecedor(Guid idEndurecedor) 
        {
            var response = new CustomApiResponse<Endurecedor>();

            try
            {
                var entidad = context.Endurecedor.SingleOrDefault(x => x.IDEndurecedor == idEndurecedor);

                if (entidad == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "Endurecedor no encontrado" };
                    return response;
                }

                response.Object = mapper.Map<Endurecedor>(entidad);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        // Método privado si quieres reutilizarlo sin mapeo
        private Endurecedor BuscarEndurecedorEntity(Guid? idEndurecedor)
        {
            return this.context.Endurecedor.SingleOrDefault(x => x.IDEndurecedor == idEndurecedor);
        }

        public async Task<CustomApiResponse<bool>> EliminarEndurecedor(Guid idEndurecedor)
        {
            var response = new CustomApiResponse<bool>();

            try
            {
                var Endurecedor = context.Endurecedor.SingleOrDefault(x => x.IDEndurecedor == idEndurecedor);

                if (Endurecedor == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "Endurecedor no encontrado" };
                    response.Object = false;
                }
                else
                {
                    context.Set<Endurecedor>().Remove(Endurecedor);
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

