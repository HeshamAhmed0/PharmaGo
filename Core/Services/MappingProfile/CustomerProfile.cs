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
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
      
                // CustomerRequestDto → Customer
                CreateMap<CustomerRequestDto, Customer>()
                    .ForPath(d => d.Address.Street, opt => opt.MapFrom(src => src.Street))
                    .ForPath(d => d.Address.City, opt => opt.MapFrom(src => src.City))
                    .ForPath(d => d.Address.Country, opt => opt.MapFrom(src => src.Country));

                // Customer → CustomerRequestDto
                CreateMap<Customer, CustomerRequestDto>()
                    .ForMember(d => d.Street, opt => opt.MapFrom(src => src.Address.Street))
                    .ForMember(d => d.City, opt => opt.MapFrom(src => src.Address.City))
                    .ForMember(d => d.Country, opt => opt.MapFrom(src => src.Address.Country));

                // CustomerResultDto → Customer
                CreateMap<CustomerResultDto, Customer>()
                    .ForPath(d => d.Address.Street, opt => opt.MapFrom(src => src.Street))
                    .ForPath(d => d.Address.City, opt => opt.MapFrom(src => src.City))
                    .ForPath(d => d.Address.Country, opt => opt.MapFrom(src => src.Country));

                // Customer → CustomerResultDto
                CreateMap<Customer, CustomerResultDto>()
                    .ForMember(d => d.Street, opt => opt.MapFrom(src => src.Address.Street))
                    .ForMember(d => d.City, opt => opt.MapFrom(src => src.Address.City))
                    .ForMember(d => d.Country, opt => opt.MapFrom(src => src.Address.Country));
      

    }
}
}
