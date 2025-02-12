using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Models;
using DataAccess.Repositories.UnitOfWork;
using System.Linq.Expressions;
using System.Net;

namespace BusinessLogic.Services
{
    public class SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        : BaseService<SessionDTO, Session>(unitOfWork.Sessions, mapper)
    {
        private readonly Expression<Func<Session, object>>[] _properties = [
            session => session.MoviePrice,
            session => session.Room,
            session => session.MoviePrice.Movie,
            session => session.MoviePrice.Movie.Genre,
            session => session.MoviePrice.Movie.Status
        ];

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
            return _mapper.Map<IEnumerable<MoviePriceDTO>>(await unitOfWork.MoviesPrices.GetAllAsync());
        }

        public async Task<IEnumerable<RoomDTO>> GetAllRoomsAsync()
        {
            return _mapper.Map<IEnumerable<RoomDTO>>(await unitOfWork.Rooms.GetAllAsync());
        }

        public async Task<bool> ValidateSesionDate(SessionDTO session)
        {
            var moviePrice = await unitOfWork.MoviesPrices.GetByIdAsync(session.MoviePriceId);
            return moviePrice != null && session.Date > moviePrice.Movie.ReleaseDate;
        }

        public async Task<IEnumerable<MovieDTO>> GetAllMoviesAsync()
        {
            return _mapper.Map<IEnumerable<MovieDTO>>(await unitOfWork.Movies.GetAllAsync());
        }

        public async Task<IEnumerable<MoviePriceDTO>> GetPricesByMovieIdAsync(int movieId)
        {
            var moviePrices = await unitOfWork.MoviesPrices.GetAllAsync(mp => mp.MovieId == movieId);
            return _mapper.Map<IEnumerable<MoviePriceDTO>>(moviePrices);
        }

        public async Task<List<(MovieDTO Movie, List<SessionDTO> Sessions)>> GetSessionsGroupedByMovies(
            Expression<Func<Session, bool>>? sessionFilter = null,
            Func<MovieDTO, bool>? movieFilter = null
            )
        {
            // applies the session filter
            var sessions = await unitOfWork.Sessions.GetAllAsync(sessionFilter);

            // groups the sessions by movie prices first,
            // but a movie can have mutliple movie prices.
            // Then, it groups the previously grouped sessions by movies,
            // flattening the session lists. [I know, that's complicated. :)]
            var sessionByMovies = sessions
                .GroupBy(it => it.MoviePrice)
                .Select(it => new {
                    MoviePrice = it.Key,
                    Sessions = it.ToList()
                })
                .GroupBy(it => it.MoviePrice.Movie)
                .Select(it => (
                    // maps the data to DTO's
                    _mapper.Map<MovieDTO>(it.Key),
                    _mapper.Map<IEnumerable<SessionDTO>>(it.SelectMany(it => it.Sessions)).ToList()
                ))
                .Where(it => movieFilter == null || movieFilter(it.Item1)) // applies the movie filter
                .ToList();


            return sessionByMovies;
        }
    }
}
