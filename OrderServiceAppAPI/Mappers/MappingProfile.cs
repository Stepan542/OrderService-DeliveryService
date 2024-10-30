using AutoMapper;
using OrderServiceAppAPI.Dto.Order;
using Shared.Models;

namespace OrderServiceAppAPI.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDto>();
        }
    }
}