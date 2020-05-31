using Library.API.Entities;
using System;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Services
{
    public class BookDbRepository: RepositoryBase<Book, Guid>, IBookDbRepository
    {
        public BookDbRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
