using System;
using System.Collections.Generic;
using System.Linq;
using ApiVela.Data;
using ApiVela.Models;
using AutoMapper;

namespace ApiVela.Repository
{
    public class RepositoryClientes
    {
        private readonly Contexto context;
        private readonly IMapper mapper;

        public RepositoryClientes(Contexto context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public CustomApiResponse<List<Cliente>> GetClientes()
        {
            var response = new CustomApiResponse<List<Cliente>>();
            try
            {
                var clientes = context.Cliente.ToList();
                response.Object = mapper.Map<List<Cliente>>(clientes);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public CustomApiResponse<Cliente> BuscarCliente(Guid idCliente)
        {
            var response = new CustomApiResponse<Cliente>();
            try
            {
                var cliente = context.Cliente.SingleOrDefault(c => c.IDCliente == idCliente);
                if (cliente == null) throw new Exception("Cliente no encontrado");
                response.Object = mapper.Map<Cliente>(cliente);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public CustomApiResponse<Cliente> InsertarCliente(Cliente clie)
        {
            var response = new CustomApiResponse<Cliente>();
            try
            {
                // Por ejemplo, si solo quieres permitir un cliente, lo verificas acá:
                if (context.Cliente.Any())
                    throw new Exception("Solo se permite un cliente");

                var cliente = mapper.Map<Cliente>(clie);
                cliente.IDCliente = Guid.NewGuid();

                context.Cliente.Add(cliente);
                context.SaveChanges();

                response.Object = mapper.Map<Cliente>(cliente);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public CustomApiResponse<Cliente> ActualizarCliente(Cliente cli)
        {
            var response = new CustomApiResponse<Cliente>();
            try
            {
                var cliente = context.Cliente.SingleOrDefault(c => c.IDCliente == cli.IDCliente);
                if (cliente == null) throw new Exception("Cliente no encontrado");

                mapper.Map(cliente, cliente);

                context.SaveChanges();

                response.Object = mapper.Map<Cliente>(cliente);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }
    }
}
