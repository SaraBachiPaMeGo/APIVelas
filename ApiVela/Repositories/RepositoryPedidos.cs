using System;
using System.Collections.Generic;
using System.Linq;
using ApiVela.Data;
using ApiVela.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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

                // 🔹 Si el pedido incluye velas, asignarles el IDPedido
                if (pedi.Velas != null && pedi.Velas.Any())
                {
                    foreach (var vela in pedi.Velas)
                    {
                        vela.IDPedido = pedi.IDPedido;
                        vela.IDVela = Guid.NewGuid();
                    }
                }

                var clienteExiste = context.Cliente.Any(c => c.IDCliente == pedi.IDCliente);
                if (!clienteExiste)
                    response.Error.Mensaje = "No se encontró el cliente con ID" + pedi.IDCliente;

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
                var existing = context.Pedido
                    .Include(p => p.Velas) // Incluye las velas relacionadas
                    .SingleOrDefault(p => p.IDPedido == pedi.IDPedido);

                if (existing == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "Pedido no encontrado" };
                    return response;
                }

               
                // ✅ Nuevo campo "Vendido"
                existing.Vendido = pedi.Vendido;

                // ✅ Actualización de la lista de Velas
                if (pedi.Velas != null && pedi.Velas.Any())
                {
                    // Elimina las relaciones antiguas si es necesario
                    existing.Velas.Clear();

                    // Asocia las nuevas velas al pedido
                    foreach (var vela in pedi.Velas)
                    {
                        var velaExistente = context.Vela.FirstOrDefault(v => v.IDVela == vela.IDVela);
                        if (velaExistente != null)
                        {
                            existing.Velas.Add(velaExistente);
                        }
                        else
                        {
                            // Si es una vela nueva, la agregamos
                            vela.IDVela = Guid.NewGuid();
                            context.Vela.Add(vela);
                            existing.Velas.Add(vela);
                        }
                    }
                }

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
