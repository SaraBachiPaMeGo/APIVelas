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
        public Guid? IDVela { get; set; }

        [Column("VelaNombre")]
        public string VelaNombre { get; set; }

        [Column("Image")]
        public string Image { get; set; }

        [Column("Observ")]
        public string Observ { get; set; }

        [Column("Endurecedor")]
        public bool Endurecedor { get; set; }

        [Column("FechaReal")]
        public DateTime FechaReal { get; set; }

        [Column("FechaVenta")]
        public DateTime FechaVenta { get; set; }

        [Column("GradFrag")]
        public decimal? GradFrag { get; set; }

        [Column("GradPig")]
        public decimal? GradPig { get; set; }

        [Column("IDFrag")]
        public Guid? IDFrag { get; set; }

        [Column("IDMolde")]
        public Guid? IDMolde { get; set; }

        [Column("IDPig")]
        public Guid? IDPig { get; set; }

        [Column("Coste")]
        public decimal? Coste { get; set; }

        [Column("IDPedido")]
        public Guid? IDPedido { get; set; }

        [Column("IDMecha")]
        public Guid? IDMecha { get; set; }

        [Column("IDCera")]
        public Guid? IDCera { get; set; }

        public List<VelaPigmento> VelaPigmentos { get; set; }
        public List<VelaFragancia> VelaFragancias { get; set; }
    }
}