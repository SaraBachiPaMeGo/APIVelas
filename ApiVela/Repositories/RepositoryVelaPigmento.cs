using ApiVela.Data;
using ApiVela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVela.Repositories
{
    public class RepositoryVelaPigmento
    {
        private readonly Contexto context;

        public RepositoryVelaPigmento(Contexto context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<VelaPigmento> Pigmentos { get; set; }

        // ------------------------------------- VELAPIGMENTO ---------------------------------------------

        public void InsertarVelaPigmento(Guid idVela, Guid idPig)
        {
            var vp = new VelaPigmento { IDVela = idVela, IDPig = idPig };
            //context.VelaPigmento.Add(vp);
            //context.SaveChanges();
        }

        public void EliminarRelacionesPigmentos(Guid idVela)
        {
            //var rels = context.VelaPigmento.Where(vp => vp.IDVela == idVela);
            //context.VelaPigmento.RemoveRange(rels);
            context.SaveChanges();
        }

        public List<Pigmento> GetPigmentosPorVela(Guid idVela)
        {
            return context.VelaPigmento
                .Where(vp => vp.IDVela == idVela)
                .Select(vp => vp.Pigmento)  // Asumiendo que tienes la propiedad de navegación Pigmento en VelaPigmento
                .ToList();
        }
    }
}
