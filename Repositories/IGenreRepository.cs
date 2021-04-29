using System.Collections.Generic;
using Entities;

namespace Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetAll();

        Genre Save(Genre genre);
    }
}
