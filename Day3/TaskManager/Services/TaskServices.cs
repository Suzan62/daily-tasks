using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualBasic;
using TaskManager.Models;
using static System.Reflection.Metadata.BlobBuilder;
using Task = TaskManager.Models.Task;
namespace TaskManager.Services
{
    public class TaskService
    {
        public List<Task> Tasks;
        public TaskService()
        {
            Tasks = new List<Task>();
            Tasks.Add(new Task()
            {
                Id = 1,
                Taskname = " Grocery Shopping",
                Description = "Buy milk, eggs, bread, and vegetables",
                IsCompleted = false
            });
            Tasks.Add(new Task()
            {
                Id = 2,
                Taskname = "Read a Task",
                Description = "Finish reading 2 chapters of novel",
                IsCompleted = false
            });
        }
        public List<Task> GetTasks()
        {
            return Tasks;
        }

        public Task GetTask(int id)
        {
            Task T = Tasks.FirstOrDefault(s => s.Id == id);
            if (T == null)
            {
                return null;
            }
            return T;
        }
        public void AddTask(Task Task)
        {
            Task.Id = Tasks.Count + 1;
            Tasks.Add(Task);
        }

        public int UpdateTassk(Task Task)
        {
            Task TaskToBeUpdated = GetTask(Task.Id);
            if (TaskToBeUpdated == null)
            {
                return -1;
            }
            else
            {
                TaskToBeUpdated.Taskname = Task.Taskname;
                TaskToBeUpdated.Description = Task.Description;
                TaskToBeUpdated.IsCompleted = Task.IsCompleted;
                return 1;
            }
        }

        public int DeleteTask(int id)
        {
            Task TaskToBeRemoved = GetTask(id);
            if (TaskToBeRemoved == null)
            {
                return -1;
            }
            else
            {
                Tasks.Remove(TaskToBeRemoved);
                return 1;
            }
        }
        public List<Task> GetCompletedTask()
        {
            List<Task> completed = new List<Task>();
            for (int indexer = 0; indexer < Tasks.Count; indexer++)
            {
                if (Tasks[indexer].IsCompleted == true)
                {
                    completed.Add(Tasks[indexer]);

                }
            }
           
           return completed;
            




        }

    }
}

