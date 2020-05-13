// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Olo42.SAROM.Logic.Users
{
  public interface IUserManager
  {
    bool FileExists { get; }

    Task<IEnumerable<User>> Get();

    Task<User> Get(string id);

    Task Store(User user);

    Task Delete(string id);
  }
}