using System.Collections.Generic;
using Entities;

namespace Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();

        Book Save(Book book);
    }
}
