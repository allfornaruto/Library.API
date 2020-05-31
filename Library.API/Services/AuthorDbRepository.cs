using Microsoft.EntityFrameworkCore;
using System;
using Library.API.Entities;

namespace Library.API.Services
{
    public class AuthorDbRepository : RepositoryBase<Author, Guid>, IAuthorDbRepository
    {
        public AuthorDbRepository(DbContext dbContext) : base(dbContext)
        { 
        
        }
    }
}
