using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC_Cinema_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetGenresController : Controller
    {
        private readonly GenreService _genreService;

        public GetGenresController(GenreService genreService)
        {
            _genreService = genreService;
        }

        // API: GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<List<GenreDTO>>> GetGenres()
        {
            var genres = await _genreService.GetAllAsync();
            return Ok(genres);
        }
    }
}
