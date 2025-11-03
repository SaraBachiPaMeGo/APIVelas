using System;
using System.Collections.Generic;
using System.Linq;
using ApiVela.Data;
using ApiVela.Models;
using AutoMapper;

namespace ApiVela.Repository
{
    public class RepositoryPedidos
    {
        private readonly Contexto context;
        private readonly IMapper mapper;

        public RepositoryPedidos(Contexto context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // ---------------------------- INSERTAR ----------------------------
        public CustomApiResponse<Pedido> InsertarPedido(Pedido pedid) 
        {
            var response = new CustomApiResponse<Pedido>();

            try
            {
                var pedi = mapper.Map<Pedido>(pedid);
                pedi.IDPedido = Guid.NewGuid();

                context.Pedido.Add(pedi);
                context.SaveChanges();

                response.Object = mapper.Map<Pedido>(pedi);

                context.Pedido.Add(pedi);
                context.SaveChanges();

                response.Object = mapper.Map<Pedido>(pedi);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        // ---------------------------- ACTUALIZAR ----------------------------
        public CustomApiResponse<Pedido> ActualizarPedido(Pedido pedi) 
        {
            var response = new CustomApiResponse<Pedido>();

            try
            {
                var existing = context.Pedido.SingleOrDefault(p => p.IDPedido == pedi.IDPedido);
                if (existing == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "Pedido no encontrado" };
                    return response;
                }

                // Actualizar solo si cambian
                existing.FechaEntrega = pedi.FechaEntrega != default(DateTime) ? pedi.FechaEntrega : existing.FechaEntrega;
                existing.IDCliente = pedi.IDCliente != Guid.Empty ? pedi.IDCliente : existing.IDCliente;
                existing.IDVela = pedi.IDVela != Guid.Empty ? pedi.IDVela : existing.IDVela;

                context.SaveChanges();

                response.Object = mapper.Map<Pedido>(existing);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        // ---------------------------- OBTENER TODOS ----------------------------
        public CustomApiResponse<List<Pedido>> GetPedidos() 
        {
            var response = new CustomApiResponse<List<Pedido>>();

            try
            {
                var list = context.Pedido.ToList();
                response.Object = mapper.Map<List<Pedido>>(list);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        // ---------------------------- BUSCAR POR ID ----------------------------
        public CustomApiResponse<Pedido> BuscarPedido(Guid idPedido) 
        {
            var response = new CustomApiResponse<Pedido>();

            try
            {
                var pedido = context.Pedido.SingleOrDefault(p => p.IDPedido == idPedido);
                if (pedido == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "Pedido no encontrado" };
                    return response;
                }

                response.Object = mapper.Map<Pedido>(pedido);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }
    }
}
