using RoverMissionPlanner.Domain;
using Microsoft.EntityFrameworkCore;

namespace RoverMissionPlanner.Infrastructure
{
    // Implementa el acceso a datos usando EF Core
    public class RoverTaskRepository : IRoverTaskRepository
    {
        private readonly RoverMissionPlannerDbContext _context;

        public RoverTaskRepository(RoverMissionPlannerDbContext context)
        {
            _context = context;
        }

        // Agrega una nueva tarea
        public async Task AddAsync(RoverTask task)
        {
            _context.RoverTasks.Add(task);
            await _context.SaveChangesAsync();
        }

        // Obtiene las tareas de un rover para una fecha específica
        public async Task<List<RoverTask>> GetByRoverAndDateAsync(string roverName, DateTime date)
        {
            var start = date.Date;
            var end = start.AddDays(1);

            return await _context.RoverTasks
                .Where(t => t.RoverName == roverName && t.StartsAt >= start && t.StartsAt < end)
                .OrderBy(t => t.StartsAt)
                .ToListAsync();
        }

        // Verifica si existe solapamiento de tareas para un rover
        public async Task<bool> HasOverlappingTaskAsync(string roverName, DateTime startsAt, int durationMinutes, Guid? excludeTaskId = null)
        {
            var endsAt = startsAt.AddMinutes(durationMinutes);

            return await _context.RoverTasks
                .Where(t => t.RoverName == roverName && (excludeTaskId == null || t.Id != excludeTaskId))
                .AnyAsync(t =>
                    (startsAt < t.StartsAt.AddMinutes(t.DurationMinutes)) &&
                    (endsAt > t.StartsAt)
                );
        }

        // Suma los minutos planificados de un rover en un día
        public async Task<int> GetTotalPlannedMinutesAsync(string roverName, DateTime date)
        {
            var start = date.Date;
            var end = start.AddDays(1);

            return await _context.RoverTasks
                .Where(t => t.RoverName == roverName && t.StartsAt >= start && t.StartsAt < end)
                .SumAsync(t => t.DurationMinutes);
        }
    }
}