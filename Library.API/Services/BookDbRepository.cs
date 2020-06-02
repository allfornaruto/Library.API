using Library.API.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Library.API.Services
{
    public class BookDbRepository: RepositoryBase<Book, Guid>, IBookDbRepository
    {
        public BookDbRepository(DbContext dbContext) : base(dbContext)
        {
            
        }

        public Task<IEnumerable<Book>> GetBooksAsync(Guid authorId) 
        {
            return Task.FromResult(DbContext.Set<Book>().Where(Book => Book.AuthorId == authorId).AsEnumerable());
        }

        public async Task<Book> GetBookAsync(Guid authorId, Guid bookId)
        {
            return await DbContext.Set<Book>().SingleOrDefaultAsync(Book => Book.AuthorId == authorId && Book.Id == bookId);
        }
    }
}
