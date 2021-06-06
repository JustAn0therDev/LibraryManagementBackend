using Entities;
using System.Collections.Generic;
using UseCases.Interfaces;
using Repositories.Interfaces;
using System;

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
                throw new ArgumentNullException(nameof(name), $"A value for {nameof(name)} must be provided");
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
                throw new ArgumentNullException(nameof(publisher), $"A value for {nameof(publisher)} must be provided");
            }

            _publisherRepository.Save(publisher);

            return publisher;
        }
    }
}
