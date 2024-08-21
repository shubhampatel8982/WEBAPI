using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskApi.Data;
using TaskApi.Models;
using Task = TaskApi.Models.Task;

namespace TaskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskRepository _taskRepository;

        public TaskController(TaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        // GET: api/task
        [HttpGet]
        public ActionResult<IEnumerable<Task>> GetTasks()
        {
            var tasks = _taskRepository.GetAllTasks();
            return Ok(tasks);
        }

        // GET: api/task/{id}
        [HttpGet("{id}")]
        public ActionResult<Task> GetTask(int id)
        {
            var task = _taskRepository.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        // POST: api/task
        [HttpPost]
        public ActionResult<Task> PostTask([FromBody] Task task)
        {
            if (task == null)
            {
                return BadRequest();
            }

            _taskRepository.AddTask(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.TaskID }, task);
        }

        // PUT: api/task/{id}
        [HttpPut("{id}")]
        public IActionResult PutTask(int id, [FromBody] Task task)
        {
            if (id != task.TaskID)
            {
                return BadRequest();
            }

            var existingTask = _taskRepository.GetTaskById(id);
            if (existingTask == null)
            {
                return NotFound();
            }

            _taskRepository.UpdateTask(task);
            return NoContent();
        }

        // DELETE: api/task/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var existingTask = _taskRepository.GetTaskById(id);
            if (existingTask == null)
            {
                return NotFound();
            }

            _taskRepository.DeleteTask(id);
            return NoContent();
        }
    }
}
