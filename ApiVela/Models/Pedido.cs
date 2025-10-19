using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVela.Models
{
    [Table("Pedido")]

    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDPedido")]
        public Guid? IDPedido { get; set; }

        [Column("FechaPedi")]
        public DateTime FechaPedi { get; set; }

        [Column("FechaEntrega")]
        public DateTime FechaEntrega { get; set; }

        [Column("IDVela")]
        public Guid? IDVela { get; set; }

        [Column("IDCliente")]
        public Guid IDCliente { get; set; }
    }
}