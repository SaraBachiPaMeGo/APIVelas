using System;
using System.Collections.Generic;
using System.Linq;
using ApiVela.Data;
using ApiVela.Models;
using AutoMapper;

namespace ApiVela.Repository
{
    public class RepositoryMechas
    {
        private readonly Contexto context;
        private readonly IMapper mapper;

        public RepositoryMechas(Contexto context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public CustomApiResponse<List<Mecha>> GetMechas()
        {
            var response = new CustomApiResponse<List<Mecha>>();
            try
            {
                var mechas = context.Mecha.ToList();
                response.Object = mapper.Map<List<Mecha>>(mechas);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public CustomApiResponse<Mecha> BuscarMecha(Guid idMecha)
        {
            var response = new CustomApiResponse<Mecha>();
            try
            {
                var mecha = context.Mecha.SingleOrDefault(m => m.IDMecha == idMecha);
                if (mecha == null) throw new Exception("Mecha no encontrada");
                response.Object = mapper.Map<Mecha>(mecha);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public CustomApiResponse<Mecha> InsertarMecha(Mecha mech)
        {
            var response = new CustomApiResponse<Mecha>();
            try
            {
                // Puedes agregar validaciones aquí si es necesario, por ejemplo permitir solo una mecha:
                // if (context.Mecha.Any()) throw new Exception("Solo se permite una mecha");

                mech.IDVela = mech.IDVela != Guid.Empty ? mech.IDVela : mech.IDVela;
                var mecha = mapper.Map<Mecha>(mech);
                mecha.IDMecha = Guid.NewGuid();

                context.Mecha.Add(mecha);
                context.SaveChanges();

                response.Object = mapper.Map<Mecha>(mecha);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public CustomApiResponse<Mecha> ActualizarMecha(Mecha mech)
        {
            var response = new CustomApiResponse<Mecha>();
            try
            {
                var mechaExistente = context.Mecha.SingleOrDefault(m => m.IDMecha == mech.IDMecha);
                if (mechaExistente == null) throw new Exception("Mecha no encontrada");

                // Actualiza solo los campos que hayan cambiado
                if (mech.Firma != mechaExistente.Firma) mechaExistente.Firma = mech.Firma;
                if (mech.Tipo != mechaExistente.Tipo) mechaExistente.Tipo = mech.Tipo;
                if (mech.CompradoEn != mechaExistente.CompradoEn) mechaExistente.CompradoEn = mech.CompradoEn;
                if (mech.IDVela != mechaExistente.IDVela) mechaExistente.IDVela = mech.IDVela;
                if (mech.Cantidad != mechaExistente.Cantidad) mechaExistente.Cantidad = mech.Cantidad;
                if (mech.Coste != mechaExistente.Coste) mechaExistente.Coste = mech.Coste;

                context.SaveChanges();

                response.Object = mapper.Map<Mecha>(mechaExistente);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public CustomApiResponse<bool> EliminarMechaAsync(Guid idMecha)
        {
            var response = new CustomApiResponse<bool>();

            try
            {
                var Mecha = context.Mecha.SingleOrDefault(x => x.IDMecha == idMecha);

                if (Mecha == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "Mecha no encontrado" };
                    response.Object = false;
                }
                else
                {
                    context.Set<Mecha>().Remove(Mecha);
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
