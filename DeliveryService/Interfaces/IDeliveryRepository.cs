using Shared.Models;

namespace DeliveryService.Interfaces
{
    public interface IDeliveryRepository
    {
        Task CreateAsync(Order order);
        Task<Order?> GetByIdAsync(int id);
    }
}