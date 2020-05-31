using Library.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public class RepositoryWrapper: IRepositoryWrapper
    {
        private IBookDbRepository _bookDbRepository = null;
        private IAuthorDbRepository _authorDbRepositroy = null;
        public LibraryDbContext LibraryDbContext { get; }

        public RepositoryWrapper(LibraryDbContext libraryDbContext)
        {
            LibraryDbContext = libraryDbContext;
        }

        public IAuthorDbRepository Author => _authorDbRepositroy ?? new AuthorDbRepository(LibraryDbContext);
        public IBookDbRepository Book => _bookDbRepository ?? new BookDbRepository(LibraryDbContext);
    }
}
