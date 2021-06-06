using Entities;
using LibraryManagementBackend.Controllers;
using Moq;
using UseCases.Interfaces;
using System.Collections.Generic;
using Xunit;
using System;

namespace Tests.ControllerTests
{
    public class PublisherControllerTests
    {
        private readonly string _publisherName = "Some publisher";
        private readonly Mock<IPublisherUseCase> _mock = new Mock<IPublisherUseCase>();

        [Fact]
        public void GetShouldReturnListOfPublishers()
        {
            _mock.Setup(s => s.GetAll()).Returns(new List<Publisher>
            {
                new Publisher 
                {
                    ID = 1,
                    Name = _publisherName
                }
            });

            var controller = new PublisherController(_mock.Object);

            var response = controller.Get();

            Assert.True(response.StatusCode == 200 && response.Value as List<Publisher> != null);
        }

        [Fact]
        public void GetShouldReturnInternalServerError()
        {
            _mock.Setup(s => s.GetAll()).Throws<Exception>();

            var controller = new PublisherController(_mock.Object);

            var response = controller.Get();

            Assert.True(response.StatusCode == 500 && response.Value as List<Publisher> == null);
        }

        [Fact]
        public void PostShouldReturnPublisher()
        {
            var publisher = new Publisher { Name = _publisherName };

            _mock.Setup(s => s.MakeObject(_publisherName)).Returns(publisher);
            _mock.Setup(s => s.Save(publisher)).Returns(publisher);

            var controller = new PublisherController(_mock.Object);

            var response = controller.Post(publisher);

            publisher = response.Value as Publisher;

            Assert.True(response.StatusCode == 201 && publisher != null);
        }

        [Fact]
        public void PostShouldReturnBadRequest()
        {
            var publisher = new Publisher { Name = _publisherName };

            _mock.Setup(s => s.MakeObject(_publisherName)).Throws<ArgumentNullException>();

            var controller = new PublisherController(_mock.Object);

            var response = controller.Post(publisher);

            Assert.True(response.StatusCode == 400);
        }

        [Fact]
        public void PostShouldReturnInternalServerError()
        {
            var publisher = new Publisher { Name = _publisherName };

            _mock.Setup(s => s.MakeObject(_publisherName)).Throws<Exception>();

            var controller = new PublisherController(_mock.Object);

            var response = controller.Post(publisher);

            Assert.True(response.StatusCode == 500);
        }
    }
}
