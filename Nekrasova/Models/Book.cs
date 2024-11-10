namespace Nekrasova.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string BookName { get; set; }
        public int Language_ID { get; set; }
        public int Category_ID { get; set; }
        public string About { get; set; }
        public string CoverPath { get; set; }
        public decimal Price { get; set; }
        public int Weight { get; set; }
    }
}
