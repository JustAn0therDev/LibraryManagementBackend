using Entities;
using System;
using System.Collections.Generic;
using UseCases.Interfaces;
using Repositories.Interfaces;

namespace UseCases
{
    public class GenreUseCase : IGenreUseCase
    {
        private readonly IGenreRepository _genreRepository;

        public GenreUseCase(IGenreRepository genreRepository) => _genreRepository = genreRepository;

        public IEnumerable<Genre> GetAll()
        {
            return _genreRepository.GetAll();
        }

        public Genre GetById(int id)
        {
            return _genreRepository.GetById(id);
        }

        public Genre MakeObject(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                UseCaseUtils.ThrowArgumentNullException(nameof(name));
            }

            return new Genre
            {
                Name = name
            };
        }

        public Genre Save(Genre genre)
        {
            if (genre == null) 
            {
                throw new ArgumentNullException("Entity not provided");
            }

            _genreRepository.Save(genre);

            return genre;
        }
    }
}
