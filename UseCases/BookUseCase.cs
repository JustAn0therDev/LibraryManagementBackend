using Entities;
using System;
using System.Collections.Generic;
using UseCases;
using UseCases.Interfaces;
using Repositories.Interfaces;

namespace UseCases
{
    public class BookUseCase : IBookUseCase
    {
        #region Private Properties

        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IGenreRepository _genreRepository;

        #endregion

        #region Private Methods

        private void PopulateBookObjectByReference(Book book)
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

        #endregion

        public BookUseCase(IBookRepository bookRepository, IAuthorRepository authorRepository, IPublisherRepository publisherRepository, IGenreRepository genreRepository) 
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _publisherRepository = publisherRepository;
            _genreRepository = genreRepository;
        }

        #region Public Methods

        public IEnumerable<Book> GetAll()
        {
            var allBooks = _bookRepository.GetAll();

            foreach (var book in allBooks)
            {
                PopulateBookObjectByReference(book);
            }

            return allBooks;
        }

        public Book GetById(int id)
        {
            var book = _bookRepository.GetById(id);

            PopulateBookObjectByReference(book);

            return book;
        }

        public Book MakeObject(string name, int? authorId, int? publisherId, int? genreId)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)) 
            {
                UseCaseUtils.ThrowArgumentNullException(nameof(name));
            }

            if (authorId == null || authorId == 0)
            {
                UseCaseUtils.ThrowArgumentNullException(nameof(authorId));
            }

            if (publisherId == null || publisherId == 0)
            {
                UseCaseUtils.ThrowArgumentNullException(nameof(publisherId));
            }

            if (genreId == null || genreId == 0)
            {
                UseCaseUtils.ThrowArgumentNullException(nameof(genreId));
            }

            return new Book
            {
                Name = name,
                AuthorID = authorId,
                PublisherID = publisherId,
                GenreID = genreId
            };
        }

        public Book Save(Book book)
        {
            if (book == null) 
            {
                throw new ArgumentNullException(nameof(book), "Entity not provided");
            }

            _bookRepository.Save(book);

            PopulateBookObjectByReference(book);

            return book;
        }

        #endregion
    }
}
