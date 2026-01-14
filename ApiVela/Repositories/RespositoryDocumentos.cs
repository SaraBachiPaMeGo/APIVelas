using ApiVela.Data;
using ApiVela.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVela.Repositories
{
    public class RespositoryDocumentos
    {
        private readonly Contexto context;
        private readonly IMapper mapper;

        public RespositoryDocumentos(Contexto context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CustomApiResponse<List<Documento>>> GetDocumentos()
        {
            var response = new CustomApiResponse<List<Documento>>();
            try
            {
                var Documentos = context.Documento.ToList();
                response.Object = mapper.Map<List<Documento>>(Documentos);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public async Task<CustomApiResponse<Documento>> BuscarDocumento(Guid idDocumento)
        {
            var response = new CustomApiResponse<Documento>();
            try
            {
                var Documento = context.Documento.SingleOrDefault(x => x.IDDoc == idDocumento);
                if (Documento == null) throw new Exception("Documento no encontrado");
                response.Object = mapper.Map<Documento>(Documento);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public async Task<CustomApiResponse<Documento>> InsertarDocumento(Documento doc)
        {
            var response = new CustomApiResponse<Documento>();
            try
            {
                var Documento = mapper.Map<Documento>(doc);
                Documento.IDDoc = Guid.NewGuid();

                context.Documento.Add(Documento);
                await context.SaveChangesAsync();

                response.Object = mapper.Map<Documento>(Documento);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public async Task<CustomApiResponse<Documento>> ActualizarDocumento(Documento doc)
        {
            var response = new CustomApiResponse<Documento>();
            try
            {
                var DocumentoExistente = context.Documento.SingleOrDefault(x => x.IDDoc == doc.IDDoc);
                if (DocumentoExistente == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "Documento no encontrada" };
                    return response;
                }

                // Actualiza solo las propiedades que desees (puedes ajustar según tu lógica)
                if (doc.Ruta != DocumentoExistente.Ruta) DocumentoExistente.Ruta = doc.Ruta;
                if (doc.NombreDoc != DocumentoExistente.NombreDoc) DocumentoExistente.NombreDoc = doc.NombreDoc;
                if (doc.TipoDoc != DocumentoExistente.TipoDoc) DocumentoExistente.TipoDoc = doc.TipoDoc;
                if (doc.Datos != DocumentoExistente.Datos) DocumentoExistente.Datos = doc.Datos;

                await context.SaveChangesAsync();

                response.Object = mapper.Map<Documento>(DocumentoExistente);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public async Task<CustomApiResponse<bool>> EliminarDocumento(Guid idDocumento)
        {
            var response = new CustomApiResponse<bool>();

            try
            {
                var Documento = context.Documento.SingleOrDefault(x => x.IDDoc == idDocumento);

                if (Documento == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "Documento no encontrado" };
                    response.Object = false;
                }
                else
                {
                    context.Set<Documento>().Remove(Documento);
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
