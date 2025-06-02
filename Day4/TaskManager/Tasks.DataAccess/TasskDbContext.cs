using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tasks.DataAccess.Models;

namespace Tasks.DataAccess
{
    public class TasskDbContext : DbContext
    {
        public TasskDbContext(DbContextOptions<TasskDbContext> options) : base(options) { }
        public DbSet<Tassk> tasks { get; set; }
    }
}
