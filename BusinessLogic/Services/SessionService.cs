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
    }
}
