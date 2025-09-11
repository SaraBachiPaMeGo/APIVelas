using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVela.Data;
using ApiVela.Models;

namespace ApiVela.Repository
{
    public class RepositoryFragancias
    {
        private readonly Contexto context;

        public RepositoryFragancias(Contexto context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // ------------------------------------- FRAGANCIA ---------------------------------------------

        public void InsertarFragancia(Fragancia fragan)
        {
            //String fragNombre, String tipo,
            //String compradoEn, String firma, int iDVela, int idCoste
            Fragancia frag = new Fragancia();

            //int? count = (from datos in context.Fragancia
            //              select datos.IDFrag).Count();

            //if (count == 0)
            //{
            //    frag.IDFrag = Guid.NewGuid();
            //}
            //else
            //{
            //    //Error
            //}

            frag.IDFrag = Guid.NewGuid();
            frag.FragNombre = fragan.FragNombre;
            frag.Tipo = fragan.Tipo;
            frag.CompradoEn = fragan.CompradoEn;
            frag.Firma = fragan.Firma;
            frag.IDVela = fragan.IDVela;
            frag.Coste = fragan.Coste;
            frag.Cantidad = fragan.Cantidad;


            this.context.Fragancia.Add(frag);
            this.context.SaveChanges();
        }

        public void ActualizarFragancia(Fragancia fragan)
        {
            Fragancia frag = BuscarFragancia(fragan.IDFrag);

            if (fragan.Firma != frag.Firma)
            {
                frag.Firma = fragan.Firma;

            }

            if (fragan.Tipo != frag.Tipo)
            {
                frag.Tipo = fragan.Tipo;

            }

            if (fragan.CompradoEn != frag.CompradoEn)
            {
                frag.CompradoEn = fragan.CompradoEn;
            }

            if (fragan.IDVela != frag.IDVela)
            {
                frag.IDVela = fragan.IDVela;
            }

            if (fragan.FragNombre != frag.FragNombre)
            {
                frag.FragNombre = fragan.FragNombre;
            }

            if (fragan.Coste != frag.Coste)
            {
                frag.Coste = fragan.Coste;
            }

            if (fragan.Cantidad != frag.Cantidad)
            {
                frag.Cantidad = fragan.Cantidad;
            }


            this.context.SaveChanges();
        }

        public List<Fragancia> GetFragancias()
        {
            int? count = (from datos in context.Fragancia
                          select datos.IDFrag).Count();

            Fragancia frag = this.context.Fragancia.FirstOrDefault();

            List<Fragancia> frags = this.context.Fragancia.ToList();

            return frags;
        }

        public Fragancia BuscarFragancia(Guid idFragancia)
        {
            return this.context.Fragancia.SingleOrDefault
                (x => x.IDFrag == idFragancia);
        }
    }
}
