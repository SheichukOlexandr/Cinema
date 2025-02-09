using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Models;
using DataAccess.Repositories.UnitOfWork;
using System.Linq.Expressions;
using System.Net;

namespace BusinessLogic.Services
{
    public class ReservationService(IUnitOfWork unitOfWork, IMapper mapper) 
        : BaseService<ReservationDTO, Reservation>(unitOfWork.Reservations, mapper)
    {
        private readonly Expression<Func<Reservation, object>>[] _properties = [
            reservation => reservation.User,
            reservation => reservation.Session,
            reservation => reservation.Session.MoviePrice,
            reservation => reservation.Session.Room,
            reservation => reservation.Session.MoviePrice.Movie,
            reservation => reservation.Session.MoviePrice.Movie.Genre,
            reservation => reservation.Session.MoviePrice.Movie.Status,
            reservation => reservation.Seat,
            reservation => reservation.Status,
        ];

        public override async Task<ReservationDTO> GetAsync(int id)
        {
            var session = await _repository.GetByIdAsync(id, includeProperties: _properties);

            if (session == null)
                throw new Exception("" + HttpStatusCode.NotFound);

            return _mapper.Map<ReservationDTO>(session);
        }

        public override async Task<IEnumerable<ReservationDTO>> GetAllAsync()
        {
            var sessions = await _repository.GetAllAsync(includeProperties: _properties);

            return _mapper.Map<IEnumerable<ReservationDTO>>(sessions);
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            return _mapper.Map<IEnumerable<UserDTO>>(await unitOfWork.Users.GetAllAsync());
        }

        public async Task<IEnumerable<SessionDTO>> GetAllSessionsAsync()
        {
            return _mapper.Map<IEnumerable<SessionDTO>>(await unitOfWork.Sessions.GetAllAsync());
        }

        public async Task<IEnumerable<SeatDTO>> GetAllSeatsAsync()
        {
            return _mapper.Map<IEnumerable<SeatDTO>>(await unitOfWork.Seats.GetAllAsync());
        }

        public async Task<IEnumerable<SeatDTO>> GetAllSeatsAsync(int sessionId)
        {
            var session = await unitOfWork.Sessions.GetByIdAsync(sessionId);
            if (session == null)
            {
                return Enumerable.Empty<SeatDTO>();
            }

            return _mapper.Map<IEnumerable<SeatDTO>>(
                await unitOfWork.Seats.GetAllAsync(filter:
                    seat => seat.RoomId == session.RoomId
                )
            );
        }

        public async Task<IEnumerable<ReservationStatusDTO>> GetAllReservationStatusesAsync()
        {
            return _mapper.Map<IEnumerable<ReservationStatusDTO>>(await unitOfWork.ReservationStatues.GetAllAsync());
        }

        public async Task<IEnumerable<ReservationDTO>> GetAllReservationsByUserIdAsync(int userId)
        {
            return _mapper.Map<IEnumerable<ReservationDTO>>(await unitOfWork.Reservations.GetAllAsync(filter: it => it.UserId == userId));
        }

        public async Task<ReservationStatus> GetOrCreateReservationStatusAsync(string statusName)
        {
            var status = await unitOfWork.ReservationStatues.FirstOrDefaultAsync(s => s.Name == statusName);
            if (status == null)
            {
                status = new ReservationStatus { Name = statusName };
                await unitOfWork.ReservationStatues.AddAsync(status);
            }
            return status;
        }

        public async Task CancelReservationAsync(ReservationDTO reservationDTO)
        {
            var reservation = await unitOfWork.Reservations.GetByIdAsync(reservationDTO.Id);
            if (reservation == null)
            {
                throw new InvalidOperationException("Reservation not found.");
            }

            var status = await GetOrCreateReservationStatusAsync(ReservationStatusDTO.Cancelled);
            reservation.StatusId = status.Id;

            await unitOfWork.Reservations.UpdateAsync(reservation);
        }

        public async Task ConfirmReservationAsync(ReservationDTO reservationDTO)
        {
            var reservation = await unitOfWork.Reservations.GetByIdAsync(reservationDTO.Id);
            if (reservation == null)
            {
                throw new InvalidOperationException("Reservation not found.");
            }

            var status = await GetOrCreateReservationStatusAsync(ReservationStatusDTO.Confirmed);
            reservation.StatusId = status.Id;
            await unitOfWork.Reservations.UpdateAsync(reservation);
        }
    }
}
