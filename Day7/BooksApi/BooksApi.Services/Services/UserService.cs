using BooksApi.Data.Models;
using BooksApi.Entities.Repositories.Interface;
using BooksApi.Models;
using BooksApi.Services.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApi.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserVM>> GetAllUsers(FilterVM filterRequest)
        {
            var users = await _userRepository.GetAllUser(filterRequest);

            return users.Select(user => new UserVM()
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                Role = user.Role,
                Book = user.BookDetails.Select(book => new Book()
                {
                    Id = book.Id,
                    Author = book.Author,
                    Description = book.Description,
                    Title = book.Title
                }).ToList()
            }).ToList();
        }


    }
}
