using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Olo42.SAROM.DataAccess.Contracts;

namespace Olo42.SAROM.WebApp.Logic
{
  public sealed class UserStore<TUser> : IUserStore<User>
  {
    private readonly IUserRepository userRepository;
    private bool isDisposed;

    public UserStore(IUserRepository userRepository)
    {
      this.userRepository = userRepository;
    }

    public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
    {
      try
      {
        this.userRepository.Add(user);
      }
      catch(DuplicateUserException)
      {
        return new Task<IdentityResult>(
          () => IdentityResult.Failed(
            new IdentityError 
            { 
              Description = $"Could not create user {user.LastName}." 
            }));
      }


      return new Task<IdentityResult>(() => IdentityResult.Success);
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
      throw new NotImplementedException();
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
  }
}
