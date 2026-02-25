using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniJiraApi.Data;
using MiniJiraApi.Models;

namespace MiniJiraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var tasks = _context.Tasks.Include(t => t.Project).ToList();
            return Ok(tasks);
        }

        [HttpGet("project/{projectId}")]
        public IActionResult GetByProject(int projectId)
        {
            var tasks = _context.Tasks
                .Where(t => t.ProjectId == projectId)
                .ToList();

            return Ok(tasks);
        }


        [HttpPost]

        public IActionResult Create(TaskItem task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return Ok(task);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateStatus(int id, [FromBody] string status)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
                return NotFound();

            task.Status = status;
            _context.SaveChanges();

            return Ok(task);
        }
        [HttpPut("edit/{id}")]
        public IActionResult EditTask(int id, [FromBody] UpdateTaskTitleDto dto)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
                return NotFound();

            task.Title = dto.Title;
            _context.SaveChanges();

            return Ok(task);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
                return NotFound();

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return Ok();
        }


    }
}