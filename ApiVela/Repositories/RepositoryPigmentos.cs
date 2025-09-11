using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVela.Data;
using ApiVela.Models;

namespace ApiVela.Repository
{
    public class RepositoryPigmentos
    {
        private readonly Contexto context;

        public RepositoryPigmentos(Contexto context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // ------------------------------------- PIGMENTO ---------------------------------------------
        public void InsertarPigmento(Pigmento pi)
        {
            Pigmento pig = new Pigmento();

            //int? count = (from datos in context.Pigmento
            //              select datos.IDPig).Count();

            //if (count == 0)
            //{
            //    pig.IDPig =Guid.NewGuid();
            //}
            //else
            //{
            //    //Error
            //}

            pig.IDPig = Guid.NewGuid();
            pig.Firma = pi.Firma;
            pig.Tipo = pi.Tipo;
            pig.ColorNombre = pi.ColorNombre;
            pig.CompradoEn = pi.CompradoEn;
            pig.IDVela = pi.IDVela;
            pig.Coste = pi.Coste;
            pig.Cantidad = pi.Cantidad;

            this.context.Pigmento.Add(pig);
            this.context.SaveChanges();
        }

        public void ActualizarPigmento(Pigmento pi)
        {
            Pigmento pig = BuscarPigmento(pi.IDPig);

            if (pi.Firma != pig.Firma)
            {
                pig.Firma = pi.Firma;

            }

            if (pi.Tipo != pig.Tipo)
            {
                pig.Tipo = pi.Tipo;

            }

            if (pi.ColorNombre != pig.ColorNombre)
            {
                pig.ColorNombre = pi.ColorNombre;
            }

            if (pi.CompradoEn != pig.CompradoEn)
            {
                pig.CompradoEn = pi.CompradoEn;
            }

            if (pi.IDVela != pig.IDVela)
            {
                pig.IDVela = pi.IDVela;
            }

            if (pi.Coste != pig.Coste)
            {
                pig.Coste = pi.Coste;
            }

            if (pi.Cantidad != pig.Cantidad)
            {
                pig.Cantidad = pi.Cantidad;
            }

            this.context.SaveChanges();
        }

        public List<Pigmento> GetPigmentos()
        {
            return this.context.Pigmento.ToList();
        }

        public Pigmento BuscarPigmento(Guid idPig)
        {
            return this.context.Pigmento.SingleOrDefault
                (x => x.IDPig == idPig);
        }
    }
}
