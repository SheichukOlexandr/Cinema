using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Models;
using DataAccess.Repositories.UnitOfWork;
using System.Linq.Expressions;
using System.Net;

namespace BusinessLogic.Services
{
    public class SessionsService : BaseService<SessionDTO, Session>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Expression<Func<Session, object>>[] _properties = {
                session => session.MoviePrice,
                session => session.Room,
                session => session.MoviePrice.Movie,
                session => session.MoviePrice.Movie.Genre,
                session => session.MoviePrice.Movie.Status
            };

        public SessionsService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork.Sessions, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<SessionDTO> GetAsync(int id)
        {
            var session = await _repository.GetByIdAsync(id, includeProperties: _properties);

            if (session == null)
                throw new Exception("" + HttpStatusCode.NotFound);

            return _mapper.Map<SessionDTO>(session);
        }

        public override async Task<IEnumerable<SessionDTO>> GetAllAsync()
        {
            var sessions = await _repository.GetAllAsync(includeProperties: _properties);

            return _mapper.Map<IEnumerable<SessionDTO>>(sessions);
        }

        public async Task<IEnumerable<MoviePriceDTO>> GetAllMoviePricesAsync()
        {
            return _mapper.Map<IEnumerable<MoviePriceDTO>>(await _unitOfWork.MoviesPrices.GetAllAsync());
        }

        public async Task<IEnumerable<RoomDTO>> GetAllRoomsAsync()
        {
            return _mapper.Map<IEnumerable<RoomDTO>>(await _unitOfWork.Rooms.GetAllAsync());
        }

        public async Task<IEnumerable<MoviePriceDTO>> GetPricesByMovieIdAsync(int movieId)
        {
            var moviePrices = await _unitOfWork.MoviesPrices.GetAllAsync(mp => mp.MovieId == movieId);
            var moviePriceDTOs = moviePrices.Select(mp => new MoviePriceDTO
            {
                Id = mp.Id,
                MovieId = mp.MovieId,
                Price = mp.Price,
                MovieName = mp.Movie.Title, // Ensure this is correctly mapped
                MoviePriceName = $"Ціна: {mp.Price.ToString("C", new System.Globalization.CultureInfo("uk-UA"))}"
            });
            return moviePriceDTOs;
        }

        public async Task<IEnumerable<MovieDTO>> GetAllMoviesAsync()
        {
            return _mapper.Map<IEnumerable<MovieDTO>>(await _unitOfWork.Movies.GetAllAsync());
        }
    }
}