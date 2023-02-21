using Bookie.DirectApp.Data;
using Bookie.DirectApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookie.DirectApp.Services
{
    public class BooksService : IBooksService
    {
        private readonly BookieDirectAppContext _context;

        public BooksService(BookieDirectAppContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }

        public Book? GetBook(int id) 
        {
            var book = _context.Books
                .Include(a => a.AuthorsLink)
                .First(b => b.BookId == id);
            return book ?? null;
        }

        public bool Exists(int id) 
        {
            var book = GetBook(id);
            return book != null;
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
                currentBook.Price = newBook.Price;
                
                _context.Books.Update(currentBook);
                _context.SaveChanges();
            }
        }

        public void AddAuthorToBook(int id, string authorName) 
        {
            var book = _context.Books
                .Include(b => b.AuthorsLink)
                .First(b => b.BookId == id);
            if (book != null) 
            {
                var lastAuthorPosition = (book.AuthorsLink != null) ?
                    book.AuthorsLink.OrderByDescending(a => a.DisplayOrder).First().DisplayOrder : 0;
                var author = _context.Author.FirstOrDefault(a => a.Name == authorName);
                if (author != null)
                {
                    book.AuthorsLink?.Add(
                        new BookAuthor 
                        { 
                            AuthorId = author.AuthorId, 
                            BookId = id, 
                            DisplayOrder = lastAuthorPosition + 1 
                        });
                }
                else 
                {
                    _context.Author.Add(new Author { Name = authorName });
                    _context.SaveChanges();
                    var newAuthor = _context.Author.First(a => a.Name == authorName);
                    _context.BookAuthor.Add(new BookAuthor { AuthorId = newAuthor.AuthorId, BookId = id, DisplayOrder = 99 });
                }
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
        public Task<List<Book>> GetAllBooks();
        public Book? GetBook(int id);
        public int AddBook(Book book);
        public void UpdateBook(int id, Book newBook);
        public void DeleteBook(int id);

        public bool Exists(int id);
        public void AddAuthorToBook(int id, string authorName);
    }
}
