using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Library.API.GraphQLSchema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("graphql")]
    [ApiController]
    public class GraphQLController : ControllerBase
    {
        public IDocumentExecuter DocumentExecuter { get; }
        public ISchema LibrarySchema { get; }
        public GraphQLController(ISchema librarySchema, IDocumentExecuter documentExecuter)
        {
            LibrarySchema = librarySchema;
            DocumentExecuter = documentExecuter;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]GraphQLRequest query)
        {
            var result = await DocumentExecuter.ExecuteAsync(options =>
            {
                options.Schema = LibrarySchema;
                options.Query = query.Query;
            });

            if (result.Errors?.Count > 0)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
