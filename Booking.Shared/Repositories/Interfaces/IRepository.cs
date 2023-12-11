using Booking.Shared.Entities.Interfaces;
using Google.Cloud.Firestore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Booking.Shared.Repositories.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        Task<T> GetAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<string> AddAsync(T entity);
        Task<string> SetAsync(T entity);
        Task VerifyEmail(string email);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id);
    }
}
