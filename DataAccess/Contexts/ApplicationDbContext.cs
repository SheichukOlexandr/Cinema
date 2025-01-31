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

            // Заповнення початковими даними для жанрів
            modelBuilder.Entity<Genre>().HasData(
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
            );

            // Заповнення початковими даними для статусів фільмів
            modelBuilder.Entity<MovieStatus>().HasData(
                new MovieStatus { Id = 1, Name = "Активний" },
                new MovieStatus { Id = 2, Name = "Неактивний" }
            );

            // Заповнення початковими даними для фільмів (15 фільмів)
            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 1,
                    Title = "Початок",
                    Director = "Крістофер Нолан",
                    Duration = 148,
                    Cast = "Леонардо ДіКапріо, Джозеф Гордон-Левітт, Еллен Пейдж",
                    GenreId = 4, // Фантастика
                    ReleaseDate = DateOnly.FromDateTime(new DateTime(2010, 7, 16)),
                    Description = "Злодій, який викрадає корпоративні таємниці через технологію спільного використання снів, отримує завдання вбудувати ідею в свідомість генерального директора.",
                    MinAge = 13,
                    StatusId = 1,
                    PosterURL = "https://example.com/poster/inception.jpg",
                    TrailerURL = "https://example.com/trailer/inception.mp4"
                },
                new Movie
                {
                    Id = 2,
                    Title = "Інтерстеллар",
                    Director = "Крістофер Нолан",
                    Duration = 169,
                    Cast = "Меттью Макконахі, Енн Гетевей, Джессіка Честейн",
                    GenreId = 4, // Фантастика
                    ReleaseDate = DateOnly.FromDateTime(new DateTime(2014, 11, 7)),
                    Description = "Група дослідників використовує червоточину, щоб подолати обмеження польоту людини в космосі і знайти новий дім для людства.",
                    MinAge = 12,
                    StatusId = 1,
                    PosterURL = "https://example.com/poster/interstellar.jpg",
                    TrailerURL = "https://example.com/trailer/interstellar.mp4"
                },
                new Movie
                {
                    Id = 3,
                    Title = "Темний лицар",
                    Director = "Крістофер Нолан",
                    Duration = 152,
                    Cast = "Крістіан Бейл, Гіт Леджер, Аарон Екхарт",
                    GenreId = 1, // Бойовик
                    ReleaseDate = DateOnly.FromDateTime(new DateTime(2008, 7, 18)),
                    Description = "Бетмен, Джокер і Гарві Дент беруть участь у боротьбі за душу Готем-Сіті.",
                    MinAge = 14,
                    StatusId = 1,
                    PosterURL = "https://example.com/poster/darkknight.jpg",
                    TrailerURL = "https://example.com/trailer/darkknight.mp4"
                },
                new Movie
                {
                    Id = 4,
                    Title = "Форсаж 7",
                    Director = "Джеймс Ван",
                    Duration = 137,
                    Cast = "Він Дізель, Пол Вокер, Джейсон Стейтем",
                    GenreId = 1, // Бойовик
                    ReleaseDate = DateOnly.FromDateTime(new DateTime(2015, 4, 3)),
                    Description = "Домінік Торетто і його команда знову в центрі подій, коли з'являється новий ворог.",
                    MinAge = 13,
                    StatusId = 1,
                    PosterURL = "https://example.com/poster/fast7.jpg",
                    TrailerURL = "https://example.com/trailer/fast7.mp4"
                },
                new Movie
                {
                    Id = 5,
                    Title = "Володар перснів: Повернення короля",
                    Director = "Пітер Джексон",
                    Duration = 201,
                    Cast = "Елайджа Вуд, Вігго Мортенсен, Ієн Маккеллен",
                    GenreId = 8, // Пригоди
                    ReleaseDate = DateOnly.FromDateTime(new DateTime(2003, 12, 17)),
                    Description = "Фродо і Сем наближаються до Мордору, щоб знищити Перстень, поки Арагорн веде армію проти сил Саурона.",
                    MinAge = 12,
                    StatusId = 1,
                    PosterURL = "https://example.com/poster/lotr3.jpg",
                    TrailerURL = "https://example.com/trailer/lotr3.mp4"
                },
                new Movie
                {
                    Id = 6,
                    Title = "Гаррі Поттер і Дари смерті: Частина 2",
                    Director = "Девід Єйтс",
                    Duration = 130,
                    Cast = "Деніел Редкліфф, Емма Вотсон, Руперт Грінт",
                    GenreId = 8, // Пригоди
                    ReleaseDate = DateOnly.FromDateTime(new DateTime(2011, 7, 15)),
                    Description = "Гаррі, Рон і Герміона продовжують пошук горокраксів, щоб знищити Волдеморта.",
                    MinAge = 12,
                    StatusId = 1,
                    PosterURL = "https://example.com/poster/hp7p2.jpg",
                    TrailerURL = "https://example.com/trailer/hp7p2.mp4"
                },
                new Movie
                {
                    Id = 7,
                    Title = "Аватар",
                    Director = "Джеймс Кемерон",
                    Duration = 162,
                    Cast = "Сем Вортінгтон, Зої Салдана, Сігурні Вівер",
                    GenreId = 4, // Фантастика
                    ReleaseDate = DateOnly.FromDateTime(new DateTime(2009, 12, 18)),
                    Description = "Колишній морський піхотинець відправляється на планету Пандора, де знайомиться з місцевими жителями.",
                    MinAge = 12,
                    StatusId = 1,
                    PosterURL = "https://example.com/poster/avatar.jpg",
                    TrailerURL = "https://example.com/trailer/avatar.mp4"
                },
                new Movie
                {
                    Id = 8,
                    Title = "Титанік",
                    Director = "Джеймс Кемерон",
                    Duration = 195,
                    Cast = "Леонардо ДіКапріо, Кейт Вінслет, Біллі Зейн",
                    GenreId = 7, // Мелодрама
                    ReleaseDate = DateOnly.FromDateTime(new DateTime(1997, 12, 19)),
                    Description = "Історія кохання на фоні трагедії затонулого лайнера.",
                    MinAge = 12,
                    StatusId = 1,
                    PosterURL = "https://example.com/poster/titanic.jpg",
                    TrailerURL = "https://example.com/trailer/titanic.mp4"
                },
                new Movie
                {
                    Id = 9,
                    Title = "Матриця",
                    Director = "Лана Вачовскі, Ліллі Вачовскі",
                    Duration = 136,
                    Cast = "Кіану Рівз, Лоренс Фішберн, Керрі-Енн Мосс",
                    GenreId = 4, // Фантастика
                    ReleaseDate = DateOnly.FromDateTime(new DateTime(1999, 3, 31)),
                    Description = "Хакер на ім'я Нео дізнається, що реальність — це ілюзія, і приєднується до боротьби проти машин.",
                    MinAge = 16,
                    StatusId = 1,
                    PosterURL = "https://example.com/poster/matrix.jpg",
                    TrailerURL = "https://example.com/trailer/matrix.mp4"
                },
                new Movie
                {
                    Id = 10,
                    Title = "Зоряні війни: Епізод IV – Нова надія",
                    Director = "Джордж Лукас",
                    Duration = 121,
                    Cast = "Марк Гемілл, Харрісон Форд, Керрі Фішер",
                    GenreId = 4, // Фантастика
                    ReleaseDate = DateOnly.FromDateTime(new DateTime(1977, 5, 25)),
                    Description = "Люк Скайвокер приєднується до повстанців, щоб знищити Зоряну Смерть.",
                    MinAge = 12,
                    StatusId = 1,
                    PosterURL = "https://example.com/poster/starwars4.jpg",
                    TrailerURL = "https://example.com/trailer/starwars4.mp4"
                },
                new Movie
                {
                    Id = 11,
                    Title = "Пірати Карибського моря: Прокляття «Чорної перлини»",
                    Director = "Гор Вербінскі",
                    Duration = 143,
                    Cast = "Джонні Депп, Орландо Блум, Кіра Найтлі",
                    GenreId = 8, // Пригоди
                    ReleaseDate = DateOnly.FromDateTime(new DateTime(2003, 7, 9)),
                    Description = "Джек Горобець і Вілл Тернер вирушають на пошуки проклятого золота.",
                    MinAge = 12,
                    StatusId = 1,
                    PosterURL = "https://example.com/poster/pirates1.jpg",
                    TrailerURL = "https://example.com/trailer/pirates1.mp4"
                },
                new Movie
                {
                    Id = 12,
                    Title = "Шерлок Холмс",
                    Director = "Гай Річі",
                    Duration = 128,
                    Cast = "Роберт Дауні-молодший, Джуд Лоу, Рейчел МакАдамс",
                    GenreId = 5, // Трилер
                    ReleaseDate = DateOnly.FromDateTime(new DateTime(2009, 12, 25)),
                    Description = "Шерлок Холмс і доктор Ватсон розслідують серію загадкових злочинів.",
                    MinAge = 12,
                    StatusId = 1,
                    PosterURL = "https://example.com/poster/sherlock.jpg",
                    TrailerURL = "https://example.com/trailer/sherlock.mp4"
                },
                new Movie
                {
                    Id = 13,
                    Title = "Гладіатор",
                    Director = "Рідлі Скотт",
                    Duration = 155,
                    Cast = "Рассел Кроу, Хоакін Фенікс, Конні Нільсен",
                    GenreId = 1, // Бойовик
                    ReleaseDate = DateOnly.FromDateTime(new DateTime(2000, 5, 5)),
                    Description = "Генерал Максимус стає гладіатором, щоб помститися за вбивство своєї сім'ї.",
                    MinAge = 16,
                    StatusId = 1,
                    PosterURL = "https://example.com/poster/gladiator.jpg",
                    TrailerURL = "https://example.com/trailer/gladiator.mp4"
                },
                new Movie
                {
                    Id = 14,
                    Title = "Втеча з Шоушенка",
                    Director = "Френк Дарабонт",
                    Duration = 142,
                    Cast = "Тім Роббінс, Морган Фріман, Боб Гантон",
                    GenreId = 3, // Драма
                    ReleaseDate = DateOnly.FromDateTime(new DateTime(1994, 9, 23)),
                    Description = "Двоє ув'язнених знаходять дружбу і надію в суворих умовах в'язниці.",
                    MinAge = 16,
                    StatusId = 1,
                    PosterURL = "https://example.com/poster/shawshank.jpg",
                    TrailerURL = "https://example.com/trailer/shawshank.mp4"
                },
                new Movie
                {
                    Id = 15,
                    Title = "Хрещений батько",
                    Director = "Френсіс Форд Коппола",
                    Duration = 175,
                    Cast = "Марлон Брандо, Аль Пачіно, Джеймс Каан",
                    GenreId = 3, // Драма
                    ReleaseDate = DateOnly.FromDateTime(new DateTime(1972, 3, 24)),
                    Description = "Історія сім'ї мафіозі Корлеоне, яка бореться за владу і вплив.",
                    MinAge = 18,
                    StatusId = 1,
                    PosterURL = "https://example.com/poster/godfather.jpg",
                    TrailerURL = "https://example.com/trailer/godfather.mp4"
                }
            );

            // Заповнення початковими даними для кімнат
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Name = "Зал 1", Capacity = 100 },
                new Room { Id = 2, Name = "Зал 2", Capacity = 150 }
            );

            // Заповнення початковими даними для місць
            modelBuilder.Entity<Seat>().HasData(
                new Seat { Id = 1, RoomId = 1, Number = 1, ExtraPrice = 0.00m }, // Number як int
                new Seat { Id = 2, RoomId = 1, Number = 2, ExtraPrice = 0.00m }  // Number як int
            );

            // Заповнення початковими даними для цін на фільми
            modelBuilder.Entity<MoviePrice>().HasData(
                new MoviePrice { Id = 1, MovieId = 1, Price = 150.00m }, // decimal
                new MoviePrice { Id = 2, MovieId = 2, Price = 160.00m }, // decimal
                new MoviePrice { Id = 3, MovieId = 3, Price = 140.00m },
                new MoviePrice { Id = 4, MovieId = 4, Price = 130.00m },
                new MoviePrice { Id = 5, MovieId = 5, Price = 170.00m },
                new MoviePrice { Id = 6, MovieId = 6, Price = 150.00m },
                new MoviePrice { Id = 7, MovieId = 7, Price = 160.00m },
                new MoviePrice { Id = 8, MovieId = 8, Price = 140.00m },
                new MoviePrice { Id = 9, MovieId = 9, Price = 150.00m },
                new MoviePrice { Id = 10, MovieId = 10, Price = 130.00m },
                new MoviePrice { Id = 11, MovieId = 11, Price = 140.00m },
                new MoviePrice { Id = 12, MovieId = 12, Price = 150.00m },
                new MoviePrice { Id = 13, MovieId = 13, Price = 160.00m },
                new MoviePrice { Id = 14, MovieId = 14, Price = 170.00m },
                new MoviePrice { Id = 15, MovieId = 15, Price = 180.00m }
            );

            // Заповнення початковими даними для сеансів
            modelBuilder.Entity<Session>().HasData(
                new Session
                {
                    Id = 1,
                    MoviePriceId = 1,
                    RoomId = 1,
                    Date = DateOnly.FromDateTime(new DateTime(2023, 10, 1)),
                    Time = TimeOnly.FromTimeSpan(new TimeSpan(18, 0, 0)) // TimeOnly замість TimeSpan
                },
                new Session
                {
                    Id = 2,
                    MoviePriceId = 2,
                    RoomId = 2,
                    Date = DateOnly.FromDateTime(new DateTime(2023, 10, 1)),
                    Time = TimeOnly.FromTimeSpan(new TimeSpan(20, 0, 0)) // TimeOnly замість TimeSpan
                }
            );
        }
    }
}