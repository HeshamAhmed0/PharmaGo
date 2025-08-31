using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Moduls.BasketModels;
using Shared.MedulesDto.BasketDtos;

namespace Services.MappingProfile
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketItem,BasketItemDto>();
            CreateMap<CustomerBasket, BasketResponseDto > ()
                     .ForMember(dest => dest.Items, Org => Org.MapFrom(src => src.Items));
        }
    }
}
