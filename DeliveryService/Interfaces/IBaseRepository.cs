namespace DeliveryService.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task<T?> GetByIdAsync(int id);
    }
}