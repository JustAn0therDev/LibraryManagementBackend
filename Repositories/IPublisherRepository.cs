using System.Collections.Generic;
using Entities;

namespace Repositories
{
    public interface IPublisherRepository
    {
        IEnumerable<Publisher> GetAll();

        Publisher Save(Publisher genre);
    }
}
