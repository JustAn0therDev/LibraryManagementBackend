using Entities;
using LibraryManagementBackend.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using UseCases.Interfaces;
using Xunit;

namespace Tests.ControllerTests
{
    public class BookControllerTests
    {
        [Fact]
        public void GetShouldReturnListOfBooks()
        {
            var bookName = "The Pragmatic Programmer";
            var mock = new Mock<IBookUseCase>();

            mock.Setup(s => s.MakeObject(bookName, null, null, null)).Returns(new Book
            {
                ID = 1,
                Name = bookName,
                Genre = new Genre { Name = "Programming" },
                Author = new Author { Name = "Forgot the guy's name" },
                Publisher = new Publisher { Name = "Some publisher" }
            });

            var loggerMock = new Mock<ILogger>();

            var controller = new BookController(loggerMock.Object, mock.Object);

            Assert.True(controller.Get() != null);
        }

        [Fact]
        public void PostShouldInsertBook()
        {

        }
    }
}
