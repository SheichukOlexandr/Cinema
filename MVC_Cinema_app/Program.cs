using DataAccess.Contexts;
using DataAccess.Repositories;
using DataAccess.Repositories.Interfaces;
using DataAccess.Repositories.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using BusinessLogic.Helpers;
using BusinessLogic.DTOs;
using System.Security.Claims;
using MVC_Cinema_app;

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

builder.Services.AddAutoMapper(typeof(ApplicationProfile).Assembly);

builder.Services.AddScoped<GenreService>();
builder.Services.AddScoped<MovieStatusService>();
builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<MoviePriceService>();

builder.Services.AddScoped<RoomService>();
builder.Services.AddScoped<SeatService>();
builder.Services.AddScoped<SessionService>();

builder.Services.AddScoped<ReservationStatusService>();
builder.Services.AddScoped<ReservationService>();

builder.Services.AddScoped<UserService>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Index";
        options.LogoutPath = "/Auth/Logout";
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(Policies.DefaultUserPolicy, policy =>
        policy.RequireClaim(ClaimTypes.Role, UserStatusDTO.Active, UserStatusDTO.Admin))
    .AddPolicy(Policies.AdminUserPolicy, policy =>
        policy.RequireClaim(ClaimTypes.Role, UserStatusDTO.Admin));

builder.Services.AddControllers();

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

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

namespace MVC_Cinema_app
{
    class Policies {
        public const string DefaultUserPolicy = "DefaultUser";
        public const string AdminUserPolicy = "AdminUser";
    };
}
