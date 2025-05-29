using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Services;

using Microsoft.AspNetCore.Http;
using Task = TaskManager.Models.Task;

namespace TaskManager.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskService taskService;
        public TaskController(TaskService taskService)
        {
            this.taskService = taskService;
        }
        [HttpGet("GetAllTasks")]
        public ActionResult<List<Task>> GetAlltask()
        {
            List<Task> Tasks = taskService.GetTasks();
            if (Tasks == null || Tasks.Count == 0)
            {
                return NotFound("No Tasks found");
            }
            else
            {
                return Ok(Tasks);
            }
        }
        [HttpGet("GetCompletedTasks")]
        public ActionResult<List<Task>> GetCompletedT()
        {
            return taskService.GetCompletedTask();
        }

        [HttpGet("GetSingleTask")]
        public ActionResult<Task> GetTask(int id)
        {
            Task Task = taskService.GetTask(id);
            if (Task == null)
            {
                return NotFound("Task Not found");
            }
            else
            {
                return Ok(Task);
            }
        }
        [HttpPost]
        public IActionResult CreateTask(Task t)
        {
            taskService.AddTask(t);
            return Ok("task added successfully");
        }

        [HttpPut]
        public ActionResult UpdateTask(Task t)
        {
            int status = taskService.UpdateTassk(t);
            if (status == -1)
            {
                return NotFound("Task Not FOund");
            }
            else if (status == 1)
            {
                return Ok("Task updated successfully");
            }
            else
            {
                return BadRequest("Bad request");
            }
        }

        [HttpDelete]
        public IActionResult DeleteTask(int id)
        {
            int status = taskService.DeleteTask(id);
            if (status == -1)
            {
                return NotFound("Task Not found");
            }
            else if (status == 1)
            {
                return Ok("Task Deleted Successfully");
            }
            else
            {
                return BadRequest("Bad request");
            }
        }
    }
}

