using DataAccess.Contexts;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;


public class MovieService
{
    private readonly ApplicationDbContext _context;

    public MovieService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MovieDto>> GetMoviesAsync()
    {
        var movies = await _context.Movies.Include(m => m.Genre).ToListAsync();
        return movies.Select(m => new MovieDto
        {
            Id = m.Id,
            Title = m.Title,
            Director = m.Director,
            Duration = m.Duration,
            Cast = m.Cast,
            GenreId = m.GenreId,
            ReleaseDate = m.ReleaseDate,
            Description = m.Description,
            MinAge = m.MinAge,
            Rating = m.Rating,
            StatusId = m.StatusId,
            PosterURL = m.PosterURL,
            TrailerURL = m.TrailerURL
        }).ToList();
    }

    public async Task AddMovieAsync(Movie movie)
    {
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();
    }
}