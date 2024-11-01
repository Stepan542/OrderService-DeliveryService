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

        public async Task CreateAsync(Order order)
        {
            await _deliveryRepository.CreateAsync(order);
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _deliveryRepository.GetByIdAsync(id);
        }
    }
}