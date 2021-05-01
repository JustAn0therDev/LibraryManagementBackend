namespace LibraryManagementBackend.Requests 
{
    public class CreateBook 
    {
        public string Name { get; set; }
        public int AuthorID { get; set; }
        public int PublisherID { get; set; }
        public int GenreID { get; set; }
    }
}