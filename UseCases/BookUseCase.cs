using Entities;
using System;
using System.Collections.Generic;
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
        
        /// <summary>
        /// Populates a book object (currently: Author, Genre and Publisher) by reference if the object is not null.
        /// <param name="book">Book object to populate</param>
        /// </summary>
        private void PopulateBookObjectByReference(Book book)
        {
            if (book == null)
            {
                return;
            }

            if (book.AuthorID != 0) 
            {
                book.Author = _authorRepository.GetById(book.AuthorID);
            }

            if (book.PublisherID != 0)
            {
                book.Publisher = _publisherRepository.GetById(book.PublisherID);
            }

            if (book.GenreID != 0) 
            {
                book.Genre = _genreRepository.GetById(book.GenreID);
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

        /// <summary>
        /// Fetches all records of Book in the repository
        /// </summary>
        /// <returns>IEnumerable<Book> collection</returns>
        public IEnumerable<Book> GetAll()
        {
            var allBooks = _bookRepository.GetAll();

            if (allBooks != null)
            {
                foreach (var book in allBooks)
                {
                    PopulateBookObjectByReference(book);
                }
            }

            return allBooks;
        }

        /// <summary>
        /// Fetches an object by its repository ID.
        /// </summary>
        /// <param name="id">Book repository ID</param>
        /// <returns>Object of Book type</returns>
        public Book GetById(int id)
        {
            var book = _bookRepository.GetById(id);

            PopulateBookObjectByReference(book);

            return book;
        }

        /// <summary>
        /// Creates an object of the given use case type (e.g. AuthorUseCase.MakeObject method returns an Author object).
        /// This method should validate any business rules regarding the desired object.
        /// </summary>
        /// <param name="name">Book name</param>
        /// <param name="authorId">Author identifier</param>
        /// <param name="publisherId">Publisher identifier</param>
        /// <param name="genreId">Genre identifier</param>
        /// <returns>Object of Book type</returns>
        public Book MakeObject(string name, int authorId, int publisherId, int genreId)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)) 
            {
                throw new ArgumentNullException(nameof(name), $"A value for {nameof(name)} must be provided");
            }

            if (authorId == 0)
            {
                throw new ArgumentNullException(nameof(authorId), $"A value for {nameof(authorId)} must be provided");
            }

            if (publisherId == 0)
            {
                throw new ArgumentNullException(nameof(publisherId), $"A value for {nameof(publisherId)} must be provided");
            }

            if (genreId == 0)
            {
                throw new ArgumentNullException(nameof(genreId), $"A value for {nameof(genreId)} must be provided");
            }

            return new Book
            {
                Name = name,
                AuthorID = authorId,
                PublisherID = publisherId,
                GenreID = genreId
            };
        }

        /// <summary>
        /// Persists the desired data in the repository.
        /// </summary>
        /// <param name="book">Book object</param>
        /// <returns>The same object. Might raise an exception if a problem occurs during saving.</returns>
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
