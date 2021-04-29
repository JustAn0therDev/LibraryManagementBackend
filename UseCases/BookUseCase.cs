using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using UseCases.Interfaces;

namespace UseCases
{
    public class BookUseCase : IBookUseCase
    {
        private readonly IBookRepository _bookRepository;

        public BookUseCase(IBookRepository bookContext) => _bookRepository = bookContext;

        public IEnumerable<Book> GetAll()
        {
            return _bookRepository.GetAll();
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

            _bookRepository.Save(entity);

            return entity;
        }
    }
}
