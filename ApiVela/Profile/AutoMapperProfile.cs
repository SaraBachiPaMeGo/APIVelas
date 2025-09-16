using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVela.Profile
{
    using ApiVela.Models;
    using AutoMapper;
    using NPOI.SS.Formula.Functions;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Aquí defines tus mapeos
            CreateMap<Cera, CustomApiResponse<T>>();
            CreateMap<List<Cera>, CustomApiResponse<T>>();

            // Puedes añadir más mapeos según tus modelos y DTOs
        }
    }

}
