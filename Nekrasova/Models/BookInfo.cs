namespace Nekrasova.Models
{
    public class BookInfo
    {
        public List<Book> books { get; set; }
        public List<Author> author { get; set; }
        public List<Genre> genre { get; set; }
        public List<PublishingHouse> publishingHouse { get; set; }
        public List<BookAuthor> bookAuthor { get; set; }
        public List<BookGenre> bookGenres { get; set; }
        public  List<BookPBH> bookPBH { get; set; }
        public List<Category> category { get; set; }
        public List<Language> language { get; set; }
        public AppDbContext DbContext { get; set; }

    }
}
