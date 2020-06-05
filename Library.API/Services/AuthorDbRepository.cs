using Microsoft.EntityFrameworkCore;
using System;
using Library.API.Entities;
using System.Security.Cryptography.X509Certificates;
using Library.API.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public class AuthorDbRepository : RepositoryBase<Author, Guid>, IAuthorDbRepository
    {
        public AuthorDbRepository(DbContext dbContext) : base(dbContext)
        { 
            
        }

        public Task<PagedList<Author>> GetAllAsync(AuthorResourceParameters parameters)
        {
            IQueryable<Author> queryableAuthors = DbContext.Set<Author>();
            return PagedList<Author>.CreateAsync(queryableAuthors, parameters.PageNumber, parameters.PageSize);
        }
    }
}
