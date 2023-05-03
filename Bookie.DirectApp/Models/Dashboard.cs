namespace Bookie.DirectApp.Models
{
    public class Dashboard
    {
        public int TotalBooks { get; set; }
        public int TotalAuthors { get; set; }
        public decimal AvgBookPrice { get; set; }
        public int TotalReviews { get; set; }
        public int TotalBooksWithReviews { get; set; }
    }
}
