using System;
using System.Collections.Generic;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.API.Filters;
using Library.API.Services;
using Library.API.Models;
using Library.API.Entities;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;

namespace Library.API.Controllers
{
    [ServiceFilter(typeof(CheckAuthorExistFilterAttribute))]
    [Route("api/authorsDb/{authorId}/books")]
    [ApiController]
    public class BookDbController : ControllerBase
    {
        public IRepositoryWrapper RepositoryWrapper { get; }
        public IMapper Mapper { get; }
        public BookDbController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            RepositoryWrapper = repositoryWrapper;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> GetBooksAsync(Guid authorId)
        {
            var books = await RepositoryWrapper.Book.GetBooksAsync(authorId);
            var bookDtoList = Mapper.Map<IEnumerable<BookDto>>(books);
            return bookDtoList.ToList();
        }

        [HttpGet("{bookId}", Name = nameof(GetBook))]
        public async Task<ActionResult<BookDto>> GetBook(Guid authorId, Guid bookId)
        {
            var book = await RepositoryWrapper.Book.GetBookAsync(authorId, bookId);
            if (book == null) return NotFound();
            var bookDto = Mapper.Map<BookDto>(book);
            return bookDto;
        }

        [HttpPost]
        public async Task<IActionResult> AddBookAsync(Guid authorId, BookForCreationDto bookForCreationDto)
        {
            var book = Mapper.Map<Book>(bookForCreationDto);
            book.AuthorId = authorId;
            RepositoryWrapper.Book.Create(book);
            if (!await RepositoryWrapper.Book.SaveAsync())
            {
                throw new Exception("创建资源Book失败");
            }
            var bookDto = Mapper.Map<BookDto>(book);
            return CreatedAtRoute(nameof(GetBooksAsync), new { bookId = bookDto.Id }, bookDto);
        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBookAsync(Guid authorId, Guid bookId, BookForUpdateDto updatedBook)
        {
            var book = await RepositoryWrapper.Book.GetBookAsync(authorId, bookId);
            if (book == null)
            {
                return NotFound();
            }
            Mapper.Map(updatedBook, book, typeof(BookForUpdateDto), typeof(Book));
            RepositoryWrapper.Book.Update(book);
            if (!await RepositoryWrapper.Book.SaveAsync())
            {
                throw new Exception("更新资源Book失败");
            }
            return NoContent();
        }

        [HttpPatch("{bookId}")]
        public async Task<IActionResult> PartiallyUpdateBookAsync(Guid authorId, Guid bookId, JsonPatchDocument<BookForUpdateDto> patchDocument)
        {
            var book = await RepositoryWrapper.Book.GetBookAsync(authorId, bookId);
            if (book == null) return NotFound();
            var bookUpdateDto = Mapper.Map<BookForUpdateDto>(book);
            patchDocument.ApplyTo(bookUpdateDto, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Mapper.Map(bookUpdateDto, book, typeof(BookForUpdateDto), typeof(Book));
            RepositoryWrapper.Book.Update(book);
            if (!await RepositoryWrapper.Book.SaveAsync())
            {
                throw new Exception("更新资源Book失败");
            }
            return NoContent();
        }
    }
}
