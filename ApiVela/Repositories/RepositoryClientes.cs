using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVela.Data;
using ApiVela.Models;

namespace ApiVela.Repository
{
    public class RepositoryClientes
    {
        private readonly Contexto context;

        public RepositoryClientes(Contexto context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

         //------------------------------------- CLIENTE ---------------------------------------------
        public void InsertarCliente(Cliente clie)
        {
            Cliente cli = new Cliente();

            int? count = (from datos in context.Cliente
                          select datos.IDCliente).Count();

            if (count == 0)
            {
                cli.IDCliente = Guid.NewGuid();
            }
            else
            {
                //Error
            }

            cli.Nombre = clie.Nombre;
            cli.Direccion = clie.Direccion;
            cli.Telf = clie.Telf;
            cli.Email = clie.Email;
            cli.IDPedido = clie.IDPedido;

            //this.context.Cliente.Add(cli);
            //this.context.SaveChanges();
        }

        public void ActualizarCliente(Cliente clie)
        {
            Cliente cli = BuscarCliente(clie.IDCliente);


            if (clie.Nombre != cli.Nombre)
            {
                cli.Nombre = clie.Nombre;

            }

            if (clie.Direccion != cli.Direccion)
            {
                cli.Direccion = clie.Direccion;

            }

            if (clie.Telf != cli.Telf)
            {
                cli.Telf = clie.Telf;
            }

            if (clie.Email != cli.Email)
            {
                cli.Email = clie.Email;
            }

            if (clie.IDPedido != cli.IDPedido)
            {
                cli.IDPedido = clie.IDPedido;
            }

            //this.context.SaveChanges();
        }

        public List<Cliente> GetClientes()
        {
            return this.context.Cliente.ToList();
        }

        public Cliente BuscarCliente(Guid idCliente)
        {
            return this.context.Cliente.SingleOrDefault
                (x => x.IDCliente == idCliente);
        }

    }
}
