using Entities;
using System.Collections.Generic;

namespace UseCases
{
    public interface IBookUseCase
    {
        IEnumerable<Book> GetAll();
    }
}
