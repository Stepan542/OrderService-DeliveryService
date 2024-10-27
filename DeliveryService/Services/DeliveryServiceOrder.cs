using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryService.Interfaces;
using Shared.Models;

namespace DeliveryService.Services
{
    public class DeliveryServiceOrder : IDeliveryService
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public DeliveryServiceOrder(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public async Task CreateDeliveryAsync(Order order)
        {
            await _deliveryRepository.CreateAsync(order);
        }
    }
}