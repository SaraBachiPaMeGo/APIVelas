using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVela.Models.DTO
{
    public class PackDTO
    {
        public Guid? IDPack { get; set; }
        public string Tipo { get; set; }
        public string Firma { get; set; }
        public decimal? Coste { get; set; }
    }
}
