using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVela.Models
{
    [Table("VelaFinalizada")]

    public class VelaFinalizada
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDVelaFin")]
        public Guid IDVelaFin { get; set; }

        [Column("Coste")]
        public decimal? Coste { get; set; }

        [Column("Beneficio")]
        public decimal? Beneficio { get; set; }

        [Column("PVP")]
        public decimal? PVP { get; set; }

        [Column("IDVela")]
        public Guid? IDVela { get; set; }

        [Column("IDPack")]
        public Guid? IDPack { get; set; }

        [Column("IDPedido")]
        public Guid? IDPedido { get; set; }

        [Column("FechaFin")]
        public DateTime FechaFin { get; set; }

        [ForeignKey(nameof(IDPack))]

        public virtual List<Pack> Packs { get; set; }

        [ForeignKey(nameof(IDVela))]

        public virtual List<Vela> Velas{ get; set; }

        [ForeignKey(nameof(IDPedido))]
        public virtual Pedido Pedido { get; set; }

    }
}
