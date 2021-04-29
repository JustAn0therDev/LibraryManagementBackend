using System.Collections.Generic;
using System.Linq;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class LibraryContext : DbContext, IBookRepository, IPublisherRepository, IAuthorRepository, IGenreRepository
    {
        public LibraryContext(DbContextOptions options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public IEnumerable<Book> GetAll()
        {
            return Books.Select(s => s);
        }

        public Book Save(Book book)
        {
            Books.Add(book);
            SaveChanges();

            return book;
        }

        IEnumerable<Genre> IGenreRepository.GetAll()
        {
            return Genres.Select(s => s);
        }

        public Genre Save(Genre genre)
        {
            Genres.Add(genre);
            SaveChanges();

            return genre;
        }

        IEnumerable<Publisher> IPublisherRepository.GetAll()
        {
            return Publishers.Select(s => s);
        }

        public Publisher Save(Publisher publisher)
        {
            Publishers.Add(publisher);
            SaveChanges();

            return publisher;
        }

        IEnumerable<Author> IAuthorRepository.GetAll()
        {
            return Authors.Select(s => s);
        }

        public Author Save(Author author)
        {
            Authors.Add(author);
            SaveChanges();

            return author;
        }
    }
}
