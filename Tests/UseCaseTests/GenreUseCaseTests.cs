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
	public class GenreUseCaseTests
    {
      private readonly string _genreName = "Science";
      private readonly Mock<IGenreRepository> _mock = new Mock<IGenreRepository>();

      [Fact]
      public void MakeObjectShouldReturnGenre()
      {
        var useCase = new GenreUseCase(_mock.Object);

        Assert.True(useCase.MakeObject(_genreName) != null);
      }

			[Fact]
			public void MakeObjectShouldThrowExceptionWhenStringEmpty()
			{
				var useCase = new GenreUseCase(_mock.Object);

				Assert.Throws<ArgumentNullException>(() => { useCase.MakeObject(string.Empty); });
			}

			[Fact]
			public void MakeObjectShouldThrowExceptionWhenStringIsWhiteSpace()
			{
				var useCase = new GenreUseCase(_mock.Object);

				Assert.Throws<ArgumentNullException>(() => { useCase.MakeObject("     "); });
			}

			[Fact]
			public void SaveShouldReturnTheSameObject()
			{
				var genre = new Genre { Name = _genreName };
				
				_mock.Setup(s => s.Save(genre)).Returns(genre);

				var useCase = new GenreUseCase(_mock.Object);

				genre = useCase.Save(genre);

				Assert.True(genre != null && genre.Name == _genreName);
			}

      [Fact]
			public void GetAllShouldReturnListOfGenres()
			{
				var collectionOfGenres = new List<Genre> { new Genre { Name = _genreName } };

				_mock.Setup(s => s.GetAll()).Returns(collectionOfGenres);

				var useCase = new GenreUseCase(_mock.Object);

				collectionOfGenres = useCase.GetAll().ToList();

				Assert.True(collectionOfGenres != null && collectionOfGenres.Count == 1);
			}

			[Fact]
			public void GetAllShouldReturnNull()
			{
				_mock.Setup(s => s.GetAll()).Returns<IEnumerable<Genre>>(null);

				var useCase = new GenreUseCase(_mock.Object);

				var collectionOfGenres = useCase.GetAll();

				Assert.True(collectionOfGenres == null);
			}

			[Fact]
			public void GetByIdShouldReturnOneGenre()
			{
				var genre = new Genre { Name = _genreName };
				
				_mock.Setup(s => s.GetById(1)).Returns(genre);

				var useCase = new GenreUseCase(_mock.Object);

				genre = useCase.GetById(1);

				Assert.True(genre != null);
			}

			[Fact]
			public void GetByIdShouldReturnNull()
			{
				_mock.Setup(s => s.GetById(2)).Returns<Genre>(null);

				var useCase = new GenreUseCase(_mock.Object);

				var genre = useCase.GetById(1);

				Assert.True(genre == null);
			}
    }
}
