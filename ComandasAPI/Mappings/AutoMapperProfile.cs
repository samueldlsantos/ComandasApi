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
            CreateMap<User, UserDTO>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
            CreateMap<UserDTO, User>();

            CreateMap<Role, RoleDTO>();
            CreateMap<RoleDTO, Role>();

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