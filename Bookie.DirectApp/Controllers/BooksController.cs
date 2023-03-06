using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bookie.DirectApp.Data;
using Bookie.DirectApp.Models;
using Bookie.DirectApp.Services;
using Bookie.DirectApp.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Bookie.DirectApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly IConfiguration _config;
        private IBooksService _booksService;
        private IAuthorsService _authorsService;

        public BooksController(IConfiguration config, IBooksService booksService, IAuthorsService authorsService)
        {
            _config = config;
            _booksService = booksService;
            _authorsService = authorsService;
        }

        // GET: Books
        [Authorize]
        public async Task<IActionResult> Index(BooksList posted)
        {

            var books = await _booksService.GetBooks(posted.Filters, 40);

            var booksList = new BooksList();
            foreach (var book in books)
            {
                booksList.Books.Add(book.ToViewModel());
            }

            booksList.PopulateFilterOptions();

            return View("Index", booksList);
        }

        // GET: Books/Details/5
        [Authorize]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _booksService.GetBook(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            var bookModel = book.ToViewModel();

            SetAuthors(bookModel);

            SetIsFavorite(bookModel);

            return View(bookModel);
        }

        private void SetAuthors(BookViewModel bookModel)
        {
            var authors = _authorsService.GetBookAuthors(bookModel.BookId);
            bookModel.Authors = string.Join(", ", authors.Select(a => a.Name));
        }

        private void SetIsFavorite(BookViewModel bookModel) 
        {
            bookModel.IsFavorite = _booksService.IsInUserFavorites(GetUserEmail(), bookModel.BookId);
        }

        //// GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        //// POST: Books/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("BookId,Title,Description,PublishedOn,Publisher,Price,ImageUrl")] Book book)
        {
            if (ModelState.IsValid)
            {
                _booksService.AddBook(book);
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        //// GET: Books/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _booksService.GetBook(id.Value);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        //// POST: Books/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("BookId,Title,Description,PublishedOn,Publisher,Price,ImageUrl")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _booksService.UpdateBook(id, book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_booksService.Exists(book.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        //// GET: Books/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _booksService.GetBook(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        //// POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = _booksService.GetBook(id);
            if (book != null)
            {
                _booksService.DeleteBook(id);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddAuthor(int id) 
        {
            var book = _booksService.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }

            var viewModel = new AddAuthor { BookId = id };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddAuthor(int id, AddAuthor posted) 
        {
            _booksService.AddAuthorToBook(id, posted.AuthorName);

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public IActionResult Favorite(int id) 
        {
            var email = GetUserEmail();

            _booksService.AddToFavorites(email, id);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        private string GetUserEmail() 
        {
            var email = User.Claims.First(c => c.Properties.Any(p => p.Value == "email"));
            return email.Value;
        }
    }
}
