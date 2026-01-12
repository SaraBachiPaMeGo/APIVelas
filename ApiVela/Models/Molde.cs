using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVela.Models
{
    [Table("Molde")]

    public class Molde
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDMolde")]
        public Guid? IDMolde { get; set; }

        [Column("MoldeNombre")]
        public string MoldeNombre { get; set; }

        [Column("Firma")]
        public string Firma { get; set; }

        [Column("Tipo")]
        public string Tipo { get; set; }

        [Column("CompradoEn")]
        public string CompradoEn { get; set; }

        [Column("GramCera")]
        public decimal? GramCera { get; set; }

        [Column("Medidas")]
        public string Medidas { get; set; }

        [Column("Duracion")]
        public decimal? Duracion { get; set; }

        [Column("CMMecha")]
        public decimal? CMMecha { get; set; }

        [Column("Observ")]
        public string Observ { get; set; }

        [Column("MilAgua")]
        public decimal? MilAgua { get; set; }

        [Column("IDVela")]
        public Guid? IDVela { get; set; }

        [Column("Coste")]
        public decimal? Coste { get; set; }

        [Column("Cantidad")]
        public int? Cantidad { get; set; }


    }
}