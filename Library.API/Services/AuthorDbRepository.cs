using Microsoft.EntityFrameworkCore;
using System;
using Library.API.Entities;
using Library.API.Helpers;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using Library.API.Extentions;

namespace Library.API.Services
{
    public class AuthorDbRepository : RepositoryBase<Author, Guid>, IAuthorDbRepository
    {
        private Dictionary<string, PropertyMapping> mappingDict = null;
        public AuthorDbRepository(DbContext dbContext) : base(dbContext)
        {
            mappingDict = new Dictionary<string, PropertyMapping>(StringComparer.OrdinalIgnoreCase);
            mappingDict.Add("Name", new PropertyMapping("Name"));
            mappingDict.Add("Age", new PropertyMapping("BirthDate", true));
            mappingDict.Add("BirthPlace", new PropertyMapping("BirthPlace"));
        }

        public Task<PagedList<Author>> GetAllAsync(AuthorResourceParameters parameters)
        {
            IQueryable<Author> queryableAuthors = DbContext.Set<Author>();
            if (!string.IsNullOrEmpty(parameters.BirthPlace)) {
                queryableAuthors = queryableAuthors.Where(m => m.BirthPlace.ToLower() == parameters.BirthPlace);
            }
            if (!string.IsNullOrEmpty(parameters.SearchQuery)) {
                queryableAuthors = queryableAuthors.Where(m => m.BirthPlace.ToLower().Contains(parameters.SearchQuery.ToLower()) || m.Name.ToLower().Contains(parameters.SearchQuery.ToLower()));
            }
            var orderedAuthors = queryableAuthors.Sort(parameters.SortBy, mappingDict);
            return PagedList<Author>.CreateAsync(orderedAuthors, parameters.PageNumber, parameters.PageSize);
        }
    }
}
