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
            RepositoryWrapper = respoitoryWrapper;
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
            var authorDto = Mapper.Map<IEnumerable<AuthorDto>>(author);
            return authorDto.FirstOrDefault();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthorDb(AuthorForCreationDto authorForCreattionDto)
        {
            var author = Mapper.Map<Author>(authorForCreattionDto);
            RepositoryWrapper.Author.Create(author);
            var result = await RepositoryWrapper.Author.SaveAsync();
            if (!result) {
                throw new Exception("创建资源author失败");
            }
            // 数据已经创建成功
            var authorCreated = Mapper.Map<AuthorDto>(author);
            // 下面这一行报错了
            return CreatedAtRoute(nameof(GetAuthorAsync), new { authorId = authorCreated.Id }, authorCreated);
        }

        [HttpDelete("{authorId}")]
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
