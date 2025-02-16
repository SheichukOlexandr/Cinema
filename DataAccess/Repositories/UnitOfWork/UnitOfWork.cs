using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Contexts;
using DataAccess.Repositories;
using DataAccess.Models;
using System.Threading.Tasks;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGenericRepository<User> Users { get; }
        public IGenericRepository<Movie> Movies { get; }
        public IGenericRepository<Reservation> Reservations { get; }
        public IGenericRepository<Room> Rooms { get; }
        public IGenericRepository<Seat> Seats { get; }
        public IGenericRepository<Session> Sessions { get; }
        public IGenericRepository<MoviePrice> MoviesPrices { get; }
        public IGenericRepository<Genre> Genres { get; }
        public IGenericRepository<MovieStatus> MovieStatues { get; }
        public IGenericRepository<ReservationStatus> ReservationStatues { get; }
        public IGenericRepository<UserStatus> UserStatuses { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new GenericRepository<User>(_context);
            Movies = new GenericRepository<Movie>(_context);
            Reservations = new GenericRepository<Reservation>(_context);
            Rooms = new GenericRepository<Room>(_context);
            Seats = new GenericRepository<Seat>(_context);
            Sessions = new GenericRepository<Session>(_context);
            MoviesPrices = new GenericRepository<MoviePrice>(_context);
            Genres = new GenericRepository<Genre>(_context);
            MovieStatues = new GenericRepository<MovieStatus>(_context);
            ReservationStatues = new GenericRepository<ReservationStatus>(_context);
            UserStatuses = new GenericRepository<UserStatus>(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
