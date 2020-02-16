// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Olo42.FileDataAccess.Contracts;
using Olo42.SAROM.DataAccess.Contracts;

namespace Olo42.SAROM.DataAccess
{
  public class UserRepository
  {
    private readonly IFileDataAccess<IEnumerable<User>> fileDataAccess;
    private readonly string filePath;
    private List<User> users;

    public UserRepository(IFileDataAccess<IEnumerable<User>> fileDataAccess, string filePath)
    {
      this.fileDataAccess = fileDataAccess;
      this.filePath = filePath;
      this.users = new List<User>();
    }

    public void Add(User user)
    {
      if (this.users.Contains(user))
        throw new DuplicateUserException("User already exist!");

      this.users.Add(user);
      fileDataAccess.Write(filePath, this.users);
    }
  }
}