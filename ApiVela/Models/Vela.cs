using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiVela.Models
{
    [Table("Vela")]

    public class Vela
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDVela")]
        public Guid IDVela { get; set; }

        [Column("VelaNombre")]
        public string VelaNombre { get; set; }

        [Column("Observ")]
        public string Observ { get; set; }

        [Column("IDEndurecedor")]
        public Guid? IDEndurecedor { get; set; }

        [Column("FechaReal")]
        public DateTime FechaReal { get; set; }

        [Column("IDMolde")]
        public Guid? IDMolde { get; set; }
                
        [Column("Coste")]
        public decimal? Coste { get; set; }

        [Column("IDCera")]
        public Guid IDCera { get; set; }

        [Column("CantidadCera")]
        public decimal? CantidadCera { get; set; }

        [Column("CantidadEnd")]
        public decimal? CantidadEnd { get; set; }

        [Column("Image")]
        public byte[] Image { get; set; }          // 🔥 BYTES

        [Column("ImagenContentType")]
        public string ImagenContentType { get; set; } // opcional (muy recomendable)

        [NotMapped]
        [JsonIgnore]
        public List<VelaPigmento> VelaPigmentos { get; set; }

        [NotMapped]
        [JsonIgnore]
        public List<VelaFragancia> VelaFragancias { get; set; }
    }
}