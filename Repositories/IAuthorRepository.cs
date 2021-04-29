using System.Collections.Generic;
using Entities;

namespace Repositories
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAll();

        Author Save(Author author);
    }
}
