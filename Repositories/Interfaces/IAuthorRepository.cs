using System.Collections.Generic;
using Entities;

namespace Repositories.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        IEnumerable<Author> GetAll();

        Author Save(Author author);
    }
}
