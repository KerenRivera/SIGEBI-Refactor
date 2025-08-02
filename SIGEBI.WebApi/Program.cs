using Microsoft.EntityFrameworkCore;
using SIGEBI.Infrastructure.Persistence.Repositories;
using SIGEBI.Infrastructure.Persistence.Context;
using SIGEBI.Application.Services;
using SIGEBI.Infrastructure.Interfaces;
using SIGEBI.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registering the DbContext with SQLite
builder.Services.AddDbContext<SIGEBIContext>(options =>
    options.UseSqlite("Data Source=sigebi.db"));

// Registering repositories and services
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
//builder.Services.AddScoped<BookService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ReservationService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => "Welcome to SIGEBI API!");

app.Run();
