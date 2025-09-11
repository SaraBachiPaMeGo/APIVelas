using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVela.Data;
using ApiVela.Models;

namespace ApiVela.Repository
{
    public class RepositoryPacks
    {
        private readonly Contexto context;

        public RepositoryPacks(Contexto context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // ------------------------------------- PACK ---------------------------------------------
        public void InsertarPack(Pack pack)
        {
            Pack packa = new Pack();

            //int? count = (from datos in context.Mecha
            //              select datos.IDMecha).Count();

            //if (count == 0)
            //{
            //    packa.IDCera =Guid.NewGuid();
            //}
            //else
            //{
            //    //Error
            //}
            packa.IDPack = Guid.NewGuid();

            packa.Tipo = pack.Tipo;
            packa.CompradoEn = pack.CompradoEn;
            packa.Firma = pack.Firma;
            //packa.IDVela = Guid.NewGuid();
            packa.Cantidad = pack.Cantidad;
            packa.Coste = pack.Coste;

            this.context.Pack.Add(packa);
            this.context.SaveChanges();
        }

        public void ActualizarPack(Pack pack)
        {
            Pack packa = BuscarPack(pack.IDPack);


            if (pack.Firma != packa.Firma)
            {
                packa.Firma = pack.Firma;

            }

            if (pack.Tipo != packa.Tipo)
            {
                packa.Tipo = pack.Tipo;

            }

            if (pack.CompradoEn != packa.CompradoEn)
            {
                packa.CompradoEn = pack.CompradoEn;
            }

            if (pack.IDVela != packa.IDVela)
            {
                packa.IDVela = pack.IDVela;
            }

            if (pack.Coste != packa.Coste)
            {
                packa.Coste = pack.Coste;

            }

            if (pack.Cantidad != packa.Cantidad)
            {
                packa.Cantidad = pack.Cantidad;

            }

            this.context.SaveChanges();
        }

        public List<Pack> GetPacks()
        {
            return this.context.Pack.ToList();
        }

        public Pack BuscarPack(Guid idPack)
        {
            return this.context.Pack.SingleOrDefault
                (x => x.IDPack == idPack);
        }
    }
}
