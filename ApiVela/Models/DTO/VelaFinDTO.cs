using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVela.Models.DTO
{
    public class VelaFinDTO
    {
        public Guid IDVelaFin { get; set; }

        public decimal? Coste { get; set; }
        public decimal? Beneficio { get; set; }
        public decimal? PVP { get; set; }

        public DateTime FechaFin { get; set; }

        public List<PackDTO> Packs { get; set; }
        public List<VelaDTO> Velas { get; set; }
        public PedidoDTO Pedido { get; set; }  // ✅ pedido opcional
    }

}
