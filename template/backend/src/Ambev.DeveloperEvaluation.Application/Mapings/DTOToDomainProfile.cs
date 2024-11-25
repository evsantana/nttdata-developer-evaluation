using Ambev.DeveloperEvaluation.Application.Carts.Command;
using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Application.Products.Command;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Mapings
{
    public class DTOToDomainProfile : Profile
    {
        public DTOToDomainProfile()
        {
            CreateMap<ProductDTO, CreateProductCommand>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating.Rate))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Rating.Count));

            CreateMap<ProductDTO, UpdateProductCommand>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating.Rate))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Rating.Count));

            CreateMap<CartDTO, CreateCartCommand>()
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems));

            CreateMap<CartDTO, UpdateCartCommand>()
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems));

            //CreateMap<CartItemDTO, CartItem>()
            //    .ForMember(dest => dest.CartId, opt => opt.Ignore());

        }
    }
}
