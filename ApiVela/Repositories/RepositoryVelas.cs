using ApiVela.Data;
using ApiVela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVela.Repository
{
    public class RepositoryVelas
    {
        private readonly Contexto context;

        public RepositoryVelas(Contexto context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<VelaPigmento> Pigmentos { get; set; }
        public List<VelaFragancia> Fragancias { get; set; }


        // ------------------------------------- VELA ---------------------------------------------

        public List<Vela> GetVelas()
        {
            List<Vela> velas = this.context.Vela.ToList();
            return velas;
        }

        public Vela BuscarVela(Guid idVela)
        {
            return this.context.Vela.SingleOrDefault
                (x => x.IDVela == idVela);
        }

        public void InsertarVela(Vela vel)
        {
            Vela vela = new Vela();

            vela.IDVela = Guid.NewGuid();

            vela.VelaNombre = vel.VelaNombre;
            vela.Endurecedor = vel.Endurecedor;
            vela.FechaVenta = DateTime.Now;
            vela.FechaReal = DateTime.Now;
            vela.GradFrag = vel.GradFrag;
            vela.GradPig = vel.GradPig;
            vela.IDCera = vel.IDCera;
            vela.IDMecha = vel.IDMecha;
            vela.Coste = vel.Coste;
            vela.IDMolde = vel.IDMolde;
            vela.IDFrag = vel.IDFrag;
            vela.IDPedido = vel.IDPedido;
            vela.IDPig = vel.IDPig;
            vela.Observ = vel.Observ;

            Pigmentos = new List<VelaPigmento>();
            Fragancias = new List<VelaFragancia>();

            // Agregar pigmentos asociados
            foreach (var pigmento in vel.VelaPigmentos)
            {
                vela.VelaPigmentos.Add(new VelaPigmento
                {
                    IDVela = vela.IDVela,
                    IDPig = pigmento.IDPig,
                    Cantidad = pigmento.Cantidad,
                    Coste = pigmento.Coste
                });
            }

            // Agregar fragancias asociadas
            foreach (var fragancia in vel.VelaFragancias)
            {
                vela.VelaFragancias.Add(new VelaFragancia
                {
                    IDVela = vela.IDVela,
                    IDFrag = fragancia.IDFrag,
                    Cantidad = fragancia.Cantidad,
                    Coste = fragancia.Coste
                });
            }

            this.context.Vela.Add(vela);
            this.context.SaveChanges();
        }

        public void Actualizarvela(Vela vel)
        {
            Vela vela = BuscarVela(vel.IDVela);

            if (vela == null)
                throw new Exception("La vela no existe");

            //MIRAR SI LOS CAMPOS ANTIGUOS SON LOS MISMOS QUE LOS DATOS QUE VIENEN
            if (vel.Endurecedor != vela.Endurecedor)
            {
                vela.Endurecedor = vel.Endurecedor;

            }

            if (vel.GradFrag != vela.GradFrag)
            {
                vela.GradFrag = vel.GradFrag;
            }

            if (vel.GradPig != vela.GradPig)
            {
                vela.GradPig = vel.GradPig;
            }

            if (vel.FechaReal != vela.FechaReal)
            {
                vela.FechaReal = vel.FechaReal;
            }

            if (vel.FechaVenta != vela.FechaVenta)
            {
                vela.FechaVenta = vel.FechaVenta;
            }

            if (vel.IDCera != vela.IDCera)
            {
                vela.IDCera = vel.IDCera;
            }

            if (vel.IDMecha != vela.IDMecha)
            {
                vela.IDMecha = vel.IDMecha;
            }

            if (vel.Coste != vela.Coste)
            {
                vela.Coste = vel.Coste;
            }

            if (vel.IDFrag != vela.IDFrag)
            {
                vela.IDFrag = vel.IDFrag;
            }

            if (vel.IDPedido != vela.IDPedido)
            {
                vela.IDPedido = vel.IDPedido;

            }

            // === Actualizar Pigmentos ===
            // Eliminar pigmentos que ya no estén
            var pigmentosAEliminar = vela.VelaPigmentos
                .Where(p => !vela.VelaPigmentos.Any(np => np.IDPig == p.IDPig))
                .ToList();

            //foreach (var pEliminar in pigmentosAEliminar)
            //{
            //    this.context.VelaPigmento.Remove(pEliminar);
            //}

            // Agregar o actualizar pigmentos
            foreach (var pigNuevo in vela.VelaPigmentos)
            {
                var pigExistente = vela.VelaPigmentos
                    .FirstOrDefault(p => p.IDPig == pigNuevo.IDPig);

                if (pigExistente != null)
                {
                    // Actualizar propiedades
                    pigExistente.Cantidad = pigNuevo.Cantidad;
                    pigExistente.Coste = pigNuevo.Coste;
                }
                //else
                //{
                //    // Agregar pigmento nuevo
                //    pigNuevo.IDVela = vela.IDVela; // Asegurar FK
                //    this.context.VelaPigmento.Add(pigNuevo);
                //}
            }

            // === Actualizar Fragancias ===
            // Eliminar fragancias que ya no estén
            var fraganciasAEliminar = vela.VelaFragancias
                .Where(f => !vela.VelaFragancias.Any(nf => nf.IDFrag == f.IDFrag))
                .ToList();

            //foreach (var fEliminar in fraganciasAEliminar)
            //{
            //    this.context.VelaFragancia.Remove(fEliminar);
            //}

            // Agregar o actualizar fragancias
            foreach (var fragNuevo in vela.VelaFragancias)
            {
                var fragExistente = vela.VelaFragancias
                    .FirstOrDefault(f => f.IDFrag == fragNuevo.IDFrag);

                if (fragExistente != null)
                {
                    // Actualizar propiedades
                    fragExistente.Cantidad = fragNuevo.Cantidad;
                    fragExistente.Coste = fragNuevo.Coste;
                }
                //else
                //{
                //    // Agregar fragancia nueva
                //    fragNuevo.IDVela = vela.IDVela;
                //    this.context.VelaFragancia.Add(fragNuevo);
                //}
            }


            //this.context.SaveChanges();
        }

    }

}