using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Olo42.SAROM.WebApp.Models;
using System.Collections.Generic;
using Olo42.SAROM.WebApp.Logic;
using Microsoft.AspNetCore.Authorization;
using System.Threading;
using System;
using Olo42.SAROM.Logic.Users;
using AutoMapper;

namespace Olo42.SAROM.WebApp.Controllers
{
  [Authorize]
  public class UsersController : Controller
  {
    private readonly IUserManager userManager;
    private readonly IMapper mapper;

    public UsersController(IUserManager userManager, IMapper mapper)
    {
      this.userManager = userManager;
      this.mapper = mapper;
    }

    // GET: Users
    public async Task<IActionResult> Index()
    {
      var users = await this.userManager.Get();
      var usersViewModel = this.mapper.Map<IEnumerable<UserViewModel>>(users);

      return View(usersViewModel);
    }

    // private IEnumerable<UserViewModel> CreateUserViewModel(
    //   IEnumerable<User> users)
    // {
    //   foreach (var user in users)
    //   {
    //       yield return (UserViewModel)user;
    //   }
    // }

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
        await this.userManager.Store(user);

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

      return new User
      {
        Id = Guid.NewGuid().ToString(),
        FirstName = userViewModel.FirstName,
        LastName = userViewModel.LastName,
        LoginName = userViewModel.LoginName,
        Password = userViewModel.Password,
        Roles = roles
      };
    }

    public async Task<IActionResult> Details(string id)
    {
      var user = await this.userManager.Get(id);

      if (user == null)
      {
        // Pass an error somehow
        return RedirectToAction("Index");
      }

      var userViewModel = this.mapper.Map<UserViewModel>(user);

      return View(userViewModel);
    }

    public async Task<IActionResult> Edit(string id)
    {
      var user = await this.userManager.Get(id);
      var userViewModel = this.mapper.Map<UserViewModel>(user);

      return View(userViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
      [Bind("Id, FirstName, LastName, LoginName")] UserViewModel userViewModel)
    {
      if (userViewModel == null)
        throw new ArgumentNullException(nameof(userViewModel));

      var user = await this.userManager.Get(userViewModel.Id);
      if (user == null)
      {
        throw new UserNotFoundException($"User {userViewModel.Id} not found");
      }
      user.FirstName = userViewModel.FirstName;
      user.LastName = userViewModel.LastName;
      user.LoginName = userViewModel.LoginName;
      await this.userManager.Store(user);

      return RedirectToAction("Index");
    }
  
    public async Task<IActionResult> Delete(string id)
    {
      var user = await this.userManager.Get(id);
      var userViewModel = this.mapper.Map<UserViewModel>(user);

      return View(userViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete([Bind("Id")] UserViewModel userViewModel)
    {
      if (userViewModel == null)
        throw new ArgumentNullException(nameof(userViewModel));
      
      await this.userManager.Delete(userViewModel.Id);

      return RedirectToAction("Index");
    }
  }
}