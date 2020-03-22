using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Olo42.SAROM.DataAccess.Contracts;

namespace Olo42.SAROM.WebApp.Logic
{
  public sealed class RoleStore<TRole> : IRoleStore<Role>
  {
    private List<Role> roles;

    public RoleStore()
    {
      this.roles = new List<Role>();
      this.roles.Add(new Role { Name = ERoleName.Administrator });
      this.roles.Add(new Role { Name = ERoleName.User });
    }

    public Task<IdentityResult> CreateAsync(
      Role role, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<IdentityResult> DeleteAsync(
      Role role, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<Role> FindByIdAsync(
      string roleId, CancellationToken cancellationToken)
    {
      var role = this.roles.Where(x =>
      {
        return x.Name.ToString().ToUpperInvariant() ==
          roleId.ToUpperInvariant();
      }).First();

      return Task.FromResult(role);
    }

    public Task<Role> FindByNameAsync(
      string normalizedRoleName, CancellationToken cancellationToken)
    {
      return this.FindByIdAsync(normalizedRoleName, cancellationToken);
    }

    public Task<string> GetNormalizedRoleNameAsync(
      Role role, CancellationToken cancellationToken)
    {
      if (role == null)
        return null;

      return Task.FromResult(role.Name.ToString().ToUpperInvariant());
    }

    public Task<string> GetRoleIdAsync(
      Role role, CancellationToken cancellationToken)
    {
      if (role == null)
        return null;

      return Task.FromResult(role.Name.ToString());
    }

    public Task<string> GetRoleNameAsync(
      Role role, CancellationToken cancellationToken)
    {
      return this.GetRoleIdAsync(role, cancellationToken);
    }

    public Task SetNormalizedRoleNameAsync(
      Role role, string normalizedName, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task SetRoleNameAsync(
      Role role, string roleName, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<IdentityResult> UpdateAsync(
      Role role, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    #region IDisposable Support

    private bool disposedValue = false; // To detect redundant calls

    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
      // TODO: uncomment the following line if the finalizer is overridden above.
      // GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
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

    #endregion IDisposable Support
  }
}