using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;

namespace DataAccess.Contexts
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        // Таблиці бази даних
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<MoviePrice> MoviePrices { get; set; }
        public DbSet<ReservationStatus> ReservationStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Конфігурація зв'язків між моделями

            // Seat -> Room
            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Room)
                .WithMany()
                .HasForeignKey(s => s.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Session -> Movie
            modelBuilder.Entity<Session>()
                .HasOne(s => s.Movie)
                .WithMany()
                .HasForeignKey(s => s.MovieId);

            // Session -> Room
            modelBuilder.Entity<Session>()
                .HasOne(s => s.Room)
                .WithMany()
                .HasForeignKey(s => s.RoomId);

            // Session -> MoviePrice
            modelBuilder.Entity<Session>()
                .HasOne(s => s.MoviePrice)
                .WithMany()
                .HasForeignKey(s => s.MoviePriceId);

            // Reservation -> User
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId);

            // Reservation -> Session
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Session)
                .WithMany()
                .HasForeignKey(r => r.SessionId);

            // Reservation -> Seat
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Seat)
                .WithMany()
                .HasForeignKey(r => r.SeatId);

            // Reservation -> ReservationStatus
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Status)
                .WithMany()
                .HasForeignKey(r => r.StatusId);

        }
    }
}