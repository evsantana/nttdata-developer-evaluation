﻿using Ambev.DeveloperEvaluation.Application.DTOs;
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
        }
    }
}