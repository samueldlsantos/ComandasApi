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

            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDTO, Order>();

            CreateMap<OrderItem, OrderItemDTO>();
            CreateMap<OrderItemDTO, OrderItem>();

            CreateMap<OrderItemOption, OrderItemOptionDTO>();
            CreateMap<OrderItemOptionDTO, OrderItemOption>();

            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options));
            CreateMap<ProductDTO, Product>();

            CreateMap<ProductOption, ProductOptionDTO>()
                .ForMember(dest => dest.Values, opt => opt.MapFrom(src => src.Values));
            CreateMap<ProductOptionDTO, ProductOption>();

            CreateMap<OptionValue, OptionValueDTO>();
            CreateMap<OptionValueDTO, OptionValue>();

        }
    }
}