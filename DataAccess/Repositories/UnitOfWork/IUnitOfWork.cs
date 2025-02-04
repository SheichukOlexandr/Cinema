using DataAccess.Models;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Movie> Movies { get; }
        IGenericRepository<Reservation> Reservations { get; }
        IGenericRepository<Room> Rooms { get; }
        IGenericRepository<Seat> Seats { get; }
        IGenericRepository<Session> Sessions { get; }
        IGenericRepository<MoviePrice> MoviesPrices { get; }
        IGenericRepository<Genre> Genres { get; }
        IGenericRepository<MovieStatus> MovieStatues { get; }
        IGenericRepository<ReservationStatus> ReservationStatues { get; }
        IGenericRepository<UserStatus> UserStatuses { get; }
        Task<int> SaveChangesAsync();
        void Dispose();
    }
}
