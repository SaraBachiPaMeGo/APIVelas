using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVela.Data;
using ApiVela.Models;

namespace ApiVela.Repository
{
    public class RepositoryPedidos
    {
        private readonly Contexto context;

        public RepositoryPedidos(Contexto context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

         //------------------------------------- PEDIDO ---------------------------------------------
        public void InsertarPedido(Guid idCliente, Guid iDVela)
        {
            Pedido pedi = new Pedido();

            int? count = (from datos in context.Pedido
                          select datos.IDPedido).Count();

            if (count == 0)
            {
                pedi.IDPedido = Guid.NewGuid();
            }
            else
            {
                //Error
            }

            pedi.IDCliente = idCliente;
            pedi.IDVela = iDVela;

            this.context.Pedido.Add(pedi);
            this.context.SaveChanges();
        }

        public void ActualizarPedido(Guid idPedo, DateTime fechaEntrega, Guid idCliente,
            Guid iDVela)
        {
            Pedido pedi = BuscarPedido(idPedo);


            if (fechaEntrega != pedi.FechaEntrega)
            {
                pedi.FechaEntrega = fechaEntrega;

            }

            if (idCliente != pedi.IDCliente)
            {
                pedi.IDCliente = idCliente;

            }

            if (iDVela != pedi.IDVela)
            {
                pedi.IDVela = iDVela;
            }

            this.context.SaveChanges();
        }

        public List<Pedido> GetPedidos()
        {
            return this.context.Pedido.ToList();
        }

        public Pedido BuscarPedido(Guid idPedo)
        {
            return this.context.Pedido.SingleOrDefault
                (x => x.IDPedido == idPedo);
        }


    }
}
