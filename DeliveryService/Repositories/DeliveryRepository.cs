using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryService.Data;
using DeliveryService.Interfaces;
using Shared.Models;

namespace DeliveryService.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly DeliveryDbContext _dbContext;

        public DeliveryRepository(DeliveryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Order order)
        {
            await _dbContext.OrderDeliveries.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}