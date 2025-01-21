using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories
{
    public class MoviePriceRepository : IMoviePriceRepository
    {
        private readonly ApplicationDbContext _context;

        public MoviePriceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MoviePrice> GetByIdAsync(int id)
        {
            return await _context.MoviePrices
                .Include(mp => mp.Movie)
                .FirstOrDefaultAsync(mp => mp.Id == id);
        }

        public async Task<IEnumerable<MoviePrice>> GetAllAsync()
        {
            return await _context.MoviePrices
                .Include(mp => mp.Movie)
                .ToListAsync();
        }

        public async Task AddAsync(MoviePrice moviePrice)
        {
            await _context.MoviePrices.AddAsync(moviePrice);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MoviePrice moviePrice)
        {
            _context.MoviePrices.Update(moviePrice);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var moviePrice = await _context.MoviePrices.FindAsync(id);
            if (moviePrice != null)
            {
                _context.MoviePrices.Remove(moviePrice);
                await _context.SaveChangesAsync();
            }
        }
    }
}
