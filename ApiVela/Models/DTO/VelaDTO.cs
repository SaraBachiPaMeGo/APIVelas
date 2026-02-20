using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVela.Models.DTO
{
    public class VelaDTO
    {
        public Guid IDVela { get; set; }


        public string VelaNombre { get; set; }

        public DateTime FechaReal { get; set; }

        public decimal? Coste { get; set; }

        public string NombreCera { get; set; }

        public decimal? CantidadCera { get; set; }

        public decimal? CantidadMecha { get; set; }

        public decimal? CantidadEnd { get; set; }

        //public List<Documento>? Documentos { get; set; }

        public byte[] Image { get; set; }          // 🔥 BYTES

        public string ImagenContentType { get; set; } // opcional (muy recomendable)

        public List<VelaPigmentoDTO> VelaPigmentos { get; set; }

        public List<VelaFraganciaDTO> VelaFragancias { get; set; }

    }
}
