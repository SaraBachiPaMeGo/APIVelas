using ApiVela.Data;
using ApiVela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVela.Repositories
{
    public class RepositoryVelaFragancia
    {

        private readonly Contexto context;

        public RepositoryVelaFragancia(Contexto context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<VelaFragancia> Fragancias { get; set; }


        // ------------------------------------- VELAFRAGANCIA ---------------------------------------------
        public void InsertarVelaFragancia(Guid idVela, Guid idFrag)
        {
            var vf = new VelaFragancia { IDVela = idVela, IDFrag = idFrag };

            context.VelaFragancia.Add(vf);
            context.SaveChanges();
        }

        public void EliminarRelacionesFragancias(Guid idVela)
        {
            var rels = context.VelaFragancia.Where(vf => vf.IDVela == idVela);
            context.VelaFragancia.RemoveRange(rels);
            context.SaveChanges();
        }

        public List<Fragancia> GetFraganciasPorVela(Guid idVela)
        {
            return context.VelaFragancia
                .Where(vf => vf.IDVela == idVela)
                .Select(vf => vf.Fragancia) // Asumiendo que tienes la navegación Fragancia en VelaFragancia
                .ToList();
        }
    }
}
