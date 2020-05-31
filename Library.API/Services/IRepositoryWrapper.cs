using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public interface IRepositoryWrapper
    {
        IBookDbRepository Book { get; }
        IAuthorDbRepository Author { get; }
    }
}
