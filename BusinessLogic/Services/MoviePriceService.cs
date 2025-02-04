using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Models;
using DataAccess.Repositories.UnitOfWork;
using System.Net;

namespace BusinessLogic.Services
{
    public class MoviePriceService(IUnitOfWork unitOfWork, IMapper mapper) 
        : BaseService<MoviePriceDTO, MoviePrice>(unitOfWork.MoviesPrices, mapper)
    {
        public override async Task<MoviePriceDTO> GetAsync(int id)
        {
            var moviePrice = await _repository.GetByIdAsync(id, includeProperties: [
                moviePrice => moviePrice.Movie
            ]);

            if (moviePrice == null)
                throw new Exception("" + HttpStatusCode.NotFound);

            return _mapper.Map<MoviePriceDTO>(moviePrice);
        }

        public override async Task<IEnumerable<MoviePriceDTO>> GetAllAsync()
        {
            var moviePrices = await _repository.GetAllAsync(includeProperties: [
                moviePrice => moviePrice.Movie
            ]);

            return _mapper.Map<IEnumerable<MoviePriceDTO>>(moviePrices);
        }
        public async Task<IEnumerable<MovieDTO>> GetAllMoviesAsync()
        {
            return _mapper.Map<IEnumerable<MovieDTO>>(await unitOfWork.Movies.GetAllAsync());
        }
    }
}