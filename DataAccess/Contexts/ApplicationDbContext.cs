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

        // Метод для заповнення бази даних початковими даними
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Movies.Any())
            {
                return; // База даних вже заповнена
            }

            // Додавання жанрів
            var genres = new Genre[]
            {
                new Genre { Name = "Бойовик" },
                new Genre { Name = "Комедія" },
                new Genre { Name = "Драма" },
                new Genre { Name = "Жахи" },
                new Genre { Name = "Наукова фантастика" }
            };
            context.Genres.AddRange(genres);
            context.SaveChanges();

            // Додавання статусів фільмів
            var movieStatuses = new MovieStatus[]
            {
                new MovieStatus { Name = "В прокаті" },
                new MovieStatus { Name = "Скоро у кіно" },
                new MovieStatus { Name = "Архівний" }
            };
            context.MovieStatuses.AddRange(movieStatuses);
            context.SaveChanges();

            // Додавання фільмів
            var movies = new Movie[]
            {
                new Movie { Title = "Початок", Director = "Крістофер Нолан", Duration = 148, Cast = "Леонардо ДіКапріо, Джозеф Гордон-Левітт", GenreId = genres[0].Id, ReleaseDate = new DateOnly(2010, 7, 16), Description = "Злодій, який викрадає корпоративні таємниці за допомогою технології обміну снами.", MinAge = 13, Rating = 8.8, StatusId = movieStatuses[0].Id, PosterURL = "url1", TrailerURL = "url1" },
                new Movie { Title = "Інтерстеллар", Director = "Крістофер Нолан", Duration = 169, Cast = "Меттью МакКонахі, Енн Гетевей", GenreId = genres[4].Id, ReleaseDate = new DateOnly(2014, 11, 7), Description = "Подорож крізь простір і час для порятунку людства.", MinAge = 12, Rating = 8.6, StatusId = movieStatuses[0].Id, PosterURL = "url2", TrailerURL = "url2" },
                new Movie { Title = "Темний лицар", Director = "Крістофер Нолан", Duration = 152, Cast = "Крістіан Бейл, Гіт Леджер", GenreId = genres[0].Id, ReleaseDate = new DateOnly(2008, 7, 18), Description = "Бетмен бореться з хаосом, який створює Джокер.", MinAge = 13, Rating = 9.0, StatusId = movieStatuses[0].Id, PosterURL = "url3", TrailerURL = "url3" },
                new Movie { Title = "Форсаж 7", Director = "Джеймс Ван", Duration = 137, Cast = "Він Дізель, Пол Вокер", GenreId = genres[0].Id, ReleaseDate = new DateOnly(2015, 4, 3), Description = "Команда вуличних гонщиків стикається з новими викликами.", MinAge = 16, Rating = 7.1, StatusId = movieStatuses[0].Id, PosterURL = "url4", TrailerURL = "url4" },
                new Movie { Title = "Месники: Фінал", Director = "Джо Руссо, Ентоні Руссо", Duration = 181, Cast = "Роберт Дауні-молодший, Кріс Еванс", GenreId = genres[0].Id, ReleaseDate = new DateOnly(2019, 4, 26), Description = "Фінальна битва Месників проти Таноса.", MinAge = 12, Rating = 8.4, StatusId = movieStatuses[0].Id, PosterURL = "url5", TrailerURL = "url5" },
                new Movie { Title = "Джокер", Director = "Тодд Філліпс", Duration = 122, Cast = "Хоакін Фенікс", GenreId = genres[2].Id, ReleaseDate = new DateOnly(2019, 10, 4), Description = "Історія походження одного з найвідоміших лиходіїв коміксів.", MinAge = 18, Rating = 8.4, StatusId = movieStatuses[0].Id, PosterURL = "url6", TrailerURL = "url6" },
                new Movie { Title = "Титанік", Director = "Джеймс Кемерон", Duration = 195, Cast = "Леонардо ДіКапріо, Кейт Вінслет", GenreId = genres[2].Id, ReleaseDate = new DateOnly(1997, 12, 19), Description = "Історія кохання на тлі катастрофи Титаніка.", MinAge = 12, Rating = 7.8, StatusId = movieStatuses[0].Id, PosterURL = "url7", TrailerURL = "url7" },
                new Movie { Title = "Гаррі Поттер і філософський камінь", Director = "Кріс Коламбус", Duration = 152, Cast = "Деніел Редкліфф, Емма Вотсон", GenreId = genres[4].Id, ReleaseDate = new DateOnly(2001, 11, 16), Description = "Перша частина пригод Гаррі Поттера у світі чарівників.", MinAge = 10, Rating = 7.6, StatusId = movieStatuses[0].Id, PosterURL = "url8", TrailerURL = "url8" },
                new Movie { Title = "Волл-і", Director = "Ендрю Стентон", Duration = 98, Cast = "Бен Берт, Елісса Найт", GenreId = genres[4].Id, ReleaseDate = new DateOnly(2008, 6, 27), Description = "Історія маленького робота, який змінив світ.", MinAge = 6, Rating = 8.4, StatusId = movieStatuses[0].Id, PosterURL = "url9", TrailerURL = "url9" },
                new Movie { Title = "Король Лев", Director = "Джон Фавро", Duration = 118, Cast = "Дональд Гловер, Бейонсе", GenreId = genres[2].Id, ReleaseDate = new DateOnly(2019, 7, 19), Description = "Ремейк класичного мультфільму про пригоди Сімби.", MinAge = 6, Rating = 6.9, StatusId = movieStatuses[0].Id, PosterURL = "url10", TrailerURL = "url10" },
                new Movie { Title = "Паразити", Director = "Пон Джун Хо", Duration = 132, Cast = "Сон Кан Хо, Лі Сон Гюн", GenreId = genres[2].Id, ReleaseDate = new DateOnly(2019, 5, 30), Description = "Історія про соціальну нерівність через призму однієї родини.", MinAge = 16, Rating = 8.6, StatusId = movieStatuses[0].Id, PosterURL = "url11", TrailerURL = "url11" },
                new Movie { Title = "Дюна", Director = "Дені Вільньов", Duration = 155, Cast = "Тімоті Шаламе, Зендея", GenreId = genres[4].Id, ReleaseDate = new DateOnly(2021, 10, 22), Description = "Епічна науково-фантастична сага про боротьбу за виживання.", MinAge = 13, Rating = 8.1, StatusId = movieStatuses[0].Id, PosterURL = "url12", TrailerURL = "url12" },
                new Movie { Title = "Зоряні війни: Епізод IV - Нова надія", Director = "Джордж Лукас", Duration = 121, Cast = "Марк Гемілл, Харрісон Форд", GenreId = genres[4].Id, ReleaseDate = new DateOnly(1977, 5, 25), Description = "Перший епізод культової космічної саги.", MinAge = 10, Rating = 8.6, StatusId = movieStatuses[0].Id, PosterURL = "url13", TrailerURL = "url13" },
                new Movie { Title = "Матриця", Director = "Лана Вачовскі, Ліллі Вачовскі", Duration = 136, Cast = "Кіану Рівз, Лоренс Фішберн", GenreId = genres[4].Id, ReleaseDate = new DateOnly(1999, 3, 31), Description = "Класика наукової фантастики про боротьбу зі штучним інтелектом.", MinAge = 16, Rating = 8.7, StatusId = movieStatuses[0].Id, PosterURL = "url14", TrailerURL = "url14" },
                new Movie { Title = "Володар перснів: Хранителі Персня", Director = "Пітер Джексон", Duration = 178, Cast = "Елайджа Вуд, Вігго Мортенсен", GenreId = genres[2].Id, ReleaseDate = new DateOnly(2001, 12, 19), Description = "Перша частина епічної трилогії за мотивами творів Толкіна.", MinAge = 12, Rating = 8.8, StatusId = movieStatuses[0].Id, PosterURL = "url15", TrailerURL = "url15" }
            };
            context.Movies.AddRange(movies);
            context.SaveChanges();

            // Додавання цін на фільми
            var moviePrices = movies.Select((movie, index) => new MoviePrice
            {
                Movie = movie,
                Price = 200.00m + index * 10
            }).ToArray();

            context.MoviePrices.AddRange(moviePrices);
            context.SaveChanges();

            // Додавання кімнат
            var rooms = new Room[]
            {
                new Room { Name = "Зал 1", Capacity = 100 },
                new Room { Name = "Зал 2", Capacity = 150 }
            };
            context.Rooms.AddRange(rooms);
            context.SaveChanges();

            // Додавання місць
            var seats = new Seat[]
            {
                new Seat { RoomId = rooms[0].Id, Number = 1, ExtraPrice = 0.00m },
                new Seat { RoomId = rooms[0].Id, Number = 2, ExtraPrice = 0.00m },
                new Seat { RoomId = rooms[1].Id, Number = 1, ExtraPrice = 50.00m },
                new Seat { RoomId = rooms[1].Id, Number = 2, ExtraPrice = 50.00m }
            };
            context.Seats.AddRange(seats);
            context.SaveChanges();

            // Додавання сеансів для кожного фільму
            var sessions = new List<Session>();
            var startTime = new TimeOnly(10, 0);

            foreach (var movie in movies)
            {
                for (int i = 0; i < 3; i++) // 3 сеанси для кожного фільму
                {
                    sessions.Add(new Session
                    {
                        MovieId = movie.Id,  // Виправлено: використовуємо ID, а не об'єкт
                        RoomId = rooms[i % rooms.Length].Id, // Виправлено: використовуємо ID
                        Date = DateOnly.FromDateTime(DateTime.Today.AddDays(i)),
                        Time = startTime.AddHours(i * 3) // Кожен сеанс через 3 години
                    });
                }
            }

            context.Sessions.AddRange(sessions);
            context.SaveChanges();
        }
    }
    }
