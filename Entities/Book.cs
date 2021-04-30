namespace Entities
{
    public class Book
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? GenreID { get; set; }
        public Genre Genre { get; set; }
        public int? AuthorID { get; set; }
        public Author Author { get; set; }
        public int? PublisherID { get; set; }
        public Publisher Publisher { get; set; }
    }
}
