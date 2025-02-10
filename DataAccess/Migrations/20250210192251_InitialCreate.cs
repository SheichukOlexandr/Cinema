using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReservationStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Director = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    Cast = table.Column<string>(type: "text", nullable: false),
                    GenreId = table.Column<int>(type: "integer", nullable: false),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    MinAge = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<double>(type: "double precision", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    PosterURL = table.Column<string>(type: "text", nullable: false),
                    TrailerURL = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movies_MovieStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "MovieStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoomId = table.Column<int>(type: "integer", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    ExtraPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "UserStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoviePrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MovieId = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviePrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoviePrices_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MoviePriceId = table.Column<int>(type: "integer", nullable: false),
                    RoomId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_MoviePrices_MoviePriceId",
                        column: x => x.MoviePriceId,
                        principalTable: "MoviePrices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sessions_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SessionId = table.Column<int>(type: "integer", nullable: false),
                    SeatId = table.Column<int>(type: "integer", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_ReservationStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ReservationStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservations_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Бойовик" },
                    { 2, "Комедія" },
                    { 3, "Драма" },
                    { 4, "Фантастика" },
                    { 5, "Трилер" },
                    { 6, "Жахи" },
                    { 7, "Мелодрама" },
                    { 8, "Пригоди" },
                    { 9, "Анімація" },
                    { 10, "Документальний" }
                });

            migrationBuilder.InsertData(
                table: "MovieStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "В прокаті" },
                    { 2, "Скоро у кіно" },
                    { 3, "Архівний" }
                });

            migrationBuilder.InsertData(
                table: "ReservationStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Створено" },
                    { 2, "Підтверджено" },
                    { 3, "Завершено" },
                    { 4, "Скасовано" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Capacity", "Name" },
                values: new object[,]
                {
                    { 1, 100, "Зал 1" },
                    { 2, 150, "Зал 2" }
                });

            migrationBuilder.InsertData(
                table: "UserStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Активний" },
                    { 2, "Адміністратор" },
                    { 3, "Заблокований" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Cast", "Description", "Director", "Duration", "GenreId", "MinAge", "PosterURL", "Rating", "ReleaseDate", "StatusId", "Title", "TrailerURL" },
                values: new object[,]
                {
                    { 1, "Леонардо ДіКапріо, Джозеф Гордон-Левітт", "Злодій, який викрадає корпоративні таємниці за допомогою технології обміну снами.", "Крістофер Нолан", 148, 1, 13, "https://example.com/poster1.jpg", 8.8000000000000007, new DateOnly(2010, 7, 16), 1, "Початок", "https://example.com/trailer1.mp4" },
                    { 2, "Меттью МакКонахі, Енн Гетевей", "Подорож крізь простір і час для порятунку людства.", "Крістофер Нолан", 169, 5, 12, "https://example.com/poster2.jpg", 8.5999999999999996, new DateOnly(2014, 11, 7), 1, "Інтерстеллар", "https://example.com/trailer2.mp4" },
                    { 3, "Крістіан Бейл, Гіт Леджер", "Бетмен бореться з хаосом, який створює Джокер.", "Крістофер Нолан", 152, 1, 13, "https://example.com/poster3.jpg", 9.0, new DateOnly(2008, 7, 18), 1, "Темний лицар", "https://example.com/trailer3.mp4" },
                    { 4, "Він Дізель, Пол Вокер", "Команда вуличних гонщиків стикається з новими викликами.", "Джеймс Ван", 137, 1, 16, "https://example.com/poster4.jpg", 7.0999999999999996, new DateOnly(2015, 4, 3), 1, "Форсаж 7", "https://example.com/trailer4.mp4" },
                    { 5, "Роберт Дауні-молодший, Кріс Еванс", "Фінальна битва Месників проти Таноса.", "Джо Руссо, Ентоні Руссо", 181, 1, 12, "https://example.com/poster5.jpg", 8.4000000000000004, new DateOnly(2019, 4, 26), 1, "Месники: Фінал", "https://example.com/trailer5.mp4" },
                    { 6, "Хоакін Фенікс", "Історія походження одного з найвідоміших лиходіїв коміксів.", "Тодд Філліпс", 122, 3, 18, "https://example.com/poster6.jpg", 8.4000000000000004, new DateOnly(2019, 10, 4), 1, "Джокер", "https://example.com/trailer6.mp4" },
                    { 7, "Леонардо ДіКапріо, Кейт Вінслет", "Історія кохання на тлі катастрофи Титаніка.", "Джеймс Кемерон", 195, 3, 12, "https://example.com/poster7.jpg", 7.7999999999999998, new DateOnly(1997, 12, 19), 1, "Титанік", "https://example.com/trailer7.mp4" },
                    { 8, "Деніел Редкліфф, Емма Вотсон", "Перша частина пригод Гаррі Поттера у світі чарівників.", "Кріс Коламбус", 152, 5, 10, "https://example.com/poster8.jpg", 7.5999999999999996, new DateOnly(2001, 11, 16), 1, "Гаррі Поттер і філософський камінь", "https://example.com/trailer8.mp4" },
                    { 9, "Бен Берт, Елісса Найт", "Історія маленького робота, який змінив світ.", "Ендрю Стентон", 98, 5, 6, "https://example.com/poster9.jpg", 8.4000000000000004, new DateOnly(2008, 6, 27), 1, "Волл-і", "https://example.com/trailer9.mp4" },
                    { 10, "Дональд Гловер, Бейонсе", "Ремейк класичного мультфільму про пригоди Сімби.", "Джон Фавро", 118, 3, 6, "https://example.com/poster10.jpg", 6.9000000000000004, new DateOnly(2019, 7, 19), 1, "Король Лев", "https://example.com/trailer10.mp4" },
                    { 11, "Сон Кан Хо, Лі Сон Гюн", "Історія про соціальну нерівність через призму однієї родини.", "Пон Джун Хо", 132, 3, 16, "https://example.com/poster11.jpg", 8.5999999999999996, new DateOnly(2019, 5, 30), 1, "Паразити", "https://example.com/trailer11.mp4" },
                    { 12, "Тімоті Шаламе, Зендея", "Епічна науково-фантастична сага про боротьбу за виживання.", "Дені Вільньов", 155, 5, 13, "https://example.com/poster12.jpg", 8.0999999999999996, new DateOnly(2021, 10, 22), 1, "Дюна", "https://example.com/trailer12.mp4" },
                    { 13, "Марк Гемілл, Харрісон Форд", "Перший епізод культової космічної саги.", "Джордж Лукас", 121, 5, 10, "https://example.com/poster13.jpg", 8.5999999999999996, new DateOnly(1977, 5, 25), 1, "Зоряні війни: Епізод IV - Нова надія", "https://example.com/trailer13.mp4" },
                    { 14, "Кіану Рівз, Лоренс Фішберн", "Класика наукової фантастики про боротьбу зі штучним інтелектом.", "Лана Вачовскі, Ліллі Вачовскі", 136, 5, 16, "https://example.com/poster14.jpg", 8.6999999999999993, new DateOnly(1999, 3, 31), 1, "Матриця", "https://example.com/trailer14.mp4" },
                    { 15, "Елайджа Вуд, Вігго Мортенсен", "Перша частина епічної трилогії за мотивами творів Толкіна.", "Пітер Джексон", 178, 3, 12, "https://example.com/poster15.jpg", 8.8000000000000007, new DateOnly(2001, 12, 19), 1, "Володар перснів: Хранителі Персня", "https://example.com/trailer15.mp4" }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "ExtraPrice", "Number", "RoomId" },
                values: new object[,]
                {
                    { 1, 0.00m, 1, 1 },
                    { 2, 0.00m, 2, 1 },
                    { 3, 50.00m, 1, 2 },
                    { 4, 50.00m, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "StatusId" },
                values: new object[,]
                {
                    { 1, "user@cinema.com", "Mike", "Brown", "4f9f10b304cfe9b2b11fcb1387f694e18f08ea358c7e9f567434d3ad6cbd7fc4", "+38050221131", 1 },
                    { 2, "admin@cinema.com", "John", "Sandres", "4f9f10b304cfe9b2b11fcb1387f694e18f08ea358c7e9f567434d3ad6cbd7fc4", "+38095221141", 2 }
                });

            migrationBuilder.InsertData(
                table: "MoviePrices",
                columns: new[] { "Id", "MovieId", "Price" },
                values: new object[,]
                {
                    { 1, 1, 200.00m },
                    { 2, 2, 210.00m },
                    { 3, 3, 220.00m },
                    { 4, 4, 230.00m },
                    { 5, 5, 240.00m },
                    { 6, 6, 250.00m },
                    { 7, 7, 260.00m },
                    { 8, 8, 270.00m },
                    { 9, 9, 280.00m },
                    { 10, 10, 290.00m },
                    { 11, 11, 300.00m },
                    { 12, 12, 310.00m },
                    { 13, 13, 320.00m },
                    { 14, 14, 330.00m },
                    { 15, 15, 340.00m }
                });

            migrationBuilder.InsertData(
                table: "Sessions",
                columns: new[] { "Id", "Date", "MoviePriceId", "RoomId", "Time" },
                values: new object[,]
                {
                    { 10, new DateOnly(2025, 3, 1), 1, 1, new TimeOnly(10, 0, 0) },
                    { 11, new DateOnly(2025, 3, 2), 1, 2, new TimeOnly(13, 0, 0) },
                    { 12, new DateOnly(2025, 3, 3), 1, 1, new TimeOnly(16, 0, 0) },
                    { 20, new DateOnly(2025, 3, 1), 2, 1, new TimeOnly(10, 0, 0) },
                    { 21, new DateOnly(2025, 3, 2), 2, 2, new TimeOnly(13, 0, 0) },
                    { 22, new DateOnly(2025, 3, 3), 2, 1, new TimeOnly(16, 0, 0) },
                    { 30, new DateOnly(2025, 3, 1), 3, 1, new TimeOnly(10, 0, 0) },
                    { 31, new DateOnly(2025, 3, 2), 3, 2, new TimeOnly(13, 0, 0) },
                    { 32, new DateOnly(2025, 3, 3), 3, 1, new TimeOnly(16, 0, 0) },
                    { 40, new DateOnly(2025, 3, 1), 4, 1, new TimeOnly(10, 0, 0) },
                    { 41, new DateOnly(2025, 3, 2), 4, 2, new TimeOnly(13, 0, 0) },
                    { 42, new DateOnly(2025, 3, 3), 4, 1, new TimeOnly(16, 0, 0) },
                    { 50, new DateOnly(2025, 3, 1), 5, 1, new TimeOnly(10, 0, 0) },
                    { 51, new DateOnly(2025, 3, 2), 5, 2, new TimeOnly(13, 0, 0) },
                    { 52, new DateOnly(2025, 3, 3), 5, 1, new TimeOnly(16, 0, 0) },
                    { 60, new DateOnly(2025, 3, 1), 6, 1, new TimeOnly(10, 0, 0) },
                    { 61, new DateOnly(2025, 3, 2), 6, 2, new TimeOnly(13, 0, 0) },
                    { 62, new DateOnly(2025, 3, 3), 6, 1, new TimeOnly(16, 0, 0) },
                    { 70, new DateOnly(2025, 3, 1), 7, 1, new TimeOnly(10, 0, 0) },
                    { 71, new DateOnly(2025, 3, 2), 7, 2, new TimeOnly(13, 0, 0) },
                    { 72, new DateOnly(2025, 3, 3), 7, 1, new TimeOnly(16, 0, 0) },
                    { 80, new DateOnly(2025, 3, 1), 8, 1, new TimeOnly(10, 0, 0) },
                    { 81, new DateOnly(2025, 3, 2), 8, 2, new TimeOnly(13, 0, 0) },
                    { 82, new DateOnly(2025, 3, 3), 8, 1, new TimeOnly(16, 0, 0) },
                    { 90, new DateOnly(2025, 3, 1), 9, 1, new TimeOnly(10, 0, 0) },
                    { 91, new DateOnly(2025, 3, 2), 9, 2, new TimeOnly(13, 0, 0) },
                    { 92, new DateOnly(2025, 3, 3), 9, 1, new TimeOnly(16, 0, 0) },
                    { 100, new DateOnly(2025, 3, 1), 10, 1, new TimeOnly(10, 0, 0) },
                    { 101, new DateOnly(2025, 3, 2), 10, 2, new TimeOnly(13, 0, 0) },
                    { 102, new DateOnly(2025, 3, 3), 10, 1, new TimeOnly(16, 0, 0) },
                    { 110, new DateOnly(2025, 3, 1), 11, 1, new TimeOnly(10, 0, 0) },
                    { 111, new DateOnly(2025, 3, 2), 11, 2, new TimeOnly(13, 0, 0) },
                    { 112, new DateOnly(2025, 3, 3), 11, 1, new TimeOnly(16, 0, 0) },
                    { 120, new DateOnly(2025, 3, 1), 12, 1, new TimeOnly(10, 0, 0) },
                    { 121, new DateOnly(2025, 3, 2), 12, 2, new TimeOnly(13, 0, 0) },
                    { 122, new DateOnly(2025, 3, 3), 12, 1, new TimeOnly(16, 0, 0) },
                    { 130, new DateOnly(2025, 3, 1), 13, 1, new TimeOnly(10, 0, 0) },
                    { 131, new DateOnly(2025, 3, 2), 13, 2, new TimeOnly(13, 0, 0) },
                    { 132, new DateOnly(2025, 3, 3), 13, 1, new TimeOnly(16, 0, 0) },
                    { 140, new DateOnly(2025, 3, 1), 14, 1, new TimeOnly(10, 0, 0) },
                    { 141, new DateOnly(2025, 3, 2), 14, 2, new TimeOnly(13, 0, 0) },
                    { 142, new DateOnly(2025, 3, 3), 14, 1, new TimeOnly(16, 0, 0) },
                    { 150, new DateOnly(2025, 3, 1), 15, 1, new TimeOnly(10, 0, 0) },
                    { 151, new DateOnly(2025, 3, 2), 15, 2, new TimeOnly(13, 0, 0) },
                    { 152, new DateOnly(2025, 3, 3), 15, 1, new TimeOnly(16, 0, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoviePrices_MovieId",
                table: "MoviePrices",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_GenreId",
                table: "Movies",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_StatusId",
                table: "Movies",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SeatId",
                table: "Reservations",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SessionId",
                table: "Reservations",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_StatusId",
                table: "Reservations",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_RoomId",
                table: "Seats",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_MoviePriceId",
                table: "Sessions",
                column: "MoviePriceId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_RoomId",
                table: "Sessions",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StatusId",
                table: "Users",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "ReservationStatuses");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "MoviePrices");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "UserStatuses");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "MovieStatuses");
        }
    }
}
