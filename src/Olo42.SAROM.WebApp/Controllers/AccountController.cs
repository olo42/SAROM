using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Olo42.SAROM.DataAccess.Contracts;

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
    public async Task<IActionResult> Login(string loginName, string password)
    {
      var user = this.userRepository.Get(loginName);
      if (user == null || user?.Password != password)
      {
        ModelState.AddModelError("Credentials", "Access denied");
        return View();
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