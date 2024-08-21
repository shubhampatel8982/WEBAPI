// SubTasksController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskData.Data;
using TaskData.Models;

namespace TaskApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubTasksController : ControllerBase
    {
        private readonly TaskDbContext _context;

        public SubTasksController(TaskDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubTask>>> GetSubTasks()
        {
            return await _context.SubTasks.Include(st => st.Task).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubTask>> GetSubTask(int id)
        {
            var subTask = await _context.SubTasks.FindAsync(id);

            if (subTask == null)
            {
                return NotFound();
            }

            return subTask;
        }

        [HttpPost]
        public async Task<ActionResult<SubTask>> PostSubTask(SubTask subTask)
        {
            _context.SubTasks.Add(subTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSubTask), new { id = subTask.SubTaskId }, subTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubTask(int id, SubTask subTask)
        {
            if (id != subTask.SubTaskId)
            {
                return BadRequest();
            }

            _context.Entry(subTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubTaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubTask(int id)
        {
            var subTask = await _context.SubTasks.FindAsync(id);
            if (subTask == null)
            {
                return NotFound();
            }

            _context.SubTasks.Remove(subTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubTaskExists(int id)
        {
            return _context.SubTasks.Any(e => e.SubTaskId == id);
        }
    }
}
