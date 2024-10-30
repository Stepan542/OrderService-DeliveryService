using AutoMapper;
using Shared.Interfaces;
using Shared.Models;

namespace DeliveryService.Mappers
{
    public class DeliveryMappingProfile : Profile
    {
        public DeliveryMappingProfile()
        {
            CreateMap<IOrderForDelivery, Order>();
        }
    }
}