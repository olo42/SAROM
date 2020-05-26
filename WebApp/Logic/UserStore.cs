using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Olo42.SAROM.Logic.Users;

namespace Olo42.SAROM.WebApp.Logic
{
  public sealed class UserStore<TUser> : ISaromUserStore<User>
  {
    private readonly IUserManager userManager;
    private bool isDisposed;

    public UserStore(IUserManager userManager)
    {
      this.userManager = userManager;
    }

    public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
    {
      this.userManager.Store(user);

      return Task.FromResult(IdentityResult.Success);
    }

    public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public void Dispose()
    {
    }

    public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
      var users = this.userManager.Get().Result;
      var user = users.ToList().Find(u => u.LoginName == userId);
      if (user != null)
        return Task.FromResult(user);
      else
        throw new UserNotFoundException();
    }

    public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
    {
      return Task.FromResult(user.LoginName);
    }

    public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
    {
      return Task.FromResult($"{user.FirstName} {user.LastName}");
    }

    public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllUsers()
    {
      return this.userManager.Get();
    }
  }
}
