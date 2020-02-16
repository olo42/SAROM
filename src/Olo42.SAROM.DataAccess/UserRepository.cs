// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Olo42.FileDataAccess.Contracts;
using Olo42.SAROM.DataAccess.Contracts;

namespace Olo42.SAROM.DataAccess
{
  public class UserRepository
  {
    private readonly IFileDataAccess<IEnumerable<User>> fileDataAccess;
    private readonly string filePath;

    public UserRepository(IFileDataAccess<IEnumerable<User>> fileDataAccess, string filePath)
    {
      this.fileDataAccess = fileDataAccess;
      this.filePath = filePath;
    }

    public void Add(User user)
    {
      var users = new List<User>();
      if (File.Exists(filePath))
      {
        users = this.fileDataAccess.Read(filePath).ToList();
      }

      if (users.Contains(user))
        throw new DuplicateUserException("User already exist!");

      users.Add(user);
      fileDataAccess.Write(filePath, users);
    }

    public IEnumerable<User> Get()
    {
      var users = this.fileDataAccess.Read(filePath);

      return users;
    }

    public User Get(string loginName)
    {
      var users = this.fileDataAccess.Read(filePath);

      return users.ToList().Find(x => x.LoginName == loginName);
    }
  }
}