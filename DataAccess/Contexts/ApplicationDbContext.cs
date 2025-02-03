using Microsoft.EntityFrameworkCore;
using DataAccess.Models;

namespace DataAccess.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseLazyLoadingProxies();
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
        public DbSet<MovieStatus> MovieStatuses { get; set; }
        public DbSet<UserStatus> UserStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Конфігурація зв'язків між моделями

            // Genre -> Movie
            modelBuilder.Entity<Genre>()
                .HasMany(g => g.Movies)
                .WithOne(m => m.Genre)
                .HasForeignKey(m => m.GenreId)
                .OnDelete(DeleteBehavior.Cascade);

            // MovieStatus -> Movie
            modelBuilder.Entity<MovieStatus>()
                .HasMany(ms => ms.Movies)
                .WithOne(m => m.Status)
                .HasForeignKey(m => m.StatusId)
                .OnDelete(DeleteBehavior.Cascade);

            // Movie -> MoviePrice
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.MoviePrices)
                .WithOne(mp => mp.Movie)
                .HasForeignKey(mp => mp.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            // Room -> Session
            modelBuilder.Entity<Room>()
                .HasMany(r => r.Sessions)
                .WithOne(s => s.Room)
                .HasForeignKey(s => s.RoomId)
                .OnDelete(DeleteBehavior.NoAction);

            // Room -> Seat
            modelBuilder.Entity<Room>()
                .HasMany(r => r.Seats)
                .WithOne(s => s.Room)
                .HasForeignKey(s => s.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seat -> Reservation
            modelBuilder.Entity<Seat>()
                .HasMany(s => s.Reservations)
                .WithOne(r => r.Seat)
                .HasForeignKey(r => r.SeatId)
                .OnDelete(DeleteBehavior.NoAction);

            // Session -> Reservation
            modelBuilder.Entity<Session>()
                .HasMany(s => s.Reservations)
                .WithOne(r => r.Session)
                .HasForeignKey(r => r.SessionId)
                .OnDelete(DeleteBehavior.NoAction);

            // MoviePrice -> Session
            modelBuilder.Entity<MoviePrice>()
                .HasMany(s => s.Sessions)
                .WithOne(r => r.MoviePrice)
                .HasForeignKey(r => r.MoviePriceId)
                .OnDelete(DeleteBehavior.NoAction);

            // User -> Reservation
            modelBuilder.Entity<User>()
                .HasMany(u => u.Reservations)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ReservationStatus -> Reservation
            modelBuilder.Entity<ReservationStatus>()
                .HasMany(rs => rs.Reservations)
                .WithOne(r => r.Status)
                .HasForeignKey(r => r.StatusId)
                .OnDelete(DeleteBehavior.Cascade);

            // UserStatus -> User
            modelBuilder.Entity<UserStatus>()
                .HasMany(us => us.Users)
                .WithOne(u => u.Status)
                .HasForeignKey(u => u.StatusId)
                .OnDelete(DeleteBehavior.Cascade);

            // Налаштування властивостей
            modelBuilder.Entity<Genre>()
                .Property(g => g.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<MoviePrice>()
                .Property(mp => mp.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Seat>()
                .Property(s => s.ExtraPrice)
                .HasColumnType("decimal(18,2)");
        }
    }
}