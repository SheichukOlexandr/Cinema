using DataAccess.Contexts;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

public class GenreService
{
    private readonly ApplicationDbContext _context;

    public GenreService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Genre>> GetGenresAsync()
    {
        return await _context.Genres.ToListAsync();
    }
}
