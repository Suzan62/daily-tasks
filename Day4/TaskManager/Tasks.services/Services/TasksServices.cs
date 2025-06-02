using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tasks.DataAccess.Models;
using Tasks.DataAccess.Repositories;
using Tasks.DataAccess;
using System.Threading.Tasks;

namespace Tasks.services.Services
{
    public class TasksServices
    {
        private readonly TasksRepository _tasksRepository;

        public TasksServices(TasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }

        public List<Tassk> GetTasks()
        {
            return _tasksRepository.GetTasks();
        }

        public Tassk GetTask(int id)
        {
            return _tasksRepository.GetTask(id);
        }

        public void AddTask(Tassk t)
        {
            _tasksRepository.AddTask(t);
        }

        public int UpdateTask(Tassk t)
        {
            return _tasksRepository.UpdateTask(t);
        }

        public int DeleteTask(int id)
        {
            return _tasksRepository.DeleteTask(id);
        }

        public List<Tassk> GetCompletedTask()
        {
            return _tasksRepository.GetCompletedTask();
        }
    }
}
