using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVela.Profile
{
    using ApiVela.Models;
    using ApiVela.Models.DTO;
    using AutoMapper;
    using NPOI.SS.Formula.Functions;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // 🕯️ Cera
            CreateMap<Cera, CustomApiResponse<Cera>>();
            CreateMap<List<Cera>, CustomApiResponse<List<Cera>>>();

            // 👤 Cliente
            CreateMap<Cliente, CustomApiResponse<Cliente>>();
            CreateMap<List<Cliente>, CustomApiResponse<List<Cliente>>>();

            // 💪 Endurecedor
            CreateMap<Endurecedor, CustomApiResponse<Endurecedor>>();
            CreateMap<List<Endurecedor>, CustomApiResponse<List<Endurecedor>>>();

            // 🌸 Fragancia
            CreateMap<Fragancia, CustomApiResponse<Fragancia>>();
            CreateMap<List<Fragancia>, CustomApiResponse<List<Fragancia>>>();

            // 🧵 Mecha
            CreateMap<Mecha, CustomApiResponse<Mecha>>();
            CreateMap<List<Mecha>, CustomApiResponse<List<Mecha>>>();

            // 🧩 Molde
            CreateMap<Molde, CustomApiResponse<Molde>>();
            CreateMap<List<Molde>, CustomApiResponse<List<Molde>>>();

            // 📦 Pack
            CreateMap<Pack, CustomApiResponse<Pack>>();
            CreateMap<List<Pack>, CustomApiResponse<List<Pack>>>();

            // 🧾 Pedido
            CreateMap<Pedido, CustomApiResponse<Pedido>>();
            CreateMap<List<Pedido>, CustomApiResponse<List<Pedido>>>();

            // 🎨 Pigmento
            CreateMap<Pigmento, CustomApiResponse<Pigmento>>();
            CreateMap<List<Pigmento>, CustomApiResponse<List<Pigmento>>>();

            // 🕯️ Vela
            CreateMap<Vela, CustomApiResponse<Vela>>();
            CreateMap<List<Vela>, CustomApiResponse<List<Vela>>>();

            // 🕯️ VelaDTO
            CreateMap<VelaDTO, CustomApiResponse<VelaDTO>>();
            CreateMap<List<VelaDTO>, CustomApiResponse<List<VelaDTO>>>();

            CreateMap<Vela, VelaDTO>();
            CreateMap<VelaDTO, Vela>();

            CreateMap<VelaPigmento, VelaPigmentoDTO>();
            CreateMap<VelaPigmentoDTO, VelaPigmento>();

            CreateMap<VelaFragancia, VelaFraganciaDTO>();
            CreateMap<VelaFraganciaDTO, VelaFragancia>();

            // 🕯️ Vela Fin
            CreateMap<VelaFinalizada, CustomApiResponse<VelaFinalizada>>();
            CreateMap<List<VelaFinalizada>, CustomApiResponse<List<VelaFinalizada>>>();

            // 🌺 VelaFragancia
            CreateMap<VelaFragancia, CustomApiResponse<VelaFragancia>>();
            CreateMap<List<VelaFragancia>, CustomApiResponse<List<VelaFragancia>>>();

            // 🎨 VelaPigmento
            CreateMap<VelaPigmento, CustomApiResponse<VelaPigmento>>();
            CreateMap<List<VelaPigmento>, CustomApiResponse<List<VelaPigmento>>>();

            CreateMap<Inventario, CustomApiResponse<Inventario>>();
            CreateMap<List<Inventario>, CustomApiResponse<List<Inventario>>>();

            // Puedes añadir más mapeos según tus modelos y DTOs
            CreateMap<Vela, VelaDTO>()
            .ForMember(
                dest => dest.NombreCera,
                opt => opt.MapFrom(src => src.Cera.Firma)
            );

            CreateMap<VelaFragancia, VelaFraganciaDTO>()
                .ForMember(dest => dest.NombreFragancia,
                           opt => opt.MapFrom(src => src.Fragancia.FragNombre));

            CreateMap<VelaPigmento, VelaPigmentoDTO>()
               .ForMember(dest => dest.NombrePigmento,
               opt => opt.MapFrom(src => src.Pigmento.ColorNombre));
        }
    }
}
