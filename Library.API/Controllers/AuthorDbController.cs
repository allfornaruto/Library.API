using System.Collections.Generic;
using AutoMapper;
using Library.API.Services;
using Library.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System;
using Library.API.Entities;

namespace Library.API.Controllers
{
    [Route("api/authorsDb")]
    [ApiController]
    public class AuthorDbController : ControllerBase
    {
        public IMapper Mapper { get; }
        public IRepositoryWrapper RepositoryWrapper { get; }
        public AuthorDbController(IRepositoryWrapper respoitoryWrapper, IMapper mapper)
        {
            RepositoryWrapper = RepositoryWrapper;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorDto>>> GetAuthorsAsync()
        {
            var authors = (await RepositoryWrapper.Author.GetAllAsync()).OrderBy(author => author.Name);
            var authorsDtoList = Mapper.Map<IEnumerable<AuthorDto>>(authors);
            return authorsDtoList.ToList();
        }

        [HttpGet("{authorId}", Name = nameof(GetAuthorAsync))]
        public async Task<ActionResult<AuthorDto>> GetAuthorAsync(Guid authorId)
        {
            var author = await RepositoryWrapper.Author.GetByConditionAsync(author => author.Id == authorId);
            if (author == null) return NotFound();
            return Mapper.Map<AuthorDto>(author);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(AuthorForCreationDto authorForCreattionDto)
        {
            var author = Mapper.Map<Author>(authorForCreattionDto);
            RepositoryWrapper.Author.Create(author);
            var result = await RepositoryWrapper.Author.SaveAsync();
            if (!result) {
                throw new Exception("创建资源author失败");
            }

            var authorCreated = Mapper.Map<AuthorDto>(author);
            return CreatedAtRoute(nameof(CreateAuthor), new { authorId = authorCreated.Id }, authorCreated);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAuthorAsync(Guid authorId)
        {
            var author = await RepositoryWrapper.Author.GetByIdAsync(authorId);
            if (author == null) {
                return NotFound();
            }

            RepositoryWrapper.Author.Delete(author);
            var result = await RepositoryWrapper.Author.SaveAsync();
            if (!result)
            {
                throw new Exception("删除资源author失败");
            }

            return NoContent();
        }
    }
}
