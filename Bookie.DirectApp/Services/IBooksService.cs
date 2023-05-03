using Bookie.Data.Entities;
using Bookie.DirectApp.ViewModels;

namespace Bookie.DirectApp.Services
{
    public interface IBooksService
    {
        public Task<List<Book>> GetAllBooks();
        public Task<List<Book>> GetBooks(BookFilterOptions filters, int? limit = null);

        public Book? GetBook(int id);
        public int AddBook(Book book);
        public void UpdateBook(int id, Book newBook);
        public void DeleteBook(int id);

        public bool Exists(int id);
        public void AddAuthorToBook(int id, string authorName);
        public void AddToFavorites(string email, int id);
        public Task<List<Book>> GetFavoriteBooks(string email);
        public bool IsInUserFavorites(string email, int id);

        public decimal AveragePrice();

        public int Count();

        public (int, int) CountBooksWithReviews();

        public Task<List<Book>> TestReadAllEntities();
    }
}
