using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public interface IBookContext
    {
        DbSet<Book> Books { get; set; }
        void Commit();
    }
}
