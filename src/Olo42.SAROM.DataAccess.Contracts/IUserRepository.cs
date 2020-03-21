using System.Collections.Generic;

namespace Olo42.SAROM.DataAccess.Contracts
{
  public interface IUserRepository
  {
    void Add(User user);

    IEnumerable<User> Get();

    User Get(string loginName);
  }
}