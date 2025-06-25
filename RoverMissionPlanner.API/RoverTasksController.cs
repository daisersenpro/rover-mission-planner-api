using Microsoft.AspNetCore.Mvc;
using RoverMissionPlanner.Application;
using RoverMissionPlanner.Domain;

namespace RoverMissionPlanner.API
{
    [ApiController]
    [Route("rovers/{roverName}/tasks")]
    public class RoverTasksController : ControllerBase
    {
        private readonly RoverTaskService _service;

        public RoverTasksController(RoverTaskService service)
        {
            _service = service;
        }

        // POST /rovers/{roverName}/tasks
        [HttpPost]
        public async Task<IActionResult> CreateTask(string roverName, [FromBody] RoverTask task)
        {
            if (roverName != task.RoverName)
                return BadRequest("El nombre del rover en la URL y en el cuerpo no coinciden.");

            try
            {
                await _service.CreateTaskAsync(task);
                return CreatedAtAction(nameof(CreateTask), new { roverName = task.RoverName, id = task.Id }, task);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); // 409 si hay solapamiento
            }
        }

        // GET /rovers/{roverName}/tasks?date=YYYY-MM-DD
        [HttpGet]
        public async Task<IActionResult> GetTasks(string roverName, [FromQuery] DateTime date)
        {
            var tasks = await _service.GetTasksForDayAsync(roverName, date);
            return Ok(tasks);
        }

        // GET /rovers/{roverName}/utilization?date=YYYY-MM-DD
        [HttpGet("~/rovers/{roverName}/utilization")]
        public async Task<IActionResult> GetUtilization(string roverName, [FromQuery] DateTime date)
        {
            var utilization = await _service.GetUtilizationAsync(roverName, date);
            return Ok(new { utilization = Math.Round(utilization, 2) });
        }
    }
}