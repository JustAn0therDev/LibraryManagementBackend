using Entities;
using Repositories;
using System.Collections.Generic;
using System.Linq;

namespace UseCases
{
    public class BookUseCase : IBookUseCase
    {
        private readonly IBookContext _bookContext;

        public BookUseCase(IBookContext bookContext) => _bookContext = bookContext;

        public IEnumerable<Book> GetAll()
        {
            return _bookContext.Books.Select(s => s);
        }
    }
}
