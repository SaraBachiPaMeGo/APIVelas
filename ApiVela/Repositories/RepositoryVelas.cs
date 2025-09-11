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

             vela.IDVela =Guid.NewGuid();
            
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
            foreach (var pigmento in vel.Pigmentos)
            {
                vela.Pigmentos.Add(new VelaPigmento
                {
                    IDVela = vela.IDVela,
                    IDPig = pigmento.IDPig,
                    Cantidad = pigmento.Cantidad,
                    Coste = pigmento.Coste
                });
            }

            // Agregar fragancias asociadas
            foreach (var fragancia in vel.Fragancias)
            {
                vela.Fragancias.Add(new VelaFragancia
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
            var pigmentosAEliminar = vela.Pigmentos
                .Where(p => !vela.Pigmentos.Any(np => np.IDPig == p.IDPig))
                .ToList();

            //foreach (var pEliminar in pigmentosAEliminar)
            //{
            //    this.context.VelaPigmento.Remove(pEliminar);
            //}

            // Agregar o actualizar pigmentos
            foreach (var pigNuevo in vela.Pigmentos)
            {
                var pigExistente = vela.Pigmentos
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
            var fraganciasAEliminar = vela.Fragancias
                .Where(f => !vela.Fragancias.Any(nf => nf.IDFrag == f.IDFrag))
                .ToList();

            //foreach (var fEliminar in fraganciasAEliminar)
            //{
            //    this.context.VelaFragancia.Remove(fEliminar);
            //}

            // Agregar o actualizar fragancias
            foreach (var fragNuevo in vela.Fragancias)
            {
                var fragExistente = vela.Fragancias
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

        // ------------------------------------- VELAFRAGANCIA ---------------------------------------------
        public void InsertarVelaFragancia(Guid idVela, Guid idFrag)
        {
            var vf = new VelaFragancia { IDVela = idVela, IDFrag = idFrag };

            //context.VelaFragancia.Add(vf);
           // context.SaveChanges();
        }

        public void EliminarRelacionesFragancias(Guid idVela)
        {
            //var rels = context.VelaFragancia.Where(vf => vf.IDVela == idVela);
            //context.VelaFragancia.RemoveRange(rels);
            //context.SaveChanges();
        }

        //public List<Fragancia> GetFraganciasPorVela(Guid idVela)
        //{
        //    return context.VelaFragancia
        //        .Where(vf => vf.IDVela == idVela)
        //        .Select(vf => vf.Fragancia) // Asumiendo que tienes la navegación Fragancia en VelaFragancia
        //        .ToList();
        //}
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
            //context.SaveChanges();
        }

        //public List<Pigmento> GetPigmentosPorVela(Guid idVela)
        //{
        //    //return context.VelaPigmento
        //    //    .Where(vp => vp.IDVela == idVela)
        //    //    .Select(vp => vp.Pigmento)  // Asumiendo que tienes la propiedad de navegación Pigmento en VelaPigmento
        //    //    .ToList();
        //}
                      
        // ------------------------------------- COSTES ---------------------------------------------
        //public void InsertarCoste(float tiempoprop, float horasLuz, float costeLuz, 
        //    float costeTarj, float costeFrag, float costeMecha, float costePack, float costeCera,
        //    float costeMolde, float costeVela, 
        //    int idPig, int idFrag, int iDVela, int idMolde, int idMecha)
        //{
        //    Costes coste = new Costes();

        //    int? count = (from datos in context.Costes
        //                  select datos.IDMecha).Count();

        //    if (count == 0)
        //    {
        //        //coste.Coste = Guid.NewGuid();
        //    }
        //    else
        //    {
        //        //Error
        //    }

        //    coste.TiempoProp = tiempoprop;
        //    coste.HorasLuz = horasLuz;
        //    coste.CosteLuz = costeLuz;
        //    coste.CosteTarj = costeTarj;
        //    coste.CosteFrag = costeFrag;
        //    coste.CosteMecha = costeMecha;
        //    coste.CostePack = costePack;
        //    coste.CosteCera = costeCera;
        //    coste.IDPig = idPig;
        //    coste.IDFrag = idFrag;
        //    coste.IDMolde = idMolde;
        //    coste.IDMecha = idMecha;
        //    coste.CosteMolde = costeMolde;
        //    coste.CosteVela = costeVela;            
        //    coste.IDVela = iDVela;

        //    this.context.Costes.Add(coste);
        //    this.context.SaveChanges();
        //}

        //public void ActualizarCoste(int idCoste, float tiempoprop, float horasLuz, float costeLuz,
        //    float costeTarj, float costeFrag, float costeMecha, float costePack, float costeCera,
        //    float costeMolde, float costeVela,
        //    int idPig, int idFrag, int iDVela, int idMolde, int idMecha)
        //{
        //    Costes coste = BuscarCoste(idCoste);

        //    if (tiempoprop != null)
        //    {
        //        coste.TiempoProp = tiempoprop;
        //    }

        //    if (horasLuz != null)
        //    {
        //        coste.HorasLuz = horasLuz;
        //    }
        //    coste.CosteCera = costeCera;

        //    if (costeLuz != null)
        //    {
        //        coste.CosteLuz = costeLuz;
        //    }

        //    if (costeTarj != null)
        //    {
        //        coste.CosteTarj = costeTarj;
        //    }

        //    if (costeFrag != null)
        //    {
        //        coste.CosteFrag = costeFrag;
        //    }

        //    if (costeMecha != null)
        //    {
        //        coste.CosteMecha = costeMecha;
        //    }

        //    if (costePack != null)
        //    {
        //        coste.CostePack = costePack;
        //    }

        //    if (idPig != null)
        //    {
        //        coste.IDPig = idPig;
        //    }

        //    if (idFrag != null)
        //    {
        //        coste.IDFrag = idFrag;
        //    }

        //    if (idMolde != null)
        //    {
        //        coste.IDMolde = idMolde;
        //    }

        //    if (idMecha != null)
        //    {
        //        coste.IDMecha = idMecha;
        //    }


        //    if (costeMolde != null)
        //    {
        //        coste.CosteMolde = costeMolde;
        //    }

        //    if (costeVela != null)
        //    {
        //        coste.CosteVela = costeVela;
        //    }

        //    if (iDVela != null)
        //    {
        //        coste.IDVela = iDVela;
        //    }

        //    this.context.SaveChanges();
        //}

        //public List<Costes> GetCostes()
        //{
        //    return this.context.Costes.ToList();
        //}

        //public Costes BuscarCoste(int idCoste)
        //{
        //    return this.context.Costes.SingleOrDefault
        //        (x => x.IDCoste == idCoste);
        //}


    }
}
