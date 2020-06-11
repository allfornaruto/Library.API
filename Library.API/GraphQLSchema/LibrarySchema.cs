using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.GraphQLSchema
{
    public class LibrarySchema: Schema
    {
        public LibrarySchema(LibraryQuery query, IDependencyResolver dependencyResolver)
        {
            Query = query;
            DependencyResolver = dependencyResolver;
        }
    }
}
