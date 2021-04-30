using System.Collections.Generic;
using Entities;

namespace Repositories.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<Book> GetAll();
        Book Save(Book book);
    }
}
