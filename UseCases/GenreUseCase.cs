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

        /// <summary>
        /// Fetches all records of Genre in the repository
        /// </summary>
        /// <returns>IEnumerable<Genre> collection</returns>
        public IEnumerable<Genre> GetAll()
        {
            return _genreRepository.GetAll();
        }

        /// <summary>
        /// Fetches an object by its repository ID.
        /// </summary>
        /// <param name="id">Genre repository ID</param>
        /// <returns>Object of Genre type</returns>
        public Genre GetById(int id)
        {
            return _genreRepository.GetById(id);
        }

        /// <summary>
        /// Creates an object of the given use case type (e.g. AuthorUseCase.MakeObject method returns an Author object).
        /// This method should validate any business rules regarding the desired object.
        /// </summary>
        /// <param name="name">Genre name</param>
        /// <returns>Object of Genre type</returns>
        public Genre MakeObject(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), $"A value for {nameof(name)} must be provided.");
            }

            return new Genre
            {
                Name = name
            };
        }

        /// <summary>
        /// Persists the desired data in the repository.
        /// </summary>
        /// <param name="genre">Genre object</param>
        /// <returns>The same object. Might raise an exception if a problem occurs during saving.</returns>
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
