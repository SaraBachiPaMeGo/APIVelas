using System;
using System.Collections.Generic;
using System.Linq;
using ApiVela.Data;
using ApiVela.Models;
using AutoMapper;

namespace ApiVela.Repository
{
    public class RepositoryPigmentos
    {
        private readonly Contexto context;
        private readonly IMapper mapper;

        public RepositoryPigmentos(Contexto context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // ------------------------------------- INSERTAR ---------------------------------------------
        public CustomApiResponse<T> InsertarPigmento<T>(Pigmento pi)
        {
            var response = new CustomApiResponse<T>();

            try
            {
                var pig = new Pigmento
                {
                    IDPig = Guid.NewGuid(),
                    Firma = pi.Firma,
                    Tipo = pi.Tipo,
                    ColorNombre = pi.ColorNombre,
                    CompradoEn = pi.CompradoEn,
                    IDVela = pi.IDVela,
                    Coste = pi.Coste,
                    Cantidad = pi.Cantidad
                };

                context.Pigmento.Add(pig);
                context.SaveChanges();

                response.Object = mapper.Map<T>(pig);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        // ------------------------------------- ACTUALIZAR ---------------------------------------------
        public CustomApiResponse<T> ActualizarPigmento<T>(Pigmento pi)
        {
            var response = new CustomApiResponse<T>();

            try
            {
                var existing = context.Pigmento.SingleOrDefault(p => p.IDPig == pi.IDPig);
                if (existing == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "Pigmento no encontrado" };
                    return response;
                }

                // Solo actualizamos si cambia o si el valor nuevo es válido
                existing.Firma = pi.Firma ?? existing.Firma;
                existing.Tipo = pi.Tipo ?? existing.Tipo;
                existing.ColorNombre = pi.ColorNombre ?? existing.ColorNombre;
                existing.CompradoEn = pi.CompradoEn ?? existing.CompradoEn;
                existing.IDVela = pi.IDVela != Guid.Empty ? pi.IDVela : existing.IDVela;
                existing.Coste = pi.Coste;
                existing.Cantidad = pi.Cantidad;

                context.SaveChanges();

                response.Object = mapper.Map<T>(existing);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        // ------------------------------------- OBTENER TODOS ---------------------------------------------
        public CustomApiResponse<List<Pigmento>> GetPigmentos()
        {
            var response = new CustomApiResponse<List<Pigmento>>();

            try
            {
                var list = context.Pigmento.ToList();
                response.Object = mapper.Map<List<Pigmento>>(list);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        // ------------------------------------- BUSCAR POR ID ---------------------------------------------
        public CustomApiResponse<T> BuscarPigmento<T>(Guid idPig)
        {
            var response = new CustomApiResponse<T>();

            try
            {
                var pigmento = context.Pigmento.SingleOrDefault(p => p.IDPig == idPig);
                if (pigmento == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "Pigmento no encontrado" };
                    return response;
                }

                response.Object = mapper.Map<T>(pigmento);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        public CustomApiResponse<bool> EliminarPigAsync(Guid idPig)
        {
            var response = new CustomApiResponse<bool>();

            try
            {
                var Pig = context.Pigmento.SingleOrDefault(x => x.IDPig == idPig);

                if (Pig == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "Pig no encontrado" };
                    response.Object = false;
                }
                else
                {
                    context.Set<Pigmento>().Remove(Pig);
                    context.SaveChangesAsync();
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
