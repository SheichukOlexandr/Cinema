using DataAccess.Contexts;
using DataAccess.Repositories;
using DataAccess.Repositories.Interfaces;
using DataAccess.Repositories.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Services;
using BusinessLogic.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Додавання DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection"))
);

// Додавання репозиторіїв
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Don't use AppDomain.CurrentDomain.GetAssemblies(); It will break scaffolding. (e.g. autogeneration of controllers)
builder.Services.AddAutoMapper(typeof(ApplicationProfile).Assembly);

builder.Services.AddScoped<GenreService>();
builder.Services.AddScoped<MovieStatusService>();
builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<MoviePriceService>();

builder.Services.AddScoped<RoomService>();
builder.Services.AddScoped<SeatService>();
builder.Services.AddScoped<ReservationStatusService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
