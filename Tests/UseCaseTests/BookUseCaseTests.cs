using Xunit;
using Moq;
using Entities;
using UseCases;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.UseCaseTests
{
    public class BookUseCaseTests
    {
        private readonly string _bookName = "Science";
        private readonly Mock<IBookRepository> _bookRepositoryMock = new Mock<IBookRepository>();
        private readonly Mock<IAuthorRepository> _authorRepositoryMock = new Mock<IAuthorRepository>();
        private readonly Mock<IPublisherRepository> _publisherRepositoryMock = new Mock<IPublisherRepository>();
        private readonly Mock<IGenreRepository> _genreRepositoryMock = new Mock<IGenreRepository>();

        [Fact]
        public void MakeObjectShouldReturnBook()
        {
            var useCase = new BookUseCase(
                        _bookRepositoryMock.Object,
                        _authorRepositoryMock.Object,
                        _publisherRepositoryMock.Object,
                        _genreRepositoryMock.Object
                        );

            Assert.True(useCase.MakeObject(_bookName, authorId: 1, publisherId: 2, genreId: 3) != null);
        }

        [Fact]
        public void MakeObjectShouldThrowExceptionWhenStringEmpty()
        {
            var useCase = new BookUseCase(
                _bookRepositoryMock.Object,
                _authorRepositoryMock.Object,
                _publisherRepositoryMock.Object,
                _genreRepositoryMock.Object
                );

            Assert.Throws<ArgumentNullException>(() => { useCase.MakeObject(string.Empty, authorId: 0, publisherId: 0, genreId: 0); });
        }

        [Fact]
        public void MakeObjectShouldThrowExceptionWhenStringIsWhiteSpace()
        {
            var useCase = new BookUseCase(
                _bookRepositoryMock.Object,
                _authorRepositoryMock.Object,
                _publisherRepositoryMock.Object,
                _genreRepositoryMock.Object
                );

            Assert.Throws<ArgumentNullException>(() => { useCase.MakeObject("     ", authorId: 0, publisherId: 0, genreId: 0); });
        }

        [Fact]
        public void MakeObjectShouldThrowExceptionWhenAuthorIdIsZero()
        {
            var useCase = new BookUseCase(
                _bookRepositoryMock.Object,
                _authorRepositoryMock.Object,
                _publisherRepositoryMock.Object,
                _genreRepositoryMock.Object
                );

            Assert.Throws<ArgumentNullException>(() => { useCase.MakeObject(_bookName, authorId: 0, publisherId: 1, genreId: 2); });
        }

        [Fact]
        public void MakeObjectShouldThrowExceptionWhenPublisherIdIsZero()
        {
            var useCase = new BookUseCase(
                _bookRepositoryMock.Object,
                _authorRepositoryMock.Object,
                _publisherRepositoryMock.Object,
                _genreRepositoryMock.Object
                );

            Assert.Throws<ArgumentNullException>(() => { useCase.MakeObject(_bookName, authorId: 1, publisherId: 0, genreId: 2); });
        }

        [Fact]
        public void MakeObjectShouldThrowExceptionWhenGenreIdIsZero()
        {
            var useCase = new BookUseCase(
                _bookRepositoryMock.Object,
                _authorRepositoryMock.Object,
                _publisherRepositoryMock.Object,
                _genreRepositoryMock.Object
                );

            Assert.Throws<ArgumentNullException>(() => { useCase.MakeObject(_bookName, authorId: 1, publisherId: 1, genreId: 0); });
        }

        [Fact]
        public void SaveShouldReturnTheSameObject()
        {
            var book = new Book { Name = _bookName };

            _bookRepositoryMock.Setup(s => s.Save(book)).Returns(book);

            var useCase = new BookUseCase(
                _bookRepositoryMock.Object,
                _authorRepositoryMock.Object,
                _publisherRepositoryMock.Object,
                _genreRepositoryMock.Object
                );

            book = useCase.Save(book);

            Assert.True(book != null && book.Name == _bookName);
        }

        [Fact]
        public void GetAllShouldReturnListOfBooks()
        {
            var collectionOfBooks = new List<Book> { new Book { Name = _bookName, AuthorID = 1, PublisherID = 1, GenreID = 1 } };

            _bookRepositoryMock.Setup(s => s.GetAll()).Returns(collectionOfBooks);
            _authorRepositoryMock.Setup(s => s.GetById(1)).Returns(new Author { Name = "Robert C. Martin" });
            _publisherRepositoryMock.Setup(s => s.GetById(1)).Returns(new Publisher { Name = "Some Publisher" });
            _genreRepositoryMock.Setup(s => s.GetById(1)).Returns(new Genre { Name = "Science & Technology" });

            var useCase = new BookUseCase(
                _bookRepositoryMock.Object,
                _authorRepositoryMock.Object,
                _publisherRepositoryMock.Object,
                _genreRepositoryMock.Object
                );

            collectionOfBooks = useCase.GetAll().ToList();

            Assert.True(collectionOfBooks != null &&
                        collectionOfBooks.Count == 1 &&
                        collectionOfBooks.FirstOrDefault()?.Author != null &&
                        collectionOfBooks.FirstOrDefault()?.Publisher != null &&
                        collectionOfBooks.FirstOrDefault()?.Genre != null
                        );
        }

        [Fact]
        public void GetAllShouldReturnNull()
        {
            _bookRepositoryMock.Setup(s => s.GetAll()).Returns<IEnumerable<Book>>(null);

            var useCase = new BookUseCase(
                _bookRepositoryMock.Object,
                _authorRepositoryMock.Object,
                _publisherRepositoryMock.Object,
                _genreRepositoryMock.Object
                );

            var collectionOfBooks = useCase.GetAll();

            Assert.True(collectionOfBooks == null);
        }

        [Fact]
        public void GetByIdShouldReturnOneBook()
        {
            var book = new Book { Name = _bookName };

            _bookRepositoryMock.Setup(s => s.GetById(1)).Returns(book);

            var useCase = new BookUseCase(
                _bookRepositoryMock.Object,
                _authorRepositoryMock.Object,
                _publisherRepositoryMock.Object,
                _genreRepositoryMock.Object
                );

            book = useCase.GetById(1);

            Assert.True(book != null);
        }

        [Fact]
        public void GetByIdShouldReturnNull()
        {
            _bookRepositoryMock.Setup(s => s.GetById(2)).Returns<Book>(null);

            var useCase = new BookUseCase(
                _bookRepositoryMock.Object,
                _authorRepositoryMock.Object,
                _publisherRepositoryMock.Object,
                _genreRepositoryMock.Object
                );

            var book = useCase.GetById(2);

            Assert.True(book == null);
        }
    }
}
