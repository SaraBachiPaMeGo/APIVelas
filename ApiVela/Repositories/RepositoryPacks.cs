using System;
using System.Collections.Generic;
using System.Linq;
using ApiVela.Data;
using ApiVela.Models;
using AutoMapper;

namespace ApiVela.Repository
{
    public class RepositoryPacks
    {
        private readonly Contexto context;
        private readonly IMapper mapper;

        public RepositoryPacks(Contexto context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // ---------------------------- INSERTAR ----------------------------
        public CustomApiResponse<Pack> InsertarPack(Pack pack) 
        {
            var response = new CustomApiResponse<Pack>();
            try
            {
                var packEntity = new Pack
                {
                    IDPack = Guid.NewGuid(),
                    Tipo = pack.Tipo,
                    CompradoEn = pack.CompradoEn,
                    Firma = pack.Firma,
                    IDVela = pack.IDVela,
                    Cantidad = pack.Cantidad,
                    Coste = pack.Coste
                };

                context.Pack.Add(packEntity);
                context.SaveChanges();

                response.Object = mapper.Map<Pack>(packEntity);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        // ---------------------------- ACTUALIZAR ----------------------------
        public CustomApiResponse<Pack> ActualizarPack(Pack pack) 
        {
            var response = new CustomApiResponse<Pack>();
            try
            {
                var existing = context.Pack.SingleOrDefault(p => p.IDPack == pack.IDPack);
                if (existing == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "Pack no encontrado" };
                    return response;
                }

                // Actualizar solo lo que sea distinto o que tenga sentido
                existing.Firma = pack.Firma ?? existing.Firma;
                existing.Tipo = pack.Tipo ?? existing.Tipo;
                existing.CompradoEn = pack.CompradoEn ?? existing.CompradoEn;
                existing.IDVela = pack.IDVela != Guid.Empty ? pack.IDVela : existing.IDVela;
                existing.Coste = pack.Coste;
                existing.Cantidad = pack.Cantidad;

                context.SaveChanges();

                response.Object = mapper.Map<Pack>(existing);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        // ---------------------------- OBTENER TODOS ----------------------------
        public CustomApiResponse<List<Pack>> GetPacks() 
        {
            var response = new CustomApiResponse<List<Pack>>();
            try
            {
                var list = context.Pack.ToList();
                response.Object = mapper.Map<List<Pack>>(list);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        // ---------------------------- BUSCAR POR ID ----------------------------
        public CustomApiResponse<Pack> BuscarPack(Guid idPack) 
        {
            var response = new CustomApiResponse<Pack>();
            try
            {
                var packEntity = context.Pack.SingleOrDefault(p => p.IDPack == idPack);
                if (packEntity == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "Pack no encontrado" };
                    return response;
                }

                response.Object = mapper.Map<Pack>(packEntity);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }
    }
}
