using Microsoft.EntityFrameworkCore;
using DataAccess.Models;

namespace DataAccess.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

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

            modelBuilder.Entity<Genre>()
                .Property(g => g.Name)
                .HasMaxLength(100)
                .IsRequired();

            // Конфігурація зв'язків між моделями

            // Movie -> Genre
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Genre)
                .WithMany()
                .HasForeignKey(m => m.GenreId)
                .OnDelete(DeleteBehavior.Cascade);

            // Movie -> MovieStatus
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Status)
                .WithMany()
                .HasForeignKey(m => m.StatusId)
                .OnDelete(DeleteBehavior.Cascade);

            // MoviePrice -> Movie
            modelBuilder.Entity<MoviePrice>()
                .HasOne(mp => mp.Movie)
                .WithMany()
                .HasForeignKey(mp => mp.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

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
                .HasForeignKey(s => s.MovieId)
                .OnDelete(DeleteBehavior.NoAction);

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
                .HasForeignKey(r => r.SessionId)
                .OnDelete(DeleteBehavior.NoAction);

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

           
            modelBuilder.Entity<MoviePrice>()
                .Property(mp => mp.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Seat>()
                .Property(s => s.ExtraPrice)
                .HasColumnType("decimal(18,2)");
        }
    }
}


