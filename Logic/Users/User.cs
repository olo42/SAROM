// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace Olo42.SAROM.Logic.Users
{
  [Serializable]
  public class User
  {
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string LoginName { get; set; }
    public string Password { get; set; }
    public IEnumerable<Role> Roles { get; set; }

    public override bool Equals(object obj)
    {
      if (obj == null || GetType() != obj.GetType())
        return false;

      User otherUser = (User)obj;

      return Id.Equals(otherUser.Id);
    }

    public override int GetHashCode()
    {
      return Id.GetHashCode();
    }
  }
}