
using BooksApi.Entities.Context;
using BooksApi.Entities.Entities;
using BooksApi.Entities.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Entities.Repositories
{
    public class BookRepository(BookDbContext bookDbContext)   : IBookRepository
    {
        private readonly BookDbContext _dbContext = bookDbContext;

        public async Task InsertBook(BookDetails bookDetails)
        {
            await _dbContext.BookDetails.AddAsync(bookDetails);
            await _dbContext.SaveChangesAsync();
        }

        public BookDetails GetById(int id)
        {
            return _dbContext.BookDetails.Include(b => b.User).FirstOrDefault(x => x.Id == id);
        }
        public List<BookDetails> GetAll()
        {
            return _dbContext.BookDetails.Include(b => b.User).ToList();
            
        }
    }
}
