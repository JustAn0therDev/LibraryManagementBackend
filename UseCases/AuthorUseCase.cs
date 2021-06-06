using Entities;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using UseCases.Interfaces;

namespace UseCases
{
    public class AuthorUseCase : IAuthorUseCase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorUseCase(IAuthorRepository authorRepository) => _authorRepository = authorRepository;

        public IEnumerable<Author> GetAll()
        {
            return _authorRepository.GetAll();
        }

        public Author GetById(int id)
        {
            return _authorRepository.GetById(id);
        }

        /// <summary>
        /// Creates an object of the given use case type (e.g. AuthorUseCase.MakeObject method returns an Author object).
        /// This method should validate any business rules regarding the desired object.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Author MakeObject(string name)
        {

            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)) 
            {
                throw new ArgumentNullException(nameof(name), $"A value for {nameof(name)} must be provided.");
            }

            return new Author
            {
                Name = name
            };
        }

        public Author Save(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author), $"A value for {nameof(author)} must be provided");
            }

            _authorRepository.Save(author);

            return author;
        }
    }
}
