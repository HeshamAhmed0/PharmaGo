using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Moduls;
using Shared.MedulesDto;

namespace Services.MappingProfile
{
    public class ProductProfile :Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductRequestDto>().ReverseMap();
            CreateMap<Product, ProductResultDto>().ReverseMap();
            CreateMap<ProductResultDto, Product>().ReverseMap();
        }
    }
}
