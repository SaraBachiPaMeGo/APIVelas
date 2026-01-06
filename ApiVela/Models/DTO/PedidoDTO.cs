using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVela.Models.DTO
{
    public class PedidoDTO
    {
        public Guid IDPedido { get; set; }
        public DateTime FechaPedi { get; set; }
        public DateTime FechaEntrega { get; set; }
        public bool? Vendido { get; set; }
    }

}
