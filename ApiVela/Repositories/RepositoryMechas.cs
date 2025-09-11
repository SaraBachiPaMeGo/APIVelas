using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVela.Data;
using ApiVela.Models;

namespace ApiVela.Repository
{
    public class RepositoryMechas
    {
        private readonly Contexto context;

        public RepositoryMechas(Contexto context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // ------------------------------------- MECHA ---------------------------------------------

        public void InsertarMecha(Mecha mech)
        {
            Mecha mecha = new Mecha();

            int? count = (from datos in context.Mecha
                          select datos.IDMecha).Count();

            if (count == 0)
            {
                mecha.IDMecha = Guid.NewGuid();
            }
            else
            {
                //Error
            }

            mecha.Tipo = mech.Tipo;
            mecha.CompradoEn = mech.CompradoEn;
            mecha.Firma = mech.Firma;
            mecha.IDVela = mech.IDVela;
            mecha.Cantidad = mech.Cantidad;
            mecha.Coste = mech.Coste;

            this.context.Mecha.Add(mecha);
            this.context.SaveChanges();
        }

        public void ActualizarMecha(Mecha mech)
        {
            Mecha mecha = BuscarMecha(mech.IDMecha);

            if (mech.Firma != mecha.Firma)
            {
                mecha.Firma = mech.Firma;

            }

            if (mech.Tipo != mecha.Tipo)
            {
                mecha.Tipo = mech.Tipo;

            }

            if (mech.CompradoEn != mecha.CompradoEn)
            {
                mecha.CompradoEn = mech.CompradoEn;
            }

            if (mech.IDVela != mecha.IDVela)
            {
                mecha.IDVela = mech.IDVela;
            }

            if (mech.Cantidad != mecha.Cantidad)
            {
                mecha.Cantidad = mech.Cantidad;
            }

            if (mech.Coste != mecha.Coste)
            {
                mecha.Coste = mech.Coste;
            }

            this.context.SaveChanges();
        }

        public List<Mecha> GetMechas()
        {
            return this.context.Mecha.ToList();
        }

        public Mecha BuscarMecha(Guid idMecha)
        {
            return this.context.Mecha.SingleOrDefault
                (x => x.IDMecha == idMecha);
        }
    }
}
