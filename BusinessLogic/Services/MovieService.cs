using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Models;
using DataAccess.Repositories.UnitOfWork;
using System.Net;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class MovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieDTO>> GetAllAsync()
        {
            var movies = await _unitOfWork.Movies.GetAllAsync(includeProperties: new Expression<Func<Movie, object>>[]
            {
                movie => movie.Genre,
                movie => movie.Status
            });

            return _mapper.Map<IEnumerable<MovieDTO>>(movies);
        }

        public async Task<MovieDTO> GetAsync(int id)
        {
            var movie = await _unitOfWork.Movies.GetByIdAsync(id, includeProperties: new Expression<Func<Movie, object>>[]
            {
                movie => movie.Genre,
                movie => movie.Status
            });

            if (movie == null)
                throw new Exception("" + HttpStatusCode.NotFound);

            return _mapper.Map<MovieDTO>(movie);
        }

        public async Task CreateAsync(MovieDTO movie)
        {
            await _unitOfWork.Movies.AddAsync(_mapper.Map<Movie>(movie));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task EditAsync(MovieDTO movie)
        {
            await _unitOfWork.Movies.UpdateAsync(_mapper.Map<Movie>(movie));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var movie = await _unitOfWork.Movies.GetByIdAsync(id);

            if (movie == null)
                throw new Exception("" + HttpStatusCode.NotFound);

            await _unitOfWork.Movies.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<GenreDTO>> GetAllGenresAsync()
        {
            return _mapper.Map<IEnumerable<GenreDTO>>(await _unitOfWork.Genres.GetAllAsync());
        }

        public async Task<IEnumerable<MovieStatusDTO>> GetAllMovieStatusesAsync()
        {
            return _mapper.Map<IEnumerable<MovieStatusDTO>>(await _unitOfWork.MovieStatues.GetAllAsync());
        }
    }
}
