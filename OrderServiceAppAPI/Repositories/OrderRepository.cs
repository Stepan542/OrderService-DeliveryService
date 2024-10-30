using Microsoft.EntityFrameworkCore;
using OrderServiceAppAPI.Data;
using OrderServiceAppAPI.Interfaces;
using Shared.Models;

namespace OrderServiceAppAPI.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext dbContext) 
            : base(dbContext) {}

        // private readonly ApplicationDbContext _dbContext;

        // public OrderRepository(ApplicationDbContext dbContext)
        // {
        //     _dbContext = dbContext;
        // }

        // public async Task<IEnumerable<Order>> GetAllAsync() 
        // {
        //     return await _dbContext.Orders.ToListAsync();
        // }

        // public async Task<Order?> GetByIdAsync(int id)
        // {
        //     return await _dbContext.Orders.FindAsync(id);
        // }

        // public async Task<Order> CreateAsync(Order order)
        // {
        //     _dbContext.Orders.Add(order);
        //     await _dbContext.SaveChangesAsync();
        //     return order;
        // }

        // public async Task UpdateAsync(Order order)
        // {
        //     _dbContext.Entry(order).State = EntityState.Modified;
        //     await _dbContext.SaveChangesAsync();
        // }

        // public async Task DeleteAsync(int id)
        // {
        //     var order = await _dbContext.Orders.FindAsync(id);

        //     if (order != null)
        //     {
        //         _dbContext.Orders.Remove(order);
        //         await _dbContext.SaveChangesAsync();
        //     }
        // }
    }
}