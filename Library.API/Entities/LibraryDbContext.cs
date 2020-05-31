using Microsoft.EntityFrameworkCore;
using System;

namespace Library.API.Entities
{
    public class LibraryDbContext: DbContext
    {
        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        { 
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.SeedData();
        }
    }

    public static class ModelBuilderExtension
    { 
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = new Guid("72D5B5F5-3008-49B7-B0D6-CC337F1A3330"),
                    Name = "Author 1",
                    BirthData = new DateTimeOffset(new DateTime(1997, 10, 10)),
                    BirthPlace = "上海",
                    Email = "author1@xxx.com"
                },
                new Author
                {
                    Id = new Guid("7D04A48E-BE4E-468E-8CE2-3AC0A0C79549"),
                    Name = "Author 2",
                    BirthData = new DateTimeOffset(new DateTime(1998, 10, 10)),
                    BirthPlace = "深圳",
                    Email = "author2@xxx.com"
                });
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = new Guid("7D8EBDA9-2634-4C0F-9469-0695D6132153"),
                    Title = "Book 1",
                    Description = "Description of Book 1",
                    Pages = 281,
                    AuthorId = new Guid("72D5B5F5-3008-49B7-B0D6-CC337F1A3330")
                },
                new Book
                {
                    Id = new Guid("7D8EBDA9-2634-4C0F-9469-0695D6132154"),
                    Title = "Book 2",
                    Description = "Description of Book 2",
                    Pages = 370,
                    AuthorId = new Guid("72D5B5F5-3008-49B7-B0D6-CC337F1A3330")
                },
                new Book
                {
                    Id = new Guid("7D8EBDA9-2634-4C0F-9469-0695D6132155"),
                    Title = "Book 3",
                    Description = "Description of Book 3",
                    Pages = 229,
                    AuthorId = new Guid("7D04A48E-BE4E-468E-8CE2-3AC0A0C79549")
                },
                new Book
                {
                    Id = new Guid("7D8EBDA9-2634-4C0F-9469-0695D6132156"),
                    Title = "Book 4",
                    Description = "Description of Book 4",
                    Pages = 440,
                    AuthorId = new Guid("7D04A48E-BE4E-468E-8CE2-3AC0A0C79549")
                });
        }
    }
}
