using RoverMissionPlanner.Domain;

namespace RoverMissionPlanner.Application
{
    // Servicio de aplicación para manejar la lógica de negocio de las tareas del rover
    public class RoverTaskService
    {
        private readonly IRoverTaskRepository _repository;

        public RoverTaskService(IRoverTaskRepository repository)
        {
            _repository = repository;
        }

        // Crea una nueva tarea, validando que no se solape con otra
        public async Task CreateTaskAsync(RoverTask task)
        {
            // Validar solapamiento
            bool overlap = await _repository.HasOverlappingTaskAsync(
                task.RoverName, task.StartsAt, task.DurationMinutes);

            if (overlap)
                throw new InvalidOperationException("La tarea se solapa con otra existente.");

            await _repository.AddAsync(task);
        }

        // Obtiene las tareas de un rover para un día específico
        public async Task<List<RoverTask>> GetTasksForDayAsync(string roverName, DateTime date)
        {
            return await _repository.GetByRoverAndDateAsync(roverName, date);
        }

        // Calcula el porcentaje de utilización del rover en un día
        public async Task<double> GetUtilizationAsync(string roverName, DateTime date)
        {
            int totalMinutes = await _repository.GetTotalPlannedMinutesAsync(roverName, date);
            return (double)totalMinutes / 1440.0 * 100.0;
        }
    }
}