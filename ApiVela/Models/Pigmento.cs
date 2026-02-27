using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVela.Models
{
    [Table("Pigmento")]

    public class Pigmento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDPig")]
        public Guid IDPig { get; set; }

        [Column("Firma")]
        public string Firma { get; set; }

        [Column("Tipo")]
        public string Tipo { get; set; }

        [Column("ColorNombre")]
        public string ColorNombre { get; set; }

        [Column("CompradoEn")]
        public string CompradoEn { get; set; }

        [Column("IDVela")]
        public Guid? IDVela { get; set; }

        [Column("Cantidad")]
        public decimal?  Cantidad { get; set; }

        [Column("Coste")]
        public decimal?  Coste { get; set; }

        public List<VelaPigmento> VelaPigmento { get; set; }
    }
}