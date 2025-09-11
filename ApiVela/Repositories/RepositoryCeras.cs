using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVela.Data;
using ApiVela.Models;

namespace ApiVela.Repository
{
    public class RepositoryCeras
    {
        private readonly Contexto context;

        public RepositoryCeras(Contexto context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // ------------------------------------- CERA ---------------------------------------------
        public void InsertarCera(Cera cer)
        {
            Cera cera = new Cera();

            //int? count = (from datos in context.Mecha
            //              select datos.IDMecha).Count();

            //if (count == 0)
            //{
            //    cera.IDCera =Guid.NewGuid();
            //}
            //else
            //{
            //    //Error
            //}
            cera.IDCera = Guid.NewGuid();

            cera.Tipo = cer.Tipo;
            cera.CompradoEn = cer.CompradoEn;
            cera.Firma = cer.Firma;
            //cera.IDVela = Guid.NewGuid();
            cera.Cantidad = cer.Cantidad;
            cera.Coste = cer.Coste;

            this.context.Cera.Add(cera);
            this.context.SaveChanges();
        }

        public void ActualizarCera(Cera cer)
        {
            Cera cera = BuscarCera(cer.IDCera);


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

            //this.context.SaveChanges();
        }

        public List<Cera> GetCeras()
        {
            return this.context.Cera.ToList();
        }

        public Cera BuscarCera(Guid idCera)
        {
            return this.context.Cera.SingleOrDefault
                (x => x.IDCera == idCera);
        }

    }
}
