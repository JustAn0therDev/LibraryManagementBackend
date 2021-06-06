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
	public class PublisherUseCaseTests
    {
      private readonly string _publisherName = "Science";
      private readonly Mock<IPublisherRepository> _mock = new Mock<IPublisherRepository>();

      [Fact]
      public void MakeObjectShouldReturnPublisher()
      {
        var useCase = new PublisherUseCase(_mock.Object);

        Assert.True(useCase.MakeObject(_publisherName) != null);
      }

			[Fact]
			public void MakeObjectShouldThrowExceptionWhenStringEmpty()
			{
				var useCase = new PublisherUseCase(_mock.Object);

				Assert.Throws<ArgumentNullException>(() => { useCase.MakeObject(string.Empty); });
			}

			[Fact]
			public void MakeObjectShouldThrowExceptionWhenStringIsWhiteSpace()
			{
				var useCase = new PublisherUseCase(_mock.Object);

				Assert.Throws<ArgumentNullException>(() => { useCase.MakeObject("     "); });
			}

			[Fact]
			public void SaveShouldReturnTheSameObject()
			{
				var publisher = new Publisher { Name = _publisherName };
				
				_mock.Setup(s => s.Save(publisher)).Returns(publisher);

				var useCase = new PublisherUseCase(_mock.Object);

				publisher = useCase.Save(publisher);

				Assert.True(publisher != null && publisher.Name == _publisherName);
			}

      [Fact]
			public void GetAllShouldReturnListOfPublishers()
			{
				var collectionOfPublishers = new List<Publisher> { new Publisher { Name = _publisherName } };

				_mock.Setup(s => s.GetAll()).Returns(collectionOfPublishers);

				var useCase = new PublisherUseCase(_mock.Object);

				collectionOfPublishers = useCase.GetAll().ToList();

				Assert.True(collectionOfPublishers != null && collectionOfPublishers.Count == 1);
			}

			[Fact]
			public void GetAllShouldReturnNull()
			{
				_mock.Setup(s => s.GetAll()).Returns<IEnumerable<Publisher>>(null);

				var useCase = new PublisherUseCase(_mock.Object);

				var collectionOfPublishers = useCase.GetAll();

				Assert.True(collectionOfPublishers == null);
			}

			[Fact]
			public void GetByIdShouldReturnOnePublisher()
			{
				var publisher = new Publisher { Name = _publisherName };
				
				_mock.Setup(s => s.GetById(1)).Returns(publisher);

				var useCase = new PublisherUseCase(_mock.Object);

				publisher = useCase.GetById(1);

				Assert.True(publisher != null);
			}

			[Fact]
			public void GetByIdShouldReturnNull()
			{
				_mock.Setup(s => s.GetById(2)).Returns<Publisher>(null);

				var useCase = new PublisherUseCase(_mock.Object);

				var publisher = useCase.GetById(1);

				Assert.True(publisher == null);
			}
    }
}
