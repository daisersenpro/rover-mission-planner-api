using RoverMissionPlanner.Infrastructure;
using RoverMissionPlanner.Domain;
using RoverMissionPlanner.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using RoverMissionPlanner.API; // <-- AGREGA ESTA LÍNEA

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer(); // Necesario para Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rover API", Version = "v1" });
});

builder.Services.AddControllers(); // <-- Agregado para soportar controladores
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<RoverTaskValidator>();

// Agrega el DbContext y la cadena de conexión SQLite
builder.Services.AddDbContext<RoverMissionPlannerDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inyección de dependencias para repositorio y servicio
builder.Services.AddScoped<IRoverTaskRepository, RoverTaskRepository>();
builder.Services.AddScoped<RoverTaskService>();

// Habilita CORS para permitir peticiones desde el frontend Angular (puerto 4200)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev",
        policy => policy
            .WithOrigins("http://localhost:4200") // Origen del frontend Angular
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Registrar el middleware global de excepciones
app.UseMiddleware<RoverMissionPlanner.API.ExceptionMiddleware>();

// Usa la política de CORS definida arriba antes de los controladores y redirección HTTPS
app.UseCors("AllowAngularDev");

app.UseHttpsRedirection();

app.MapControllers(); // <-- Agregado para mapear los controladores

// ...tus endpoints existentes...

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}