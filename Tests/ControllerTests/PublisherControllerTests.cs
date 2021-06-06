using Entities;
using LibraryManagementBackend.Controllers;
using Moq;
using UseCases.Interfaces;
using Xunit;

namespace Tests.ControllerTests
{
    public class PublisherControllerTests
    {
        [Fact]
        public void GetShouldReturnListOfPublishers()
        {
            var publisherName = "testing";

            var mock = new Mock<IPublisherUseCase>();

            mock.Setup(s => s.MakeObject(publisherName)).Returns(new Publisher
            {
                ID = 1,
                Name = publisherName
            });

            var controller = new PublisherController(mock.Object);

            Assert.True(controller.Get() != null);
        }
    }
}
