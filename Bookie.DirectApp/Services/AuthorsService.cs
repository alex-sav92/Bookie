using BenchmarkDotNet.Attributes;
using Bookie.Data;
using Bookie.Data.Entities;
using Bookie.DirectApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookie.DirectApp.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly BookieDirectAppContext _context;

        public AuthorsService(BookieDirectAppContext context)
        {
            _context = context;
        }

        public int AddAuthor(Author author)
        {
            _context.Author.Add(author);
            var result = _context.SaveChanges();

            return result;
        }

        public void DeleteAuthor(int id)
        {
            var current = _context.Author.Find(id);
            if (current != null)
            {
                _context.Author.Remove(current);
                _context.SaveChanges();
            }
        }

        public Author? GetAuthor(int id)
        {
            var author = _context.Author.Find(id);
            return author ?? null;
        }

        public List<Author> GetAuthors()
        {
            return _context.Author.ToList();
        }

        public List<Author> GetBookAuthors(int bookId)
        {
            var book = _context.Books
                .Include(b => b.AuthorsLink)
                .First(b => b.BookId == bookId);
            List<Author> result = new List<Author>();
            if (book.AuthorsLink != null) 
            {
                foreach (var authorLink in book.AuthorsLink.ToList())
                {
                    var author = _context.Author.Find(authorLink.AuthorId);
                    if (author != null) 
                    {
                        result.Add(author);
                    }   
                }
            }
            return result;
        }

        public void UpdateAuthor(int id, Author author)
        {
            var currentAuthor = _context.Author.Find(id);
            if (currentAuthor != null)
            {
                currentAuthor.Name = author.Name;

                _context.Author.Update(currentAuthor);
                _context.SaveChanges();
            }
        }

        public int Count() 
        { 
            return _context.Author.Count();
        }
    }
}
