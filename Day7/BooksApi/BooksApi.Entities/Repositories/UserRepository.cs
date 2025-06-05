using BooksApi.Data.Models;
using BooksApi.Entities.Context;
using BooksApi.Entities.Entities;
using BooksApi.Entities.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Entities.Repositories
{
    public class UserRepository(BookDbContext bookDbContext) : IUserRepository
    {
        private readonly BookDbContext _dbContext = bookDbContext;

        //Task<IList<User>>
        public async Task<IList<User>> GetAllUser(FilterVM filterRequest)
        {
            var query = _dbContext.Users
                .Include(u => u.BookDetails) 
                .AsQueryable();

            // Apply filter
            if (!string.IsNullOrWhiteSpace(filterRequest.SearchFilter))
            {
                query = query.Where(u => u.Name.ToLower().Contains(filterRequest.SearchFilter.ToLower()));
            }

            // Sorting
            if (filterRequest.SortBy.ToLower() == "name")
            {
                query = filterRequest.SortDirection == "asc"
                    ? query.OrderBy(u => u.Name)
                    : query.OrderByDescending(u => u.Name);
            }

            if (filterRequest.SortBy.ToLower() == "email")
            {
                query = filterRequest.SortDirection == "asc"
                    ? query.OrderBy(u => u.Email)
                    : query.OrderByDescending(u => u.Email);
            }

            // Pagination
            query = query
                .Skip((filterRequest.PageNumber - 1) * filterRequest.PageSize)
                .Take(filterRequest.PageSize);

            return await query.ToListAsync();  
        }


    }
}
