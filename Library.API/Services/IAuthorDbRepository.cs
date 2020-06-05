using Library.API.Entities;
using Library.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public interface IAuthorDbRepository: IRepositoryBase<Author>, IRepositoryBase2<Author, Guid>
    {
        Task<PagedList<Author>> GetAllAsync(AuthorResourceParameters parameters);
    }
}
