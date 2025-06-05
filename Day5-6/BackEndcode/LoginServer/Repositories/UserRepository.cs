using LoginServer.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginServer.Repositories
{
    public class UserRepository
    {
        private readonly UserDbContext _dbContext;

       
        public UserRepository(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> UpdateUser(User updatedUser)
        {
            var existingUser = await _dbContext.Users.FindAsync(updatedUser.Id);
            if (existingUser == null) return false;

            // Update properties (example)
            existingUser.Id= updatedUser.Id;
            existingUser.Name = updatedUser.Name;
            existingUser.Email=updatedUser.Email;
            existingUser.Role = updatedUser.Role;
            existingUser.Password = updatedUser.Password;  // You might want to hash passwords
                                                           // Add other fields to update as needed

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public User? Login(string username, string password)
        {
            var user = _dbContext.Users
                .FirstOrDefault(x => x.Email == username && x.Password == password);
            return user;
        }
    }
}
