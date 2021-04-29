using System.Collections.Generic;
using System.Linq;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class LibraryContext : DbContext, IBookRepository
    {
        public LibraryContext(DbContextOptions options) : base(options) { }

        public DbSet<Book> Books { get; set; }

        public Book Save(Book book)
        {
            Books.Add(book);
            SaveChanges();

            return book;
        }
        public IEnumerable<Book> GetAll()
        {
            return Books.Select(s => s);
        }
    }
}
