using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using UseCases.Interfaces;
using Repositories.Interfaces;

namespace UseCases
{
    public class BookUseCase : IBookUseCase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IGenreRepository _genreRepository;

        public BookUseCase(IBookRepository bookRepository, IAuthorRepository authorRepository, IPublisherRepository publisherRepository, IGenreRepository genreRepository) 
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _publisherRepository = publisherRepository;
            _genreRepository = genreRepository;
        }

        public IEnumerable<Book> GetAll()
        {
            var allBooks = _bookRepository.GetAll();

            foreach (var book in allBooks) 
            {
                if (book.AuthorID.HasValue) 
                {
                    book.Author = _authorRepository.GetById(book.AuthorID.Value);
                }

                if (book.PublisherID.HasValue) 
                {
                    book.Publisher = _publisherRepository.GetById(book.PublisherID.Value);
                }

                if (book.GenreID.HasValue) 
                {
                    book.Genre = _genreRepository.GetById(book.GenreID.Value);
                }
            }

            return allBooks;
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
