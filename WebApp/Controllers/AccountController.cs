using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Olo42.SAROM.DataAccess.Contracts;
using Olo42.SAROM.WebApp.Models.Account;

namespace Olo42.SAROM.WebApp.Controllers
{
    public class AccountController : Controller
  {
    private readonly IUserRepository userRepository;
    private readonly SignInManager<User> signInManager;

    public AccountController(
      IUserRepository userRepository,
      SignInManager<User> signInManager)
    {
      this.userRepository = userRepository;
      this.signInManager = signInManager;
    }

    public IActionResult AccessDenied()
    {
      return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
      this.CreateInitialUserIfNoUserExist();

      return View();
    }

    private void CreateInitialUserIfNoUserExist()
    {
      if (!this.userRepository.Get().Any())
      {
        var user = new User
        {
          LoginName = "first",
          FirstName = "First",
          LastName = "User",
          Password = "user"
        };
        this.userRepository.Add(user);
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([Bind("LoginName, Password")] Login login)
    {
      var user = this.userRepository.Get(login?.LoginName);
      if (user == null || user?.Password != login.Password)
      {
        login.Failed = true;

        return View(login);
      }

      await this.signInManager.SignInAsync(user, true, null);
      
      return RedirectToAction("Index", "Operations");
    }

    public async Task<IActionResult> Logout()
    {
      await this.signInManager.SignOutAsync();

      return RedirectToAction("Index", "Home");
    }
  }
}