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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IDPedido")]
        public Guid IDPedido { get; set; }

        [Column("FechaPedi")]
        public DateTime FechaPedi { get; set; }

        [Column("FechaEntrega")]
        public DateTime FechaEntrega { get; set; }

        // ✅ Nuevo campo: indica si el pedido ha sido vendido
        [Column("Vendido")]
        public bool? Vendido { get; set; }

        // 🔗 Relación con Cliente (muchos pedidos -> un cliente)
        [ForeignKey(nameof(Cliente))]
        [Column("IDCliente")]
        public Guid IDCliente { get; set; }

        public virtual Cliente Cliente { get; set; }

        // 🔗 Relación con Velas (un pedido -> muchas velas)
        public virtual ICollection<Vela> Velas { get; set; } = new List<Vela>();
        public ICollection<Documento>? Documentos { get; set; }
    }
}