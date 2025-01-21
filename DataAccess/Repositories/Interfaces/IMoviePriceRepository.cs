using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface IMoviePriceRepository
    {
        Task<MoviePrice> GetByIdAsync(int id);
        Task<IEnumerable<MoviePrice>> GetAllAsync();
        Task AddAsync(MoviePrice moviePrice);
        Task UpdateAsync(MoviePrice moviePrice);
        Task DeleteAsync(int id);
    }
}
