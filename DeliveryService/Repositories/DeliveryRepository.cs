using DeliveryService.Data;
using DeliveryService.Interfaces;
using Shared.Models;

namespace DeliveryService.Repositories
{
    public class DeliveryRepository : BaseRepository<Order>, IDeliveryRepository
    {
        // нужно ли писать crud для сервисов?
        public DeliveryRepository(DeliveryDbContext contex) : base(contex) {}
    }
}