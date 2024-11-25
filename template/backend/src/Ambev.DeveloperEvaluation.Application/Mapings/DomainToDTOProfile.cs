using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Domain.Models.CartCase.Entities;
using Ambev.DeveloperEvaluation.Domain.Models.ProductCase.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Mapings
{
    public class DomainToDTOProfile : Profile
    {
        public DomainToDTOProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new ProductRatingDTO(src.Rating, src.Count)));
            //.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("yyyy-MM-dd")));


            CreateMap<Cart, CartDTO>()
                //.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems)); // Mapeia os CartItems corretamente


            CreateMap<CartItem, CartItemDTO>().ReverseMap();

        }
    }
}
