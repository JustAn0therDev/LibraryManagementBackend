using Entities;
using System.Collections.Generic;
using UseCases.Interfaces;
using Repositories.Interfaces;

namespace UseCases
{
    public class PublisherUseCase : IPublisherUseCase
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublisherUseCase(IPublisherRepository publisherRepository) => _publisherRepository = publisherRepository;

        public IEnumerable<Publisher> GetAll()
        {
            return _publisherRepository.GetAll();
        }

        public Publisher GetById(int id)
        {
            return _publisherRepository.GetById(id);
        }

        public Publisher MakeObject(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                UseCaseUtils.ThrowArgumentNullException(nameof(name));
            }

            return new Publisher
            {
                Name = name
            };
        }

        public Publisher Save(Publisher publisher)
        {
            if (publisher == null) 
            {
                UseCaseUtils.ThrowArgumentNullException(nameof(publisher));
            }

            _publisherRepository.Save(publisher);

            return publisher;
        }
    }
}
