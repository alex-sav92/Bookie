﻿using BenchmarkDotNet.Attributes;
using Bookie.Data;
using Bookie.Data.Entities;
using Bookie.DirectApp.Models;
using Bookie.DirectApp.ViewModels;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Security.Policy;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        private IQueryable<Book> GetBooks()
        {
            return _context.Books.AsQueryable();
        }

        public async Task<List<Book>> GetBooks(BookFilterOptions filters, int? limit = null) 
        {
            var filtered = GetBooks();
            if (!string.IsNullOrEmpty(filters.Title))
            {
                filtered = filtered.Where(b => b.Title.Contains(filters.Title));
            }
            if (filters.PriceFrom != 0 && filters.PriceTo != 0) 
            {
                filtered = filtered.Where(b => b.Price >= filters.PriceFrom && b.Price <= filters.PriceTo);
            }

            if (filters.PublishTime != null) 
            {
                var publishLimits = filters.GetPublishTimes();

                filtered = filtered.Where(b => b.PublishedOn >= publishLimits.Item1 && b.PublishedOn <= publishLimits.Item2);
            }

            if (filters.SortOption != null) 
            {
                switch (filters.SortOption) 
                {
                    case "ByPriceAsc":
                        filtered = filtered.OrderBy(b => b.Price);
                        break;
                    case "ByPriceDesc":
                        filtered = filtered.OrderByDescending(b => b.Price); 
                        break;
                    case "ByMostRecent":
                        filtered = filtered.OrderByDescending(b => b.PublishedOn); 
                        break;
                    case "ByOldest":
                        filtered = filtered.OrderBy(b => b.PublishedOn); 
                        break;

                    default:
                        filtered = filtered.OrderByDescending(b => b.PublishedOn);
                        break;
                }
            }

            if (limit.HasValue) 
            {
                filtered = filtered.Take(limit.Value);
            }

            return await filtered.ToListAsync();
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

        public void AddToFavorites(string email, int id)
        {
            _context.Favorites.Add(new Favorite { UserEmail = email, BookId = id });
            _context.SaveChanges();
        }

        public bool IsInUserFavorites(string email, int id)
        {
            var fav = _context.Favorites.FirstOrDefault(f => f.UserEmail == email && f.BookId == id);

            return fav != null;
        }

        public async Task<List<Book>> GetFavoriteBooks(string email) 
        {
            var ids = await _context.Favorites
                .Where(f => f.UserEmail == email)
                .Select(b => b.BookId)
                .ToListAsync();

            var books = GetBooks().Where(b => ids.Contains(b.BookId));

            return await books.ToListAsync();
        }

        public decimal AveragePrice() 
        {
            //can do tests here
            var x = _context.Books.Sum(b => b.Price) / _context.Books.Count();

            return x;
        }

        public int Count() 
        { 
            return _context.Books.Count(); 
        }
        
        public void TestEFExceptions() 
        {
            try
            {
                _context.Books.Add(new Book
                {
                    Description = "soon",
                    Price = 2,
                    ImageUrl = "pics.com/book1",
                    Publisher = "Nemira",
                    Title = "Once upon a time there was a queen"
                });

                _context.SaveChanges();
            }
            catch (UniqueConstraintException e)
            {
                //Handle exception here
            }
            catch (CannotInsertNullException e)
            {
                //Handle exception here
            }
            catch (MaxLengthExceededException e)
            {
                //Handle exception here
            }
        }


        public Task<List<Book>> TestReadAllEntities()
        {
            return _context.Books.ToListAsync();
        }

        public (int, int) CountBooksWithReviews()
        {
            var books = _context.Books
                .Include(b => b.Reviews)
                .Where(b => b.Reviews != null && b.Reviews.Count > 0);

            var countBooks = books.Count();

            var countReviews = _context.Review.Count();

            return (countBooks, countReviews);
        }

        public Dictionary<string, int> GetTopPublishers(int count)
        {
            var countByPublisher = _context.Books.AsQueryable()
                .GroupBy(b => b.Publisher)
                .Select(g => new { Publisher = g.Key, CountBooks = g.Count() })
                .OrderByDescending(o => o.CountBooks)
                .Take(count);

            return countByPublisher.ToDictionary(o => o.Publisher, o => o.CountBooks);
        }
    }
}
