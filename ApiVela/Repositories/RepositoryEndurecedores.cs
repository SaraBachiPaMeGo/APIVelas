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
        public CustomApiResponse<T> InsertarEndurecedor<T>(Endurecedor cer) where T : class
        {
            var response = new CustomApiResponse<T>();

            try
            {
                var cera = new Endurecedor
                {
                    IDEndurecedor = Guid.NewGuid(),
                    Tipo = cer.Tipo,
                    CompradoEn = cer.CompradoEn,
                    Firma = cer.Firma,
                    Cantidad = cer.Cantidad,
                    Coste = cer.Coste,
                    IDVela = cer.IDVela
                };

                context.Endurecedor.Add(cera);
                context.SaveChanges();

                response.Object = mapper.Map<T>(cera);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        // Actualizar Endurecedor
        public CustomApiResponse<T> ActualizarEndurecedor<T>(Endurecedor cer) where T : class
        {
            var response = new CustomApiResponse<T>();

            try
            {
                var cera = BuscarEndurecedorEntity(cer.IDEndurecedor);
                if (cera == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "Endurecedor no encontrado" };
                    return response;
                }

                cera.Firma = cer.Firma ?? cera.Firma;
                cera.Tipo = cer.Tipo ?? cera.Tipo;
                cera.CompradoEn = cer.CompradoEn ?? cera.CompradoEn;
                cera.IDVela = cer.IDVela != Guid.Empty ? cer.IDVela : cera.IDVela;
                cera.Coste = cer.Coste;
                cera.Cantidad = cer.Cantidad;

                context.SaveChanges();
                response.Object = mapper.Map<T>(cera);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        // Obtener todos los endurecedores
        public CustomApiResponse<List<T>> GetEndurecedor<T>() where T : class
        {
            var response = new CustomApiResponse<List<T>>();

            try
            {
                var datos = context.Endurecedor.ToList();
                response.Object = mapper.Map<List<T>>(datos);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        // Buscar uno por ID
        public CustomApiResponse<T> BuscarEndurecedor<T>(Guid idEndurecedor) where T : class
        {
            var response = new CustomApiResponse<T>();

            try
            {
                var entidad = context.Endurecedor.SingleOrDefault(x => x.IDEndurecedor == idEndurecedor);

                if (entidad == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "Endurecedor no encontrado" };
                    return response;
                }

                response.Object = mapper.Map<T>(entidad);
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
            return context.Endurecedor.SingleOrDefault(x => x.IDEndurecedor == idEndurecedor);
        }
    }
}

