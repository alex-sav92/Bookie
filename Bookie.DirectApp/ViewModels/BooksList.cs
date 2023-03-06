
namespace Bookie.DirectApp.ViewModels
{
    public class BooksList
    {
        public IList<BookViewModel> Books { get; set; }

        public BookFilterOptions Filters { get; set; }


        public BooksList()
        {
            Books = new List<BookViewModel>();
            Filters = new BookFilterOptions();
        }

        public void PopulateFilterOptions()
        {
            Filters.Title = "";
            Filters.PriceFrom = 1;
            Filters.PriceTo = 999;
            Filters.Categories = new List<string>
            {
                "Technical", "Personal improvement", "Fiction", "NonFiction"
            };
            Filters.PublishTimes = new Dictionary<string, string>
            {
                { "Current year", "Current year" },
                { "Past 2 years", "Past 2 years" },
                { "Past 5 years", "Past 5 years" },
                { "All times", "All times" }
            };
            Filters.SortOptions = new Dictionary<string, string>
            {
                { "By Price 🠕", "ByPriceAsc" },
                { "By Price 🠗", "ByPriceDesc" },
                { "By Most Recent", "ByMostRecent" },
                { "By oldest", "ByOldest" }
            };
        }
    }

    public class BookFilterOptions 
    {
        public static decimal DefaultPriceFrom = 1;
        public static decimal DefaultPriceTo = 999;

        public string Title { get; set; }

        public DateTime PublishedFrom { get; set; }
        public DateTime PublishedTo { get; set; }

        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; }

        public string Category { get; set; }
        public List<string> Categories { get; set; }

        public string PublishTime { get; set; }
        public Dictionary<string, string> PublishTimes { get; set; }

        public string SortOption { get; set; }
        public Dictionary<string, string> SortOptions { get; set; }

        public Tuple<DateTime, DateTime> GetPublishTimes() 
        {
            if (PublishTime == "Current year") 
            {
                return new Tuple<DateTime, DateTime>(new DateTime(DateTime.Now.Year, 1, 1), new DateTime(DateTime.Now.Year, 12, 12));
            }
            if (PublishTime == "Past 2 years")
            {
                return new Tuple<DateTime, DateTime>(new DateTime(DateTime.Now.Year - 1 , 1, 1), new DateTime(DateTime.Now.Year, 12, 12));
            }
            if (PublishTime == "Past 5 years")
            {
                return new Tuple<DateTime, DateTime>(new DateTime(DateTime.Now.Year - 5, 1, 1), new DateTime(DateTime.Now.Year, 12, 12));
            }

            return new Tuple<DateTime, DateTime>(DateTime.MinValue, DateTime.MaxValue);
        }

    }
}
