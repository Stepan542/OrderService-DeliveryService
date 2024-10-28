using Shared.Models;

namespace DeliveryService.Interfaces
{
    public interface IDeliveryService
    {
        Task CreateDeliveryAsync(Order order);
        Task<Order?> GetDeliveryByIdAsync(int id);
    }
}