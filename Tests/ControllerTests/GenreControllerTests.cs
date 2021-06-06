using Entities;
using LibraryManagementBackend.Controllers;
using Moq;
using UseCases.Interfaces;
using Xunit;

namespace Tests.ControllerTests
{
    public class GenreControllerTests
    {
        [Fact]
        public void GetShouldReturnListOfGenres()
        {
            var genreName = "Science & Technology";
            var mock = new Mock<IGenreUseCase>();

            mock.Setup(s => s.MakeObject(genreName)).Returns(new Genre
            {
                ID = 1,
                Name = genreName
            });

            var controller = new GenreController(mock.Object);

            Assert.True(controller.Get() != null);
        }
    }
}
