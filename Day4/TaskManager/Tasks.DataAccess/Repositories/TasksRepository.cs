using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.DataAccess.Models;

namespace Tasks.DataAccess.Repositories
{
    public class TasksRepository
    {
        private readonly TasskDbContext _context;
        public TasksRepository(TasskDbContext context)
        {
            _context = context;
        }
        public List<Tassk> GetTasks()
        {
            return _context.tasks.ToList();
        }
        public Tassk GetTask(int id)
        {
            Tassk T = _context.tasks.FirstOrDefault(s => s.Id == id);
            if (T == null)
            {
                return null;
            }
            return T;
        }
        public void AddTask(Tassk T)
        {
            _context.tasks.Add(T);
            _context.SaveChanges();
        }

        public int UpdateTask(Tassk T)
        {
            Tassk TaskToBeUpdated = GetTask(T.Id);
            if (TaskToBeUpdated == null)
            {
                return -1;
            }
            else
            {
                TaskToBeUpdated.Taskname = T.Taskname;
                TaskToBeUpdated.Description = T.Description;
                TaskToBeUpdated.IsCompleted = T.IsCompleted;
                _context.SaveChanges();
                return 1;
            }
        }

        public int DeleteTask(int id)
        {
            Tassk TaskToBeRemoved = GetTask(id);
            if (TaskToBeRemoved == null)
            {
                return -1;
            }
            else
            {
                _context.tasks.Remove(TaskToBeRemoved);
                _context.SaveChanges();
                return 1;
            }
        }
        public List<Tassk> GetCompletedTask()
        {
            List<Tassk> completed = new List<Tassk>();
            List<Tassk> list = _context.tasks.ToList();
            for (int indexer = 0; indexer <list.Count; indexer++)
            {
                if (list[indexer].IsCompleted == true)
                {
                    completed.Add(list[indexer]);

                }
            }

            return completed;





        }
    }
}
