using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Olo42.SAROM.Logic.Users;
using Olo42.SAROM.WebApp.Models.Account;

namespace Olo42.SAROM.WebApp.Controllers
{
  public class AccountController : Controller
  {
    private readonly IUserManager userManager;
    private readonly SignInManager<User> signInManager;

    public AccountController(
      IUserManager userManager,
      SignInManager<User> signInManager)
    {
      this.userManager = userManager;
      this.signInManager = signInManager;
    }

    public IActionResult AccessDenied()
    {
      return View();
    }

    [HttpGet]
    public async Task<IActionResult> LoginAsync()
    {
      await this.CreateInitialUserIfNoUserExistAsync();

      return View();
    }

    private async Task CreateInitialUserIfNoUserExistAsync()
    {
      if (!this.userManager.FileExists)
      {
        var user = new User
        {
          LoginName = "first",
          FirstName = "First",
          LastName = "User",
          Password = "user"
        };

        await this.userManager.Store(user);
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([Bind("LoginName, Password")] Login login)
    {
      var allUsers = await this.userManager.Get();
      var loginUser = allUsers
        .Where(u => u.LoginName == login?.LoginName)
        .Where(u => u.Password == login?.Password);

      if (!loginUser.Any())
      {
        login.Failed = true;

        return View(login);
      }

      await this.signInManager.SignInAsync(loginUser.First(), true, null);

      return RedirectToAction("Index", "Operations");
    }

    public async Task<IActionResult> Logout()
    {
      await this.signInManager.SignOutAsync();

      return RedirectToAction("Index", "Home");
    }
  }
}