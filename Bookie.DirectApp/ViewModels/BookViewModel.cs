using Bookie.DirectApp.Models;

namespace Bookie.DirectApp.ViewModels
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Authors { get; set; }
        public string Price { get; set; }
        public string PublishedOn { get; set; }
        public string ImageUrl { get; set; }
        public string Publisher { get; set; }
    }

    public static class Extensions
    {
        public static BookViewModel ToViewModel(this Book book)
        {
            var vm = new BookViewModel();
            vm.Title = book.Title;
            vm.Description = book.Description;
            vm.Price = book.Price.ToString("##.## $");
            vm.PublishedOn = book.PublishedOn.Date.ToShortDateString();
            vm.Publisher = book.Publisher;
            vm.BookId = book.BookId;
            vm.ImageUrl = book.ImageUrl;

            return vm;
        }
    }
}
