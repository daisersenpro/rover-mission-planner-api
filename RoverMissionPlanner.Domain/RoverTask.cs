using System;

namespace RoverMissionPlanner.Domain
{
    public class RoverTask
    {
        public Guid Id { get; set; }
        public string RoverName { get; set; } = string.Empty;
        public TaskType TaskType { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime StartsAt { get; set; } // UTC
        public int DurationMinutes { get; set; }
        public TaskStatus Status { get; set; }
    }
}