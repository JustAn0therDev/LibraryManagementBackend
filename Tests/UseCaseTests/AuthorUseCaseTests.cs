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
	public class AuthorUseCaseTests
    {
      [Fact]
      public void MakeObjectShouldReturnAuthor()
      {
          var authorName = "Robert C. Martin";
          var mock = new Mock<IAuthorRepository>();

          var useCase = new AuthorUseCase(mock.Object);

          Assert.True(useCase.MakeObject(authorName) != null);
        }

				[Fact]
        public void MakeObjectShouldThrowExceptionWhenStringEmpty()
        {
          var mock = new Mock<IAuthorRepository>();

	        var useCase = new AuthorUseCase(mock.Object);

          Assert.Throws<ArgumentNullException>(() => { useCase.MakeObject(string.Empty); });
        }

				[Fact]
        public void MakeObjectShouldThrowExceptionWhenStringIsWhiteSpace()
        {
          var mock = new Mock<IAuthorRepository>();

	        var useCase = new AuthorUseCase(mock.Object);

          Assert.Throws<ArgumentNullException>(() => { useCase.MakeObject("     "); });
        }

        [Fact]
        public void SaveShouldReturnTheSameObject()
        {
          var authorName = "Robert C. Martin";

          var author = new Author { Name = authorName };
          
          var mock = new Mock<IAuthorRepository>();

          mock.Setup(s => s.Save(author)).Returns(author);

	        var useCase = new AuthorUseCase(mock.Object);

          author = useCase.Save(author);

          Assert.True(author != null && author.Name == authorName);
        }

        [Fact]
        public void GetAllShouldReturnListOfAuthors()
        {
          var collectionOfAuthors = new List<Author> { new Author { Name = "Robert C. Martin" } };
          var mock = new Mock<IAuthorRepository>();

          mock.Setup(s => s.GetAll()).Returns(collectionOfAuthors);

          var useCase = new AuthorUseCase(mock.Object);

          collectionOfAuthors = useCase.GetAll().ToList();

          Assert.True(collectionOfAuthors != null && collectionOfAuthors.Count == 1);
        }

        [Fact]
        public void GetAllShouldReturnNull()
        {
          var mock = new Mock<IAuthorRepository>();

          mock.Setup(s => s.GetAll()).Returns<IEnumerable<Author>>(null);

          var useCase = new AuthorUseCase(mock.Object);

          var collectionOfAuthors = useCase.GetAll();

          Assert.True(collectionOfAuthors == null);
        }

        [Fact]
        public void GetByIdShouldReturnOneAuthor()
        {
          var author = new Author { Name = "Robert C. Martin" };
          var mock = new Mock<IAuthorRepository>();

          mock.Setup(s => s.GetById(1)).Returns(author);

          var useCase = new AuthorUseCase(mock.Object);

          author = useCase.GetById(1);

          Assert.True(author != null);
        }

        [Fact]
        public void GetByIdShouldReturnNull()
        {
          var mock = new Mock<IAuthorRepository>();

          mock.Setup(s => s.GetById(2)).Returns<Author>(null);

          var useCase = new AuthorUseCase(mock.Object);

          var author = useCase.GetById(1);

          Assert.True(author == null);
        }
    }
}
