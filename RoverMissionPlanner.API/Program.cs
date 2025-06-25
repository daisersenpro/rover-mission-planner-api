using RoverMissionPlanner.Infrastructure;
using RoverMissionPlanner.Domain;
using RoverMissionPlanner.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer(); // Necesario para Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rover API", Version = "v1" });
});

builder.Services.AddControllers(); // <-- Agregado para soportar controladores
builder.Services.AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<RoverTaskValidator>();
});

// Agrega el DbContext y la cadena de conexión SQLite
builder.Services.AddDbContext<RoverMissionPlannerDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inyección de dependencias para repositorio y servicio
builder.Services.AddScoped<IRoverTaskRepository, RoverTaskRepository>();
builder.Services.AddScoped<RoverTaskService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Registrar el middleware global de excepciones
app.UseMiddleware<RoverMissionPlanner.API.ExceptionMiddleware>();

app.UseHttpsRedirection();

app.MapControllers(); // <-- Agregado para mapear los controladores

// ...tus endpoints existentes...

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}