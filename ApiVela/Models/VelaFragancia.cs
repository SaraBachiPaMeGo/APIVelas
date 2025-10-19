using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVela.Models
{
    [Table("VelaFragancia")]
    public class VelaFragancia
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDVela")]
        public Guid? IDVela { get; set; }

        [Column("Vela")]
        public Vela Vela { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDFrag")]
        public Guid? IDFrag { get; set; }

        [Column("Fragancia")]
        public Fragancia Fragancia { get; set; }

        [Column("Cantidad")]
        public decimal? Cantidad { get; set; }

        [Column("Coste")]
        public decimal? Coste { get; set; }
    }
}