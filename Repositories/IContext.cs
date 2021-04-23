using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public interface IBookContext
    {
        public DbSet<Book> Books { get; set; }
    }
}
