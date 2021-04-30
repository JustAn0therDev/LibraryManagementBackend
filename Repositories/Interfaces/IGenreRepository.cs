using System.Collections.Generic;
using Entities;

namespace Repositories.Interfaces
{
    public interface IGenreRepository : IRepository<Genre>
    {
        IEnumerable<Genre> GetAll();

        Genre Save(Genre genre);
    }
}
