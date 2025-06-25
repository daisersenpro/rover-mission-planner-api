using Microsoft.EntityFrameworkCore; // Importa EF Core
using RoverMissionPlanner.Domain;    // Importa las entidades del dominio

namespace RoverMissionPlanner.Infrastructure
{
    // Esta clase representa la conexión a la base de datos y define las tablas
    public class RoverMissionPlannerDbContext : DbContext
    {
        // El constructor recibe las opciones de configuración (cadena de conexión, etc.)
        public RoverMissionPlannerDbContext(DbContextOptions<RoverMissionPlannerDbContext> options)
            : base(options)
        {
        }

        // Esta propiedad representa la tabla RoverTasks en la base de datos
        public DbSet<RoverTask> RoverTasks { get; set; }
        // DbSet es una colección de entidades que EF Core rastrea y guarda en la base de datos
    }
}