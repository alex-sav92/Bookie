using BenchmarkDotNet.Attributes;
using Bookie.DirectApp.Services;
using Bookie.DirectApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsTCPIP;
using System.Diagnostics;
using System.Text;

namespace Bookie.DirectApp.Controllers
{
    public class HomeController : Controller
    {
        private IBooksService _booksService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IBooksService booksService)
        {
            _logger = logger;
            _booksService = booksService;
        }

        public IActionResult Index()
        {
            return View();
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