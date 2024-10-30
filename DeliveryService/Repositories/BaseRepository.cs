using DeliveryService.Data;
using DeliveryService.Interfaces;

namespace DeliveryService.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T: class
    {
        protected readonly DeliveryDbContext _dbContext;

        public BaseRepository(DeliveryDbContext dbContex)
        {
            _dbContext = dbContex;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}