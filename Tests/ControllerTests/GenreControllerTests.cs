using Entities;
using LibraryManagementBackend.Controllers;
using Moq;
using UseCases.Interfaces;
using System.Collections.Generic;
using Xunit;
using System;

namespace Tests.ControllerTests
{
    public class GenreControllerTests
    {
        private readonly string _genreName = "Science & Technology";
        private readonly Mock<IGenreUseCase> _mock = new Mock<IGenreUseCase>();

        [Fact]
        public void GetShouldReturnListOfGenres()
        {
            _mock.Setup(s => s.GetAll()).Returns(new List<Genre>
            {
                new Genre 
                {
                    ID = 1,
                    Name = _genreName
                }
            });

            var controller = new GenreController(_mock.Object);

            var response = controller.Get();

            Assert.True(response.StatusCode == 200 && response.Value as List<Genre> != null);
        }

        [Fact]
        public void GetShouldReturnInternalServerError()
        {
            _mock.Setup(s => s.GetAll()).Throws<Exception>();

            var controller = new GenreController(_mock.Object);

            var response = controller.Get();

            Assert.True(response.StatusCode == 500 && response.Value as List<Genre> == null);
        }

        [Fact]
        public void PostShouldReturnGenre()
        {
            var genre = new Genre { Name = _genreName };

            _mock.Setup(s => s.MakeObject(_genreName)).Returns(genre);
            _mock.Setup(s => s.Save(genre)).Returns(genre);

            var controller = new GenreController(_mock.Object);

            var response = controller.Post(genre);

            genre = response.Value as Genre;

            Assert.True(response.StatusCode == 201 && genre != null);
        }

        [Fact]
        public void PostShouldReturnBadRequest()
        {
            var genre = new Genre { Name = _genreName };

            _mock.Setup(s => s.MakeObject(_genreName)).Throws<ArgumentNullException>();

            var controller = new GenreController(_mock.Object);

            var response = controller.Post(genre);

            Assert.True(response.StatusCode == 400 && response.Value as Genre is null);
        }

        [Fact]
        public void PostShouldReturnInternalServerError()
        {
            var genre = new Genre { Name = _genreName };

            _mock.Setup(s => s.MakeObject(_genreName)).Throws<Exception>();

            var controller = new GenreController(_mock.Object);

            var response = controller.Post(genre);

            Assert.True(response.StatusCode == 500 && response.Value as Genre is null);
        }
    }
}
