using Microsoft.EntityFrameworkCore;

namespace Library.API.Entities
{
    public class LibraryDbContext: DbContext
    {
        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        { 
        
        }
    }
}
