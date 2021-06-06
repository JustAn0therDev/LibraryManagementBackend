using Entities;
using LibraryManagementBackend.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using UseCases.Interfaces;
using Xunit;
using System.Collections.Generic;
using System;

namespace Tests.ControllerTests
{
    public class BookControllerTests
    {
        private readonly string _bookName = "The Pragmatic Programmer";
        private readonly Mock<IBookUseCase> _mock = new Mock<IBookUseCase>();

        [Fact]
        public void GetShouldReturnListOfBooks()
        {
            var bookName = "The Pragmatic Programmer";
            var mock = new Mock<IBookUseCase>();

            mock.Setup(s => s.GetAll()).Returns(new List<Book>
            {
                new Book 
                {
                    ID = 1,
                    Name = bookName,
                    Genre = new Genre { Name = "Science" },
                    Author = new Author { Name = "Andrew Hunt" },
                    Publisher = new Publisher { Name = "Some publisher" }
                }
            });

            var loggerMock = new Mock<ILogger>();

            var controller = new BookController(loggerMock.Object, mock.Object);

            var response = controller.Get();

            Assert.True(response.StatusCode == 200 && response.Value as List<Book> != null);
        }

        [Fact]
        public void PostShouldInsertBook()
        {
            var book = new Book { Name = _bookName, AuthorID = 1, PublisherID = 32, GenreID = 23 };

            var mock = new Mock<IBookUseCase>();

            mock.Setup(s => s.MakeObject(book.Name, book.AuthorID, book.PublisherID, book.GenreID)).Returns(book);
            mock.Setup(s => s.Save(book)).Returns(book);

            var loggerMock = new Mock<ILogger>();

            var controller = new BookController(loggerMock.Object, mock.Object);

            var response = controller.Post(book);

            Assert.True(response.StatusCode == 201 && response.Value as Book != null);
        }

        [Fact]
        public void PostShouldReturnBadRequest()
        {
            var book = new Book { Name = "", AuthorID = 1, PublisherID = 32, GenreID = 23 };

            var mock = new Mock<IBookUseCase>();

            mock.Setup(s => s.MakeObject(book.Name, book.AuthorID, book.PublisherID, book.GenreID)).Throws<ArgumentNullException>();

            var loggerMock = new Mock<ILogger>();

            var controller = new BookController(loggerMock.Object, mock.Object);

            var response = controller.Post(book);

            Assert.True(response.StatusCode == 400 && response.Value as Book == null);
        }

        [Fact]
        public void PostShouldReturnInternalServerError()
        {
            var book = new Book { Name = "", AuthorID = 1, PublisherID = 32, GenreID = 23 };

            var mock = new Mock<IBookUseCase>();

            mock.Setup(s => s.MakeObject(book.Name, book.AuthorID, book.PublisherID, book.GenreID)).Throws<Exception>();

            var loggerMock = new Mock<ILogger>();

            var controller = new BookController(loggerMock.Object, mock.Object);

            var response = controller.Post(book);

            Assert.True(response.StatusCode == 500 && response.Value as Book == null);
        }
    }
}
