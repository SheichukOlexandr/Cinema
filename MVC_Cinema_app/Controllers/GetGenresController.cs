using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MVC_Cinema_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetGenresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GetGenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // API: GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<List<Genre>>> GetGenres()
        {
            var genres = await _context.Genres
                .Select(g => new { g.Id, g.Name })
                .ToListAsync();
            return Ok(genres);
        }
    }
}