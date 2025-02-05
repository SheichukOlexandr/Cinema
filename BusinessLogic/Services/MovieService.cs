using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Models;
using DataAccess.Repositories.UnitOfWork;
using System.Net;

namespace BusinessLogic.Services
{
    public class MovieService(IUnitOfWork unitOfWork, IMapper mapper) 
        : BaseService<MovieDTO, Movie>(unitOfWork.Movies, mapper)
    {
        public override async Task<MovieDTO> GetAsync(int id)
        {
            var movie = await _repository.GetByIdAsync(id, includeProperties: [
                movie => movie.Genre,
                movie => movie.Status
            ]);

            if (movie == null)
                throw new Exception("" + HttpStatusCode.NotFound);

            return _mapper.Map<MovieDTO>(movie);
        }

        public override async Task<IEnumerable<MovieDTO>> GetAllAsync()
        {
            var movies = await _repository.GetAllAsync(includeProperties: [
                movie => movie.Genre,
                movie => movie.Status
            ]);

            return _mapper.Map<IEnumerable<MovieDTO>>(movies);
        }

        public async Task<IEnumerable<GenreDTO>> GetAllGenresAsync()
        {
            return _mapper.Map<IEnumerable<GenreDTO>>(await unitOfWork.Genres.GetAllAsync());
        }

        public async Task<IEnumerable<MovieStatusDTO>> GetAllMovieStatusesAsync()
        {
            return _mapper.Map<IEnumerable<MovieStatusDTO>>(await unitOfWork.MovieStatues.GetAllAsync());
        }

        public async Task<string?> GetMovieNameByIdAsync(int movieId)
        {
            var movie = await _repository.GetByIdAsync(movieId);
            return movie?.Title;
        }
    }
}
