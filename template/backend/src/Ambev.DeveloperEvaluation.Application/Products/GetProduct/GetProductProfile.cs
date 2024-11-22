using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Models.ProductCase.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    public class GetProductProfile : Profile
    {
        public GetProductProfile() 
        {
            CreateMap<Product, GetProductResult>();
        }
    }
}
