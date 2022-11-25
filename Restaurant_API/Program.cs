using Microsoft.EntityFrameworkCore;
using Restaurant_API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Creaci�n de la configuraci�n de cadena de conexi�n contra el entorno
var conn = @"SERVER=.\SQLEXPRESS;DATABASE=Restaurant;INTEGRATED SECURITY=TRUE; User Id=;Password=";
builder.Services.AddDbContext<RestaurantContext>(options => options.UseSqlServer(conn));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
