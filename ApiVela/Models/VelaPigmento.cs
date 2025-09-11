using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiVela.Models;

namespace ApiVela.Models
{
    [Table("VelaPigmento")]
    public class VelaPigmento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDVela")]
        public Guid IDVela { get; set; }

        [Column("Vela")]
        public Vela Vela { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDPig")]
        public Guid IDPig { get; set; }

        [Column("Pigmento")]
        public Pigmento Pigmento { get; set; }

        [Column("Cantidad")]
        public decimal? Cantidad { get; set; }

        [Column("Coste")]
        public decimal? Coste { get; set; }
    }
}