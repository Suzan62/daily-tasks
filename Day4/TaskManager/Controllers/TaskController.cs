using Microsoft.AspNetCore.Mvc;


using Microsoft.AspNetCore.Http;
using TaskManager.Services;
using Tasks.DataAccess.Models;
using Tasks.services.Services;


namespace TaskManager.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TasksServices _taskService;
        public TaskController(TasksServices taskService)
        {
            _taskService = taskService;
        }
        [HttpGet("GetAllTasks")]
        public ActionResult<List<Tassk>> GetAlltask()
        {
            
            List<Tassk> T = _taskService.GetTasks();
            if (T == null || T.Count == 0)
            {
                return NotFound("No Tasks found");
            }
            else
            {
                return Ok(T);
            }
        }
        [HttpGet("GetCompletedTasks")]
        public ActionResult<List<Tassk>> GetCompletedT()
        {
            return _taskService.GetCompletedTask();
            
        }

        [HttpGet("GetSingleTask")]
        public ActionResult<Tassk> GetTask(int id)
        {
            Tassk Task = _taskService.GetTask(id);
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
        public IActionResult CreateTask(Tassk t)
        {
            _taskService.AddTask(t);
            return Ok("task added successfully");
        }

        [HttpPut]
        public ActionResult UpdateTask(Tassk t)
        {
            int status = _taskService.UpdateTask(t);
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
            int status = _taskService.DeleteTask(id);
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

