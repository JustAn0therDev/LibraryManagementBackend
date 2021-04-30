using Entities;
using Repositories;
using System;
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

        public Publisher MakeObject(string name)
        {
            return new Publisher
            {
                Name = name
            };
        }

        public Publisher Save(Publisher publisher)
        {
            if (publisher == null) 
            {
                throw new ArgumentNullException("Entity not provided");
            }

            _publisherRepository.Save(publisher);

            return publisher;
        }
    }
}
