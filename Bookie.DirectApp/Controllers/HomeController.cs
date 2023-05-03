using BenchmarkDotNet.Attributes;
using Bookie.DirectApp.Models;
using Bookie.DirectApp.Services;
using Bookie.DirectApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsTCPIP;
using System.Diagnostics;
using System.Text;

namespace Bookie.DirectApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;
        private IBooksService _booksService;
        private IAuthorsService _authorsService;

        public HomeController(IConfiguration config, IBooksService booksService, IAuthorsService authorsService)
        {
            _config = config;
            _booksService = booksService;
            _authorsService = authorsService;
        }

        public async Task<IActionResult> Index()
        {
            var avgPrice = _booksService.AveragePrice();
            var countBooks = _booksService.Count();
            var countAuthors = _authorsService.Count();

            (int totalBooksWithReviews, int totalReviews) = _booksService.CountBooksWithReviews();

            var dash = new Dashboard
            {
                TotalBooks = countBooks,
                TotalAuthors = countAuthors,
                AvgBookPrice = avgPrice,
                TotalReviews = totalReviews,
                TotalBooksWithReviews = totalBooksWithReviews
            };

            return View(dash);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}