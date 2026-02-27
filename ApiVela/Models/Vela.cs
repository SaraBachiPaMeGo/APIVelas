using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVela.Models
{
    [Table("Vela")]

    public class Vela
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDVela")]
        public Guid IDVela { get; set; }

        //[Column("IDVelaFin")]
        //public Guid IDVelaFin { get; set; }

        [Column("VelaNombre")]
        public string VelaNombre { get; set; }

        [Column("Observ")]
        public string Observ { get; set; }

        [Column("IDEndurecedor")]
        public Guid? IDEndurecedor { get; set; }

        [Column("FechaReal")]
        public DateTime FechaReal { get; set; }

        [Column("GradFrag")]
        public decimal? GradFrag { get; set; }

        [Column("GradEnd")]
        public decimal? GradEnd { get; set; }

        [Column("GradPig")]
        public decimal? GradPig { get; set; }

        //[Column("IDFrag")]
        //public Guid? IDFrag { get; set; }

        [Column("IDMolde")]
        public Guid? IDMolde { get; set; }

        //[Column("IDPig")]
        //public Guid? IDPig { get; set; }
        
        [Column("Coste")]
        public decimal? Coste { get; set; }

        [Column("IDMecha")]
        public Guid IDMecha { get; set; }

        [Column("IDCera")]
        public Guid IDCera { get; set; }

        [Column("CantidadCera")]
        public decimal? CantidadCera { get; set; }

        [Column("CantidadMecha")]
        public decimal? CantidadMecha { get; set; }

        [Column("CantidadFrag")]
        public decimal? CantidadFrag { get; set; }

        [Column("CantidadPig")]
        public decimal? CantidadPig { get; set; }

        [Column("CantidadEnd")]
        public decimal? CantidadEnd { get; set; }

        //[ForeignKey(nameof(IDVelaFin))]
        //public virtual VelaFinalizada? VelaFinalizada { get; set; }


        [Column("Image")]
        public byte[] Image { get; set; }          // 🔥 BYTES

        [Column("ImagenContentType")]
        public string ImagenContentType { get; set; } // opcional (muy recomendable)

        
        public List<VelaPigmento> VelaPigmentos { get; set; }

        public List<VelaFragancia> VelaFragancias { get; set; }
    }
}