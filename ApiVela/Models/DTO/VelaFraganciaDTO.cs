using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVela.Models.DTO
{
    public class VelaFraganciaDTO
    {
        public Guid? IDFrag { get; set; }
        public string NombreFragancia { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? Coste { get; set; }
        
    }
}
