using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Olo42.SAROM.DataAccess.Contracts;

namespace Olo42.SAROM.WebApp.Logic
{
  public interface ISaromUserStore<TUser> : IUserStore<User>
  {
      Task<IEnumerable<TUser>> GetAllUsers();
  }
}