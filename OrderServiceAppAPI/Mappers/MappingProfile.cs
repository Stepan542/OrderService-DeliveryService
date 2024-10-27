using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OrderServiceAppAPI.Dto.Order;
using Shared.Models;

namespace OrderServiceAppAPI.Mappers
{
    public class MappingProfile : Profile
    {
        // AutoMapper
        public MappingProfile()
        {
            CreateMap<Order, OrderDto>();
        }
    }
}