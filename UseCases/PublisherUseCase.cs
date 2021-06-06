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

        /// <summary>
        /// Fetches all records of Publisher in the repository
        /// </summary>
        /// <returns>IEnumerable<Publisher> collection</returns>
        public IEnumerable<Publisher> GetAll()
        {
            return _publisherRepository.GetAll();
        }

        /// <summary>
        /// Fetches an object by its repository ID.
        /// </summary>
        /// <param name="id">Publisher repository ID</param>
        /// <returns>Object of Publisher type</returns>
        public Publisher GetById(int id)
        {
            return _publisherRepository.GetById(id);
        }

        /// <summary>
        /// Creates an object of the given use case type (e.g. AuthorUseCase.MakeObject method returns an Author object).
        /// This method should validate any business rules regarding the desired object.
        /// </summary>
        /// <param name="name">Publisher name</param>
        /// <returns>Object of Publisher type</returns>
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

        /// <summary>
        /// Persists the desired data in the repository.
        /// </summary>
        /// <param name="publisher">Publisher object</param>
        /// <returns>The same object. Might raise an exception if a problem occurs during saving.</returns>
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
