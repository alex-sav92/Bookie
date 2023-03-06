using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Bookie.DirectApp.Models.Account;
using Bookie.DirectApp.Services;

namespace Bookie.DirectApp.Controllers
{
    public class AccountController : Controller
    {
        private IBooksService _booksService;

        public AccountController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        public async Task Login(string returnUrl = "/")
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        [Authorize]
        public async Task Logout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri(Url.Action("Index", "Home"))
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var profile = new UserProfileViewModel()
            {
                Name = User.Identity.Name,
                EmailAddress = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value,
                ProfileImage = User.FindFirst(c => c.Type == "picture")?.Value
            };

            var favorites = await _booksService.GetFavoriteBooks(profile.EmailAddress);

            profile.FavoritesCount = favorites.Count;

            return View(profile);
        }

    }
}
