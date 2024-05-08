using Microsoft.AspNetCore.Mvc;
using Task = AspCore_Api1.Models.Task;

namespace AspCore_Api1.Controllers
{
    public class TaskController : Controller
    {
        private static List<Task> tasks = new List<Task>()
        {
            new Task {Id = Guid.NewGuid(), Tilte = "Task 1", IsCompleted = true},
            new Task {Id = Guid.NewGuid(), Tilte = "Task 2", IsCompleted = false},
            new Task {Id = Guid.NewGuid(), Tilte = "Task 3", IsCompleted=false},
        };
        [HttpGet]
        public IEnumerable<Task> GetTask()
        {
            return tasks;
        }
        [HttpGet("{id}")]
        public IActionResult GetTask(Guid id)
        {
            var task = tasks.Find(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
        [HttpPost("{id}")]
        public IActionResult CreateTask(Task task)
        {
            task.Id = Guid.NewGuid();
            tasks.Add(task);
            return CreatedAtAction (nameof(GetTask), new {id = task.Id}, task);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateTask(Guid id, Task updatedTask)
        {
            var task = tasks.Find(t =>t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            task.Tilte = updatedTask.Tilte;
            task.IsCompleted = updatedTask.IsCompleted;
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(Guid id)
        {
            var task = tasks.Find(t =>t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            tasks.Remove(task);
            return NoContent();
        }
        [HttpPost("Bulk")]
        public ActionResult<IEnumerable<Task>> BulkAddTasks(IEnumerable<Task> newtasks)
        {
            var addedTasks = new List<Task>();
            foreach (var task in newtasks)
            {
                task.Id = Guid.NewGuid();
                tasks.Add(task);
                addedTasks.Add(task);
            }
            return CreatedAtAction(nameof(BulkAddTasks), addedTasks);
        }
        [HttpDelete("Bulk")]
        public IActionResult BulkDeleteTasks(IEnumerable<Guid> ids)
        {
            var tasksToDelete = tasks.Where(tasks => ids.Contains(tasks.Id)).ToList();
            if (tasksToDelete.Count == 0)
            {
                return NotFound();
            }
            tasks.RemoveAll(tasks => ids.Contains(tasks.Id));
            return NoContent();

        }
    }
}
