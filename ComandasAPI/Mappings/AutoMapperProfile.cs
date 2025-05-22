using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ComandasAPI.DTO;
using ComandasAPI.Models;
namespace ComandasAPI.Mappings
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options));

            CreateMap<ProductOption, ProductOptionDTO>()
                .ForMember(dest => dest.Values, opt => opt.MapFrom(src => src.Values));

            CreateMap<OptionValue, OptionValueDTO>();

            // Mapeos inversos si planeas recibir datos del cliente y actualizar modelos
            CreateMap<ProductDTO, Product>();
            CreateMap<ProductOptionDTO, ProductOption>();
            CreateMap<OptionValueDTO, OptionValue>();
        }
    }
}