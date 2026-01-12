using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVela.Models
{
    [Table("Inventario")]

    public class Inventario
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDInventario")]
        public Guid? IDInventario { get; set; }

        [Column("Firma")]
        public string Firma { get; set; }

        [Column("Tipo")]
        public string Tipo { get; set; }

        [Column("CompradoEn")]
        public string CompradoEn { get; set; }

        [Column("Cantidad")]
        public int? Cantidad { get; set; }

        [Column("Coste")]
        public decimal? Coste { get; set; }
    }
}
