using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Olo42.SAROM.DataAccess.Contracts;
using Olo42.SAROM.WebApp.Models.Account;

namespace Olo42.SAROM.WebApp.Controllers
{
    public class AccountController : Controller
  {
    private readonly IUserRepository userRepository;

    public AccountController(IUserRepository userRepository)
    {
      this.userRepository = userRepository;
    }

    public IActionResult AccessDenied()
    {
      return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([Bind("LoginName, Password")] Login login)
    {
      var user = this.userRepository.Get(login.LoginName);
      if (user == null || user?.Password != login.Password)
      {
        login.ErrorMessage = "Ungültige Zugangsdaten!";  // TODO: I18n

        return View(login);
      }

      var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
      identity.AddClaim(new Claim(ClaimTypes.Name, user.LoginName));
      identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));
      identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));

      var principal = new ClaimsPrincipal(identity);
      await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).ConfigureAwait(false);
      
      return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
      await HttpContext.SignOutAsync().ConfigureAwait(false);

      return RedirectToAction(nameof(Login));
    }
  }
}