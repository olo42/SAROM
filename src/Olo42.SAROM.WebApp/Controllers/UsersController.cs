using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Olo42.SAROM.WebApp.Models;
using Olo42.SAROM.DataAccess.Contracts;
using System.Collections.Generic;
using Olo42.SAROM.WebApp.Logic;
using Microsoft.AspNetCore.Authorization;
using System.Threading;

namespace Olo42.SAROM.WebApp.Controllers
{
  [Authorize]
  public class UsersController : Controller
  {
    private readonly ISaromUserStore<User> userStore;

    public UsersController(ISaromUserStore<User> userStore)
    {
      this.userStore = userStore;
    }

    // GET: Users
    public async Task<IActionResult> Index()
    {
      var users = await this.userStore.GetAllUsers();
      var enumerableUserViewModel = this.CreateUserViewModel(users);

      return View(enumerableUserViewModel);
    }

    private IEnumerable<UserViewModel> CreateUserViewModel(
      IEnumerable<User> users)
    {
      foreach (var user in users)
      {
          yield return new UserViewModel {
            FirstName = user.FirstName,
            LastName = user.LastName,
            LoginName = user.LoginName,
            Password = user.Password
          };
      }
    }

    // GET: Users/Create
    public async Task<IActionResult> Create()
    {
      return View();
    }
  
    // POST: Users/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
      [Bind("FirstName, LastName, LoginName, Password, VerifyPassword")] 
        UserViewModel userViewModel)
    {
      if (ModelState.IsValid)
      {
        var user = this.CreateUser(userViewModel); 
        try{
          await this.userStore.CreateAsync(user, CancellationToken.None).ConfigureAwait(true);
        }
        catch(DuplicateUserException)
        {
          ModelState.TryAddModelError("Create", "User already exists!");

          return View();
        }

        return RedirectToAction("Index");
      }

      return View();
    }

    private User CreateUser(UserViewModel userViewModel)
    {
      // Set default user role as log as no role management is implemented
      var roles = new List<Role>{
        new Role { Name = ERoleName.User }
      };

      return new User {
        FirstName = userViewModel.FirstName,
        LastName = userViewModel.LastName,
        LoginName = userViewModel.LoginName,
        Password = userViewModel.Password,
        Roles = roles
      };
    }
  

  
  
  }
}