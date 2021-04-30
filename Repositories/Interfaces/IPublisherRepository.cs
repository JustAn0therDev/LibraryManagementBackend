using System.Collections.Generic;
using Entities;

namespace Repositories.Interfaces
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        IEnumerable<Publisher> GetAll();

        Publisher Save(Publisher genre);
    }
}
