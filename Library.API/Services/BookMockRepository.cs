using Library.API.Data;
using Library.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public class BookMockRepository: IBookRepository
    {
        public BookDto GetBookForAuthor(Guid authorId, Guid bookId)
        {
            return LibraryMockData.Current.Books.FirstOrDefault(book => book.AuthorId == authorId && book.Id == bookId);
        }

        public IEnumerable<BookDto> GetBooksForAuthor(Guid authorId)
        {
            return LibraryMockData.Current.Books.Where(book => book.AuthorId == authorId).ToList();
        }

        public void AddBook(BookDto book)
        {
            LibraryMockData.Current.Books.Add(book);
        }

        public void DeleteBook(BookDto book)
        {
            LibraryMockData.Current.Books.Remove(book);
        }

        public void UpdateBook(Guid authorId, Guid bookId, BookForUpdateDto book)
        {
            var originBook = GetBookForAuthor(authorId, bookId);
            originBook.Title = book.Title;
            originBook.Pages = book.Pages;
            originBook.Description = book.Description;
        }
    }
}
