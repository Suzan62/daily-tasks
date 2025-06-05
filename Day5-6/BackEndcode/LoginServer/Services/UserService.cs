using LoginServer.Repositories;
using LoginServer.Models;
namespace LoginServer.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task AddUser(User user)
        {
            await this._userRepository.AddUser(user);
        }
        public async Task<bool> UpdateUser(User user)
        {
            return await _userRepository.UpdateUser(user);
        }

        public User? Login(string email, string password)
        {
            return this._userRepository.Login(email, password);
        }
    }
}
