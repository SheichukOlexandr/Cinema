using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Models;
using DataAccess.Repositories.UnitOfWork;
using System.Net;

namespace BusinessLogic.Services
{
    public class SeatService(IUnitOfWork unitOfWork, IMapper mapper) 
        : BaseService<SeatDTO, Seat>(unitOfWork.Seats, mapper)
    {
        public override async Task<SeatDTO> GetAsync(int id)
        {
            var seat = await _repository.GetByIdAsync(id, includeProperties: [
                seat => seat.Room
            ]);

            if (seat == null)
                throw new Exception("" + HttpStatusCode.NotFound);

            return _mapper.Map<SeatDTO>(seat);
        }

        public override async Task<IEnumerable<SeatDTO>> GetAllAsync()
        {
            var seats = await _repository.GetAllAsync(includeProperties: [
                seat => seat.Room
            ]);

            return _mapper.Map<IEnumerable<SeatDTO>>(seats);
        }
        public async Task<IEnumerable<RoomDTO>> GetAllRoomsAsync()
        {
            return _mapper.Map<IEnumerable<RoomDTO>>(await unitOfWork.Rooms.GetAllAsync());
        }
    }
}

