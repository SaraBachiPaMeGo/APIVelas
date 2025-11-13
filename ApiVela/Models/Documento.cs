using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVela.Models
{
    [Table("Documento")]

    public class Documento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDDoc")] public Guid IDDoc { get; set; }

        [Column("IDVela")] 
        public Guid IDVela { get; set; }

        [Column("IDPack")]
        public Guid IDPack { get; set; }

        [Column("IDEndurecedor")]
        public Guid IDEndurecedor { get; set; }

        [Column("IDMecha")] 
        public Guid IDMecha { get; set; }

        [Column("IDCera")]
        public Guid IDCera { get; set; }

        [Column("IDFrag")] 
        public Guid IDFrag { get; set; }

        [Column("IDPig")] 
        public Guid IDPig { get; set; }

        [Column("IDMolde")] 
        public Guid IDMolde { get; set; }

        [Column("IDPedido")] 
        public Guid IDPedido { get; set; }

        [ForeignKey(nameof(IDPedido))]
        public Pedido pedido { get; set; }

        [ForeignKey(nameof(IDVela))]
     
        public Vela Vela { get; set; }

        [ForeignKey(nameof(IDPack))]
         public Pack pack { get; set; }

        [ForeignKey(nameof(IDEndurecedor))]
         public Endurecedor endurecedor { get; set; }

        [ForeignKey(nameof(IDMecha))]
         public Mecha mecha { get; set; }

        [ForeignKey(nameof(IDCera))]
         public Cera cera { get; set; }

        [ForeignKey(nameof(IDFrag))]
         public Fragancia fragancia { get; set; }

        [ForeignKey(nameof(IDPig))]
         public Pigmento pigmento { get; set; } 
        
        [ForeignKey(nameof(IDMolde))]
         public Molde molde { get; set; }

        [Column("NombreDoc")]
         public string NombreDoc { get; set; }

        [Column("TipoDoc")]
        public string TipoDoc { get; set; }

        [Column("Ruta")]
        public string? Ruta { get; set; }

        [Column("Datos")]
          public byte[]? Datos { get; set; }

          public DateTime FechaSubida { get; set; }
    }

}
