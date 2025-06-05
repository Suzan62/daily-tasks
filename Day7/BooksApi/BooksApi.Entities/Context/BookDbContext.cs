using BooksApi.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Entities.Context
{
    public class BookDbContext(DbContextOptions<BookDbContext> options) : DbContext(options)
    {
        public DbSet<BookDetails> BookDetails { get; set; }
        public DbSet<User>  Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = 1,
                Name = "admin",
                Email = "admin@tatvasoft.com",
                Role = "Admin",
                Password = "admin"
            });
            modelBuilder.Entity<BookDetails>()
    .HasOne(b => b.User)
    .WithMany() // or .WithMany(u => u.Books) if User has navigation
    .HasForeignKey(b => b.UserId)
    .OnDelete(DeleteBehavior.Restrict);
  
          
            base.OnModelCreating(modelBuilder);
        }
    }
}
