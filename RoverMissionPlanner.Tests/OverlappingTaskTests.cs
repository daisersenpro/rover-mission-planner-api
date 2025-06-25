using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RoverMissionPlanner.Domain;
using RoverMissionPlanner.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace RoverMissionPlanner.Tests
{
    public class OverlappingTaskTests
    {
        private RoverTaskRepository GetRepositoryWithTasks(List<RoverTask> tasks)
        {
            var options = new DbContextOptionsBuilder<RoverMissionPlannerDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new RoverMissionPlannerDbContext(options);
            context.RoverTasks.AddRange(tasks);
            context.SaveChanges();
            return new RoverTaskRepository(context);
        }

        [Fact]
        public async Task NoOverlap_ReturnsFalse()
        {
            var tasks = new List<RoverTask>
            {
                new RoverTask
                {
                    Id = Guid.NewGuid(),
                    RoverName = "Curiosity",
                    StartsAt = new DateTime(2025, 6, 25, 10, 0, 0),
                    DurationMinutes = 60
                }
            };
            var repo = GetRepositoryWithTasks(tasks);
            // Nueva tarea fuera del rango
            var result = await repo.HasOverlappingTaskAsync("Curiosity", new DateTime(2025, 6, 25, 12, 0, 0), 30);
            Assert.False(result);
        }

        [Fact]
        public async Task Overlap_ReturnsTrue()
        {
            var tasks = new List<RoverTask>
            {
                new RoverTask
                {
                    Id = Guid.NewGuid(),
                    RoverName = "Curiosity",
                    StartsAt = new DateTime(2025, 6, 25, 10, 0, 0),
                    DurationMinutes = 60
                }
            };
            var repo = GetRepositoryWithTasks(tasks);
            // Nueva tarea que se solapa
            var result = await repo.HasOverlappingTaskAsync("Curiosity", new DateTime(2025, 6, 25, 10, 30, 0), 60);
            Assert.True(result);
        }

        [Fact]
        public async Task Overlap_DifferentRover_ReturnsFalse()
        {
            var tasks = new List<RoverTask>
            {
                new RoverTask
                {
                    Id = Guid.NewGuid(),
                    RoverName = "Curiosity",
                    StartsAt = new DateTime(2025, 6, 25, 10, 0, 0),
                    DurationMinutes = 60
                }
            };
            var repo = GetRepositoryWithTasks(tasks);
            // Nueva tarea para otro rover
            var result = await repo.HasOverlappingTaskAsync("Perseverance", new DateTime(2025, 6, 25, 10, 30, 0), 60);
            Assert.False(result);
        }
    }
} 