namespace Booking.Shared.Repositories.Interfaces;

public interface IDynamicRepository<T> where T: IEntity
{
    Task<T> GetAsync(string id, string accountId);
    Task<IEnumerable<DynamicEntity>> GetAllAsync(string accountId);
    Task<string> SetAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(string id, string accountId);
}
