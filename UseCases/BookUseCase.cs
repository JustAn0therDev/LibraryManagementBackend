using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using UseCases.Interfaces;

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

        public Book MakeObject(string name)
        {
            return new Book 
            {
                Name = name
            };
        }

        public Book Save(Book entity)
        {
            if (entity == null) 
            {
                throw new ArgumentNullException("Entity not provided");
            }

            _bookContext.Books.Add(entity);

            _bookContext.Commit();

            return entity;
        }
    }
}
