using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVela.Models.DTO
{
    public class VelaPigmentoDTO
    {
        public Guid? IDPig { get; set; }
        public string NombrePigmento { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? Coste { get; set; }
    }
}
