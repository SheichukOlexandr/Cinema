using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface ISeatRepository
    {
        Task<Seat> GetByIdAsync(int id);
        Task<IEnumerable<Seat>> GetAllAsync();
        Task AddAsync(Seat seat);
        Task UpdateAsync(Seat seat);
        Task DeleteAsync(int id);
    }
}
