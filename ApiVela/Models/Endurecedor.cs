using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVela.Models
{
    [Table("Endurecedor")]

    public class Endurecedor

    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDEndurecedor")]
        public Guid IDEndurecedor { get; set; }

        [Column("Firma")]
        public string Firma { get; set; }

        [Column("Tipo")]
        public string Tipo { get; set; }

        [Column("CompradoEn")]
        public string CompradoEn { get; set; }

        [Column("Cantidad")]
        public  decimal?  Cantidad { get; set; }

        [Column("Coste")]
        public  decimal?  Coste { get; set; }

        [Column("IDVela")]
        public Guid? IDVela { get; set; }

      

    }
}