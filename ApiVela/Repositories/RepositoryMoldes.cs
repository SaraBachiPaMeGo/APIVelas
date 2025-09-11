using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVela.Data;
using ApiVela.Models;

namespace ApiVela.Repository
{
    public class RepositoryMoldes
    {
        private readonly Contexto context;

        public RepositoryMoldes(Contexto context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

         //------------------------------------- MOLDE ---------------------------------------------

        public void InsertarMolde(Molde mol)
        {
            Molde molde = new Molde();

            int? count = (from datos in context.Molde
                          select datos.IDMolde).Count();

            if (count == 0)
            {
                molde.IDMolde = Guid.NewGuid();
            }
            else
            {
                //Error
            }

            molde.MoldeNombre = mol.MoldeNombre;
            molde.CMMecha = mol.CMMecha;
            molde.GramCera = mol.GramCera;
            molde.Medidas = mol.Medidas;
            molde.Duracion = mol.Duracion;
            molde.Observ = mol.Observ;
            molde.CompradoEn = mol.CompradoEn;
            molde.Firma = mol.Firma;
            molde.IDVela = mol.IDVela;
            molde.MilAgua = mol.MilAgua;
            molde.Tipo = mol.Tipo;
            molde.Coste = mol.Coste;

            this.context.Molde.Add(molde);
            this.context.SaveChanges();
        }

        public void ActualizarMolde(Molde mol)
        {
            Molde molde = BuscarMolde(mol.IDMolde);

            if (mol.Firma != molde.Firma)
            {
                molde.Firma = mol.Firma;
            }

            if (mol.Tipo != molde.Tipo)
            {
                molde.Tipo = mol.Tipo;

            }

            if (mol.CompradoEn != molde.CompradoEn)
            {
                molde.CompradoEn = mol.CompradoEn;
            }

            if (mol.IDVela != molde.IDVela)
            {
                molde.IDVela = mol.IDVela;
            }

            if (mol.CMMecha != molde.CMMecha)
            {
                molde.CMMecha = mol.CMMecha;
            }

            if (mol.GramCera != molde.GramCera)
            {
                molde.GramCera = mol.GramCera;
            }

            if (mol.Medidas != molde.Medidas)
            {
                molde.Medidas = mol.Medidas;
            }

            if (mol.Duracion != molde.Duracion)
            {
                molde.Duracion = mol.Duracion;
            }

            if (mol.Observ != molde.Observ)
            {
                molde.Observ = mol.Observ;
            }

            if (mol.CompradoEn != molde.CompradoEn)
            {
                molde.CompradoEn = mol.CompradoEn;
            }

            if (mol.Coste != molde.Coste)
            {
                molde.Coste = mol.Coste;
            }


            molde.Coste = mol.Coste;
            this.context.SaveChanges();
        }

        public List<Molde> GetMoldes()
        {
            try
            {

                Molde mold = this.context.Molde.FirstOrDefault();
                return this.context.Molde.ToList();
            }
            catch (Exception x)
            {

                throw x;
            }
        }

        public Molde BuscarMolde(Guid idMolde)
        {
            return this.context.Molde.SingleOrDefault
                (x => x.IDMolde == idMolde);
        }
    }
}
