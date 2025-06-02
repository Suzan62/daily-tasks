using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.DataAccess.Models
{
    public class Tassk
    {
        public int Id { get; set; }
        public string Taskname { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
