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

        public Author MakeObject(string name)
        {
            return new Author
            {
                Name = name
            };
        }

        public Author Save(Author author)
        {
            if (author == null) 
            {
                throw new ArgumentNullException("Entity not provided");
            }

            _authorRepository.Save(author);

            return author;
        }
    }
}
