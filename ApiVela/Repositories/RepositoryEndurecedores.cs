using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVela.Data;
using ApiVela.Models;

namespace ApiVela.Repository
{
    public class RepositoryEndurecedores
    {
        private readonly Contexto context;

        public RepositoryEndurecedores(Contexto context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // ------------------------------------- ENDURECEDOR ---------------------------------------------
        public void InsertarEndurecedor(Endurecedor cer)
        {
            Endurecedor cera = new Endurecedor();

            //int? count = (from datos in context.Mecha
            //              select datos.IDMecha).Count();

            //if (count == 0)
            //{
            //    cera.IDEndurecedor =Guid.NewGuid();
            //}
            //else
            //{
            //    //Error
            //}
            cera.IDEndurecedor = Guid.NewGuid();

            cera.Tipo = cer.Tipo;
            cera.CompradoEn = cer.CompradoEn;
            cera.Firma = cer.Firma;
            //cera.IDVela = Guid.NewGuid();
            cera.Cantidad = cer.Cantidad;
            cera.Coste = cer.Coste;

            this.context.Endurecedor.Add(cera);
            this.context.SaveChanges();
        }

        public void ActualizarEndurecedor(Endurecedor cer)
        {
            Endurecedor cera = BuscarEndurecedor(cer.IDEndurecedor);


            if (cer.Firma != cera.Firma)
            {
                cera.Firma = cer.Firma;

            }

            if (cer.Tipo != cera.Tipo)
            {
                cera.Tipo = cer.Tipo;

            }

            if (cer.CompradoEn != cera.CompradoEn)
            {
                cera.CompradoEn = cer.CompradoEn;
            }

            if (cer.IDVela != cera.IDVela)
            {
                cera.IDVela = cer.IDVela;
            }

            if (cer.Coste != cera.Coste)
            {
                cera.Coste = cer.Coste;

            }

            if (cer.Cantidad != cera.Cantidad)
            {
                cera.Cantidad = cer.Cantidad;

            }

            this.context.SaveChanges();
        }

        public List<Endurecedor> GetEndurecedor()
        {
            return this.context.Endurecedor.ToList();
        }

        public Endurecedor BuscarEndurecedor(Guid idEndurecedor)
        {
            return this.context.Endurecedor.SingleOrDefault
                (x => x.IDEndurecedor == idEndurecedor);
        }
    }
}
