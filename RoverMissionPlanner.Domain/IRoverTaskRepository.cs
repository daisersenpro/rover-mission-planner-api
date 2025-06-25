using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoverMissionPlanner.Domain
{
    public interface IRoverTaskRepository
    {
        Task AddAsync(RoverTask task);
        Task<List<RoverTask>> GetByRoverAndDateAsync(string roverName, DateTime date);
        Task<bool> HasOverlappingTaskAsync(string roverName, DateTime startsAt, int durationMinutes, Guid? excludeTaskId = null);
        Task<int> GetTotalPlannedMinutesAsync(string roverName, DateTime date);
        // Puedes agregar más métodos según lo necesites
    }
}