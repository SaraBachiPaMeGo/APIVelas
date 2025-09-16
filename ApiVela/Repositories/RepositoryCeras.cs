using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVela.Data;
using ApiVela.Models;
using AutoMapper;

namespace ApiVela.Repository
{
    public class RepositoryCeras
    {
        private readonly Contexto context;
        private readonly IMapper mapper;

        public RepositoryCeras(Contexto context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public CustomApiResponse<List<Cera>> GetCeras() 
        {
            var response = new CustomApiResponse<List<Cera>>();
            try
            {
                var ceras = context.Cera.ToList();
                response.Object = mapper.Map<List<Cera>>(ceras);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public CustomApiResponse<Cera> BuscarCera(Guid idCera) 
        {
            var response = new CustomApiResponse<Cera>();
            try
            {
                var cera = context.Cera.SingleOrDefault(x => x.IDCera == idCera);
                if (cera == null) throw new Exception("Cera no encontrada");
                response.Object = mapper.Map<Cera>(cera);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public CustomApiResponse<Cera> InsertarCera(Cera cer) 
        {
            var response = new CustomApiResponse<Cera>();
            try
            {
                var cera = mapper.Map<Cera>(cer);
                cera.IDCera = Guid.NewGuid();

                context.Cera.Add(cera);
                context.SaveChanges();

                response.Object = mapper.Map<Cera>(cera);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public CustomApiResponse<Cera> ActualizarCera(Cera cer) 
        {
            var response = new CustomApiResponse<Cera>();
            try
            {
                var ceraExistente = context.Cera.SingleOrDefault(x => x.IDCera == cer.IDCera);
                if (ceraExistente == null) throw new Exception("Cera no encontrada");

                // Actualiza solo las propiedades que desees (puedes ajustar según tu lógica)
                if (cer.Firma != ceraExistente.Firma) ceraExistente.Firma = cer.Firma;
                if (cer.Tipo != ceraExistente.Tipo) ceraExistente.Tipo = cer.Tipo;
                if (cer.CompradoEn != ceraExistente.CompradoEn) ceraExistente.CompradoEn = cer.CompradoEn;
                if (cer.IDVela != ceraExistente.IDVela) ceraExistente.IDVela = cer.IDVela;
                if (cer.Coste != ceraExistente.Coste) ceraExistente.Coste = cer.Coste;
                if (cer.Cantidad != ceraExistente.Cantidad) ceraExistente.Cantidad = cer.Cantidad;

                context.SaveChanges();

                response.Object = mapper.Map<Cera>(ceraExistente);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }
    }
}
