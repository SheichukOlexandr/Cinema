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

            Initialize(modelBuilder);
        }

        // Метод для заповнення бази даних початковими даними
        public static void Initialize(ModelBuilder modelBuilder)
        {
            // Додавання жанрів
            var genres = new Genre[]
            {
                new Genre { Id = 1, Name = "Бойовик" },
                new Genre { Id = 2, Name = "Комедія" },
                new Genre { Id = 3, Name = "Драма" },
                new Genre { Id = 4, Name = "Фантастика" },
                new Genre { Id = 5, Name = "Трилер" },
                new Genre { Id = 6, Name = "Жахи" },
                new Genre { Id = 7, Name = "Мелодрама" },
                new Genre { Id = 8, Name = "Пригоди" },
                new Genre { Id = 9, Name = "Анімація" },
                new Genre { Id = 10, Name = "Документальний" }
            };
            modelBuilder.Entity<Genre>().HasData(genres);

            // Додавання статусів фільмів
            var movieStatuses = new MovieStatus[]
            {
                new MovieStatus { Id = 1, Name = "В прокаті" },
                new MovieStatus { Id = 2, Name = "Скоро у кіно" },
                new MovieStatus { Id = 3, Name = "Архівний" }
            };
            modelBuilder.Entity<MovieStatus>().HasData(movieStatuses);

            // Додавання фільмів
            var movies = new Movie[]
            {
                new Movie { Id = 1, Title = "Початок", Director = "Крістофер Нолан", Duration = 148, Cast = "Леонардо ДіКапріо, Джозеф Гордон-Левітт", GenreId = genres[0].Id, ReleaseDate = new DateOnly(2010, 7, 16), Description = "Злодій, який викрадає корпоративні таємниці за допомогою технології обміну снами.", MinAge = 13, Rating = 8.8, StatusId = movieStatuses[0].Id, PosterURL = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/r84x4x93LbZ2gozISTBYVeq0gLZ.jpg", BannerURL = "https://media.themoviedb.org/t/p/w1000_and_h563_face/ii8QGacT3MXESqBckQlyrATY0lT.jpg", TrailerURL = "https://youtu.be/cdx31ak4KbQ" },
                new Movie { Id = 2, Title = "Інтерстеллар", Director = "Крістофер Нолан", Duration = 169, Cast = "Меттью МакКонахі, Енн Гетевей", GenreId = genres[3].Id, ReleaseDate = new DateOnly(2014, 11, 7), Description = "Подорож крізь простір і час для порятунку людства.", MinAge = 12, Rating = 8.6, StatusId = movieStatuses[0].Id, PosterURL = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/9d1sCoMSGJZtghS2X9us1h9u8lW.jpg", BannerURL = "https://media.themoviedb.org/t/p/w1000_and_h563_face/rAiYTfKGqDCRIIqo664sY9XZIvQ.jpg", TrailerURL = "https://youtu.be/_DQ-lqOhwIM" },
                new Movie { Id = 3, Title = "Темний лицар", Director = "Крістофер Нолан", Duration = 152, Cast = "Крістіан Бейл, Гіт Леджер", GenreId = genres[0].Id, ReleaseDate = new DateOnly(2008, 7, 18), Description = "Бетмен бореться з хаосом, який створює Джокер.", MinAge = 13, Rating = 9.0, StatusId = movieStatuses[0].Id, PosterURL = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/hAf98uHIXMFzqNN5LX1vnouCShr.jpg", BannerURL = "https://media.themoviedb.org/t/p/w1000_and_h563_face/enNubozHn9pXi0ycTVYUWfpHZm.jpg", TrailerURL = "https://www.youtube.com/watch?v=EXeTwQWrcwY" },
                new Movie { Id = 4, Title = "Форсаж 7", Director = "Джеймс Ван", Duration = 137, Cast = "Він Дізель, Пол Вокер", GenreId = genres[0].Id, ReleaseDate = new DateOnly(2015, 4, 3), Description = "Команда вуличних гонщиків стикається з новими викликами.", MinAge = 16, Rating = 7.1, StatusId = movieStatuses[0].Id, PosterURL = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/rLxfXS0Dpd4ixD7HOYagWTx83SS.jpg", BannerURL = "https://media.themoviedb.org/t/p/w1000_and_h563_face/cHkhb5A4gQRK6zs6Pv7zorHs8Nk.jpg", TrailerURL = "https://youtu.be/USvJsCb1hA4" },
                new Movie { Id = 5, Title = "Месники: Фінал", Director = "Джо Руссо, Ентоні Руссо", Duration = 181, Cast = "Роберт Дауні-молодший, Кріс Еванс", GenreId = genres[3].Id, ReleaseDate = new DateOnly(2019, 4, 26), Description = "Фінальна битва Месників проти Таноса.", MinAge = 12, Rating = 8.4, StatusId = movieStatuses[0].Id, PosterURL = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/6gWhqrYEOBqqzXPKP7c0m4bIvTX.jpg", BannerURL = "https://media.themoviedb.org/t/p/w1000_and_h563_face/h9q0ozwMWy7CK5U7FSZsMVtbsCQ.jpg", TrailerURL = "https://youtu.be/7ZHb4PWOI5M" },
                new Movie { Id = 6, Title = "Джокер", Director = "Тодд Філліпс", Duration = 122, Cast = "Хоакін Фенікс", GenreId = genres[1].Id, ReleaseDate = new DateOnly(2019, 10, 4), Description = "Історія походження одного з найвідоміших лиходіїв коміксів.", MinAge = 18, Rating = 8.4, StatusId = movieStatuses[0].Id, PosterURL = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/5h77YBF7g9s0ju5yblWskkN3wa7.jpg", BannerURL = "https://media.themoviedb.org/t/p/w1000_and_h563_face/ftkY1xIQ6ianSVp3EDufPVPLwa2.jpg", TrailerURL = "https://youtu.be/y2NE4dYI5Pg" },
                new Movie { Id = 7, Title = "Їжак Сонік 3 ", Director = "Джефф Фаулер", Duration = 110, Cast = "Джим Керрі, Кіану Рівз", GenreId = genres[1].Id, ReleaseDate = new DateOnly(2024, 12, 26), Description = "Сонік, Наклз і Тейлз повертаються для нової грандіозної пригоди.", MinAge = 0, Rating = 7.8, StatusId = movieStatuses[0].Id, PosterURL = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/9U8WEuITXagHQWFQW48egF8oZeb.jpg", BannerURL = "https://media.themoviedb.org/t/p/w1000_and_h563_face/zOpe0eHsq0A2NvNyBbtT6sj53qV.jpg", TrailerURL = "https://youtu.be/bDIUaKYV_Tg" },
                new Movie { Id = 8, Title = "Гаррі Поттер і філософський камінь", Director = "Кріс Коламбус", Duration = 152, Cast = "Деніел Редкліфф, Емма Вотсон", GenreId = genres[7].Id, ReleaseDate = new DateOnly(2001, 11, 16), Description = "Перша частина пригод Гаррі Поттера у світі чарівників.", MinAge = 10, Rating = 7.6, StatusId = movieStatuses[0].Id, PosterURL = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/fczjRHiQCSHHFXBwa6peifwbvFz.jpg", BannerURL = "https://media.themoviedb.org/t/p/w1000_and_h563_face/hwInwXo34ji3QfcNXvFBC3GX2TA.jpg", TrailerURL = "https://youtu.be/l91Km49W9qI" },
                new Movie { Id = 9, Title = "Волл-і", Director = "Ендрю Стентон", Duration = 98, Cast = "Бен Берт, Елісса Найт", GenreId = genres[3].Id, ReleaseDate = new DateOnly(2008, 6, 27), Description = "Історія маленького робота, який змінив світ.", MinAge = 6, Rating = 8.4, StatusId = movieStatuses[0].Id, PosterURL = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/1ZF4joYyVyg4xrapmGzZUCIv7Lj.jpg", BannerURL = "https://media.themoviedb.org/t/p/w1000_and_h563_face/3CvNc04vTwTir5q3LBDuvUb8Ng6.jpg", TrailerURL = "https://youtu.be/Tbr_L9Gap_M" },
                new Movie { Id = 10, Title = "Король Лев", Director = "Джон Фавро", Duration = 118, Cast = "Дональд Гловер, Бейонсе", GenreId = genres[1].Id, ReleaseDate = new DateOnly(2019, 7, 19), Description = "Ремейк класичного мультфільму про пригоди Сімби.", MinAge = 6, Rating = 6.9, StatusId = movieStatuses[0].Id, PosterURL = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/azLX5jPAbjWbdAUUrspkRFvx8Z1.jpg", BannerURL = "https://media.themoviedb.org/t/p/w1000_and_h563_face/zfqOvDITgMM4tg1DGRnLRtlu5PN.jpg", TrailerURL = "https://youtu.be/QUoxYKDtpUk" },
                new Movie { Id = 11,Title = "Паразити", Director = "Пон Джун Хо", Duration = 132, Cast = "Сон Кан Хо, Лі Сон Гюн", GenreId = genres[4].Id, ReleaseDate = new DateOnly(2019, 5, 30), Description = "Історія про соціальну нерівність через призму однієї родини.", MinAge = 16, Rating = 8.6, StatusId = movieStatuses[0].Id, PosterURL = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/eYQlHTn4kD7CKbudVzxHMxVU3cV.jpg", BannerURL = "https://media.themoviedb.org/t/p/w1000_and_h563_face/hiKmpZMGZsrkA3cdce8a7Dpos1j.jpg", TrailerURL = "https://youtu.be/KKNXGJIpnKY" },
                new Movie { Id = 12,Title = "Дюна", Director = "Дені Вільньов", Duration = 155, Cast = "Тімоті Шаламе, Зендея", GenreId = genres[3].Id, ReleaseDate = new DateOnly(2021, 10, 22), Description = "Епічна науково-фантастична сага про боротьбу за виживання.", MinAge = 13, Rating = 8.1, StatusId = movieStatuses[0].Id, PosterURL = "https://uaserial.com/images/serials/66/662c218bd4504886698257.webp", BannerURL = "https://media.themoviedb.org/t/p/w1000_and_h563_face/jYEW5xZkZk2WTrdbMGAPFuBqbDc.jpg", TrailerURL = "https://youtu.be/Ljzu52GMytk" },
                new Movie { Id = 13,Title = "Зоряні війни: Епізод IV - Нова надія", Director = "Джордж Лукас", Duration = 121, Cast = "Марк Гемілл, Харрісон Форд", GenreId = genres[7].Id, ReleaseDate = new DateOnly(1977, 5, 25), Description = "Перший епізод культової космічної саги.", MinAge = 10, Rating = 8.6, StatusId = movieStatuses[0].Id, PosterURL = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/rEpXNtqdiNul8sJa3sUQRIYdVDU.jpg", BannerURL = "https://media.themoviedb.org/t/p/w1000_and_h563_face/v8VIkb8XwmM9KilPHLvyNZpEEur.jpg", TrailerURL = "https://youtu.be/XsS1yE2f-hE" },
                new Movie { Id = 14, Title = "Матриця", Director = "Лана Вачовскі, Ліллі Вачовскі", Duration = 136, Cast = "Кіану Рівз, Лоренс Фішберн", GenreId = genres[0].Id, ReleaseDate = new DateOnly(1999, 3, 31), Description = "Класика наукової фантастики про боротьбу зі штучним інтелектом.", MinAge = 16, Rating = 8.7, StatusId = movieStatuses[0].Id, PosterURL = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/58748AndVH1DitlTbcbLpKHuSS2.jpg", BannerURL = "https://media.themoviedb.org/t/p/w1000_and_h563_face/tlm8UkiQsitc8rSuIAscQDCnP8d.jpg", TrailerURL = "https://youtu.be/d0XTFAMmhrE" },
                new Movie { Id = 15, Title = "Володар перснів: Хранителі Персня", Director = "Пітер Джексон", Duration = 178, Cast = "Елайджа Вуд, Вігго Мортенсен", GenreId = genres[0].Id, ReleaseDate = new DateOnly(2001, 12, 19), Description = "Перша частина епічної трилогії за мотивами творів Толкіна.", MinAge = 12, Rating = 8.8, StatusId = movieStatuses[0].Id, PosterURL = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/yrEZeHgn2Y3F3G4w6qeI22LrZzQ.jpg", BannerURL = "https://media.themoviedb.org/t/p/w1000_and_h563_face/x2RS3uTcsJJ9IfjNPcgDmukoEcQ.jpg", TrailerURL = "https://youtu.be/CbYmZOV3G-Q" }
            };
            modelBuilder.Entity<Movie>().HasData(movies);

            // Додавання цін на фільми
            var moviePrices = movies.Select((movie, index) => new MoviePrice
            {
                Id = index + 1,
                MovieId = movie.Id,
                Price = 200.00m + index * 10
            }).ToArray();

            modelBuilder.Entity<MoviePrice>().HasData(moviePrices);

            // Додавання кімнат
            var rooms = new Room[]
            {
                new Room { Id = 1, Name = "Зал 1", Capacity = 100 },
                new Room { Id = 2, Name = "Зал 2", Capacity = 150 }
            };
            modelBuilder.Entity<Room>().HasData(rooms);

            // Додавання місць
            var seats = new Seat[]
            {
                new Seat { Id = 1, RoomId = rooms[0].Id, Number = 1, ExtraPrice = 0.00m },
                new Seat { Id = 2, RoomId = rooms[0].Id, Number = 2, ExtraPrice = 0.00m },
                new Seat { Id = 3, RoomId = rooms[1].Id, Number = 1, ExtraPrice = 50.00m },
                new Seat { Id = 4, RoomId = rooms[1].Id, Number = 2, ExtraPrice = 50.00m }
            };
            modelBuilder.Entity<Seat>().HasData(seats);

            // Додавання сеансів для кожного фільму
            var sessions = new List<Session>();
            var startTime = new TimeOnly(10, 0);

            foreach (var moviePrice in moviePrices)
            {
                for (int i = 0; i < 3; i++) // 3 сеанси для кожного фільму
                {
                    sessions.Add(new Session
                    {
                        Id = moviePrice.Id * 10 + i,
                        MoviePriceId = moviePrice.Id,
                        RoomId = rooms[i % rooms.Length].Id,
                        Date = DateOnly.FromDateTime(new DateTime(2025, 3, 1).AddDays(i + moviePrice.Id)),
                        Time = startTime.AddHours(i * 3) // Кожен сеанс через 3 години
                    });
                }
            }

            modelBuilder.Entity<Session>().HasData(sessions);

            // Add reservation statuses
            var reservationStatuses = new ReservationStatus[]
            {
                new ReservationStatus { Id = 1, Name = ReservationStatus.Created },
                new ReservationStatus { Id = 2, Name = ReservationStatus.Confirmed },
                new ReservationStatus { Id = 3, Name = ReservationStatus.Completed },
                new ReservationStatus { Id = 4, Name = ReservationStatus.Cancelled },
            };
            modelBuilder.Entity<ReservationStatus>().HasData(reservationStatuses);

            // Add user statuses
            var userStatuses = new UserStatus[]
            {
                new UserStatus { Id = 1, Name = UserStatus.Active },
                new UserStatus { Id = 2, Name = UserStatus.Admin },
                new UserStatus { Id = 3, Name = UserStatus.Blocked },
            };
            modelBuilder.Entity<UserStatus>().HasData(userStatuses);

            // Add some initial users
            var users = new User[]
            {
                new User { Id = 1, FirstName = "Mike", LastName = "Brown", Email = "user@cinema.com", PhoneNumber = "+38050221131", 
                    Password = "4f9f10b304cfe9b2b11fcb1387f694e18f08ea358c7e9f567434d3ad6cbd7fc4", // 11223344
                    StatusId = userStatuses[0].Id },
                new User { Id = 2, FirstName = "John", LastName = "Sandres", Email = "admin@cinema.com", PhoneNumber = "+38095221141", 
                    Password = "4f9f10b304cfe9b2b11fcb1387f694e18f08ea358c7e9f567434d3ad6cbd7fc4", // 11223344
                    StatusId = userStatuses[1].Id },
            };
            modelBuilder.Entity<User>().HasData(users);
        }
    }
}
