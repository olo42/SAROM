using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Olo42.SAROM.DataAccess.Contracts;

namespace Olo42.SAROM.WebApp.Logic
{
  public sealed class RoleStore<TRole> : IRoleStore<Role>
  {
    public Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls

    void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
        {
          // TODO: dispose managed state (managed objects).
        }

        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        // TODO: set large fields to null.

        disposedValue = true;
      }
    }

    // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
    // ~RoleStore()
    // {
    //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
    //   Dispose(false);
    // }

    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
      // TODO: uncomment the following line if the finalizer is overridden above.
      // GC.SuppressFinalize(this);
    }
    #endregion

  }
}