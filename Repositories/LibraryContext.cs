using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class LibraryContext : DbContext, IBookContext
    {
        public LibraryContext(DbContextOptions options) : base(options) { }

        public DbSet<Book> Books { get; set; }
    }
}
