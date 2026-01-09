using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVela.Data;
using ApiVela.Models;
using AutoMapper;

namespace ApiVela.Repository
{
    public class RepositoryFragancias
    {
        private readonly Contexto context;
        private readonly IMapper mapper;

        public RepositoryFragancias(Contexto context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // Insertar Fragancia
        public CustomApiResponse<Fragancia> InsertarFragancia(Fragancia fragan) 
        {
            var response = new CustomApiResponse<Fragancia>();

            try
            {
                var frag = new Fragancia
                {
                    IDFrag = Guid.NewGuid(),
                    FragNombre = fragan.FragNombre,
                    Tipo = fragan.Tipo,
                    CompradoEn = fragan.CompradoEn,
                    Firma = fragan.Firma,
                    IDVela = fragan.IDVela,
                    Coste = fragan.Coste,
                    Cantidad = fragan.Cantidad
                };

                context.Fragancia.Add(frag);
                context.SaveChanges();

                response.Object = mapper.Map<Fragancia>(frag);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        // Actualizar Fragancia
        public CustomApiResponse<Fragancia> ActualizarFragancia(Fragancia fragan) 
        {
            var response = new CustomApiResponse<Fragancia>();

            try
            {
                var frag = BuscarFraganciaEntity(fragan.IDFrag);
                if (frag == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "Fragancia no encontrada" };
                    return response;
                }

                frag.Firma = fragan.Firma ?? frag.Firma;
                frag.Tipo = fragan.Tipo ?? frag.Tipo;
                frag.CompradoEn = fragan.CompradoEn ?? frag.CompradoEn;
                frag.FragNombre = fragan.FragNombre ?? frag.FragNombre;
                frag.IDVela = fragan.IDVela != Guid.Empty ? fragan.IDVela : frag.IDVela;
                frag.Coste = fragan.Coste;
                frag.Cantidad = fragan.Cantidad;

                context.SaveChanges();
                response.Object = mapper.Map<Fragancia>(frag);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        // Obtener todas las fragancias
        public CustomApiResponse<List<Fragancia>> GetFragancias() 
        {
            var response = new CustomApiResponse<List<Fragancia>>();

            try
            {
                var frags = context.Fragancia.ToList();
                response.Object = mapper.Map<List<Fragancia>>(frags);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        // Buscar una fragancia por ID
        public CustomApiResponse<Fragancia> BuscarFragancia(Guid idFragancia) 
        {
            var response = new CustomApiResponse<Fragancia>();

            try
            {
                var frag = context.Fragancia.SingleOrDefault(x => x.IDFrag == idFragancia);

                if (frag == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "Fragancia no encontrada" };
                    return response;
                }

                response.Object = mapper.Map<Fragancia>(frag);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        // Método interno privado (sin mapeo)
        private Fragancia BuscarFraganciaEntity(Guid? idFragancia)
        {
            return context.Fragancia.SingleOrDefault(x => x.IDFrag == idFragancia);
        }

        public CustomApiResponse<bool> EliminarFrag(Guid idFrag)
        {
            var response = new CustomApiResponse<bool>();

            try
            {
                var Frag = context.Fragancia.SingleOrDefault(x => x.IDFrag == idFrag);

                if (Frag == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "Frag no encontrado" };
                    response.Object = false;
                }
                else
                {
                    context.Set<Fragancia>().Remove(Frag);
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
