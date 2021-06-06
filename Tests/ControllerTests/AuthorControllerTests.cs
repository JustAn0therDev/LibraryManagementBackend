using System;
using Xunit;
using Moq;
using Entities;
using LibraryManagementBackend.Controllers;
using UseCases.Interfaces;

namespace Tests.ControllerTests
{
    public class AuthorControllerTests
    {
        [Fact]
        public void GetShouldReturnListOfAuthors()
        {
            var authorName = "Robert C. Martin";
            var mock = new Mock<IAuthorUseCase>();

            mock.Setup(s => s.MakeObject(authorName)).Returns(new Author
            {
                ID = 1,
                Name = authorName
            });

            var controller = new AuthorController(mock.Object);

            Assert.True(controller.Get() != null);
        }
    }
}
