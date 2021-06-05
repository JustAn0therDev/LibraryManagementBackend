using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using Entities;
using LibraryManagementBackend.Controllers;
using UseCases.Interfaces;

namespace Tests
{
    public class GenreControllerTests
    {
        [Fact]
        public void GetShouldReturnListOfGenres()
        {
            var mock = new Mock<IGenreUseCase>();

            mock.Setup(s => s.MakeObject("")).Returns(new Genre {
                    ID = 1,
                    Name = "Science & Technology"
            });
            
            var controller = new GenreController(mock.Object);

            Assert.True(controller.Get() != null);
        }
    }
}
