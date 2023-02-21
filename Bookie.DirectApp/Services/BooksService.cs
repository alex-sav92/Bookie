using Bookie.DirectApp.Data;
using Bookie.DirectApp.Models;

namespace Bookie.DirectApp.Services
{
    public class BooksService : IBooksService
    {
        private readonly BookieDirectAppContext _context;

        public BooksService(BookieDirectAppContext context)
        {
            _context = context;
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public Book? GetBook(int id) 
        {
            var book = _context.Books.Find(id);
            return book ?? null;
        }

        public int AddBook(Book book) 
        {
            _context.Books.Add(book);
            var result = _context.SaveChanges();

            return result;
        }

        public void UpdateBook(int id, Book newBook) 
        {
            var currentBook = _context.Books.Find(id);
            if (currentBook != null) 
            { 
                currentBook.Description = newBook.Description;
                currentBook.Title = newBook.Title;
                currentBook.PublishedOn = newBook.PublishedOn;
                
                _context.Books.Update(currentBook);
                _context.SaveChanges();
            }
        }

        public void DeleteBook(int id)
        {
            var currentBook = _context.Books.Find(id);
            if (currentBook != null)
            {
                _context.Books.Remove(currentBook);
                _context.SaveChanges();
            }
        }
    }

    public interface IBooksService
    {
        public List<Book> GetAllBooks();
        public Book? GetBook(int id);
        public int AddBook(Book book);
        public void UpdateBook(int id, Book newBook);
        public void DeleteBook(int id);
    }
}
