using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVela.Models
{
    [Table("Pack")]

    public class Pack
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDPack")]
        public Guid? IDPack { get; set; }

        [Column("Firma")]
        public string Firma { get; set; }

        [Column("Tipo")]
        public string Tipo { get; set; }

        [Column("CompradoEn")]
        public string CompradoEn { get; set; }

        [Column("Cantidad")]
        public decimal? Cantidad { get; set; }

        [Column("Coste")]
        public decimal? Coste { get; set; }

        [Column("IDVela")]
        public Guid? IDVela { get; set; }
      


        // FK opcional hacia VelaFinalizada
        [Column("IDVelaFin")]
        public Guid? IDVelaFin { get; set; }

        [ForeignKey(nameof(IDVelaFin))]
        public virtual VelaFinalizada? VelaFinalizada { get; set; }

    }
}
