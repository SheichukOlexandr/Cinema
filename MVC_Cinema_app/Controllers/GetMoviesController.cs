using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC_Cinema_app.Controllers
{
    [Route("api/Movies")]
    [ApiController]
    public class GetMoviesController : ControllerBase
    {
        private readonly MovieService _movieService;

        public GetMoviesController(MovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
        {
            var movies = await _movieService.GetAllAsync();
            return Ok(movies);
        }
    }
}
