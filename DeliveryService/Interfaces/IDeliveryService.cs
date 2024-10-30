using Shared.Models;

namespace DeliveryService.Interfaces
{
    public interface IDeliveryService
    {
        Task CreateAsync(Order order);
        Task<Order?> GetByIdAsync(int id);
    }
}