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
      private readonly string _authorName = "Robert C. Martin";
      private readonly Mock<IAuthorRepository> _mock = new Mock<IAuthorRepository>();

      [Fact]
      public void MakeObjectShouldReturnAuthor()
      {
        var useCase = new AuthorUseCase(_mock.Object);

        Assert.True(useCase.MakeObject(_authorName) != null);
      }

      [Fact]
      public void MakeObjectShouldThrowExceptionWhenStringEmpty()
      {
        var useCase = new AuthorUseCase(_mock.Object);

        Assert.Throws<ArgumentNullException>(() => { useCase.MakeObject(string.Empty); });
      }

      [Fact]
      public void MakeObjectShouldThrowExceptionWhenStringIsWhiteSpace()
      {
        var useCase = new AuthorUseCase(_mock.Object);

        Assert.Throws<ArgumentNullException>(() => { useCase.MakeObject("     "); });
      }

      [Fact]
      public void SaveShouldReturnTheSameObject()
      {
        var author = new Author { Name = _authorName };
        
        _mock.Setup(s => s.Save(author)).Returns(author);

        var useCase = new AuthorUseCase(_mock.Object);

        author = useCase.Save(author);

        Assert.True(author != null && author.Name == _authorName);
      }

      [Fact]
      public void GetAllShouldReturnListOfAuthors()
      {
        var collectionOfAuthors = new List<Author> { new Author { Name = _authorName } };

        _mock.Setup(s => s.GetAll()).Returns(collectionOfAuthors);

        var useCase = new AuthorUseCase(_mock.Object);

        collectionOfAuthors = useCase.GetAll().ToList();

        Assert.True(collectionOfAuthors != null && collectionOfAuthors.Count == 1);
      }

      [Fact]
      public void GetAllShouldReturnNull()
      {
        _mock.Setup(s => s.GetAll()).Returns<IEnumerable<Author>>(null);

        var useCase = new AuthorUseCase(_mock.Object);

        var collectionOfAuthors = useCase.GetAll();

        Assert.True(collectionOfAuthors == null);
      }

      [Fact]
      public void GetByIdShouldReturnOneAuthor()
      {
        var author = new Author { Name = _authorName };

        _mock.Setup(s => s.GetById(1)).Returns(author);

        var useCase = new AuthorUseCase(_mock.Object);

        author = useCase.GetById(1);

        Assert.True(author != null);
      }

      [Fact]
      public void GetByIdShouldReturnNull()
      {
        _mock.Setup(s => s.GetById(2)).Returns<Author>(null);

        var useCase = new AuthorUseCase(_mock.Object);

        var author = useCase.GetById(1);

        Assert.True(author == null);
      }
    }
}
