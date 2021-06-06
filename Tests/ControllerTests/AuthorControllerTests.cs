using Xunit;
using Moq;
using Entities;
using LibraryManagementBackend.Controllers;
using UseCases.Interfaces;
using System;
using System.Collections.Generic;

namespace Tests.ControllerTests
{
	public class AuthorControllerTests
    {
        [Fact]
        public void GetShouldReturnListOfAuthors()
        {
            var authorName = "Robert C. Martin";
            var mock = new Mock<IAuthorUseCase>();

            mock.Setup(s => s.GetAll()).Returns(new List<Author> {
                new Author
                {
                    ID = 1,
                    Name = authorName
                }
            });

            var controller = new AuthorController(mock.Object);

            var response = controller.Get();

            List<Author> authors = response.Value as List<Author>;

            Assert.True(response.StatusCode == 200);
        }

        [Fact]
        public void PostShouldReturnAuthor()
        {
            var authorName = "Robert C. Martin";

            var author = new Author { Name = authorName };

            var mock = new Mock<IAuthorUseCase>();

            mock.Setup(s => s.MakeObject(authorName)).Returns(author);
            mock.Setup(s => s.Save(author)).Returns(author);

            var controller = new AuthorController(mock.Object);

            var response = controller.Post(author);

            author = response.Value as Author;

            Assert.True(response.StatusCode == 201 && author != null);
        }

        [Fact]
        public void PostShouldReturnBadRequest()
        {
            var authorName = "";

            var author = new Author { Name = authorName };

            var mock = new Mock<IAuthorUseCase>();

            mock.Setup(s => s.MakeObject(authorName)).Throws<ArgumentNullException>();

            var controller = new AuthorController(mock.Object);

            var response = controller.Post(author);

            Assert.True(response.StatusCode == 400);
        }

        [Fact]
        public void PostShouldReturnInternalServerError()
        {
            var authorName = "";

            var author = new Author { Name = authorName };

            var mock = new Mock<IAuthorUseCase>();

            mock.Setup(s => s.MakeObject(authorName)).Throws<Exception>();

            var controller = new AuthorController(mock.Object);

            var response = controller.Post(author);

            Assert.True(response.StatusCode == 500 && response.Value as Author is null);
        }
    }
}
