// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Extensions.Configuration;
using Olo42.FileDataAccess.Contracts;
using Olo42.SAROM.DataAccess.Contracts;

namespace Olo42.SAROM.DataAccess
{
  public class UserRepository : IUserRepository
  {
    private readonly IFileDataAccess<IEnumerable<User>> fileDataAccess;
    private readonly string filePath;

    public UserRepository(IFileDataAccess<IEnumerable<User>> fileDataAccess, IConfiguration configuration)
    {
      this.fileDataAccess = fileDataAccess;
      this.filePath = configuration.GetSection("SAROMSettings")["UserStoragePath"];
      
      if (!File.Exists(this.filePath))
        File.Create(this.filePath).Dispose();
    }

    public void Add(User user)
    {
      var users = new List<User>();
      if (File.Exists(filePath))
      {
        users = this.Get().ToList();
      }

      if (users.Contains(user))
        throw new DuplicateUserException("User already exist!");

      users.Add(user);
      fileDataAccess.Write(filePath, users);
    }

    public void Delete(string id)
    {
      if (string.IsNullOrWhiteSpace(id))
        throw new ArgumentNullException(nameof(id));

      var users = this.Get().ToList();
      var userToRemove = this.Get(id);
      users.Remove(userToRemove);

      this.fileDataAccess.Write(filePath, users);
    }

    public IEnumerable<User> Get()
    {
      try
      {
        var users = this.fileDataAccess.Read(filePath);

        return users;
      }
      catch(SerializationException ex)
      {
        // TODO: Log "ex"
        return new List<User>();
      }
    }

    public User Get(string id)
    {
      var users = this.Get();
      var user = users.ToList().Find(
        x => x.Id == id);
      
      return user; 
    }

    public void Update(User user)
    {
      if (user == null)
        throw new ArgumentNullException(nameof(user));
        
      var users = this.Get().ToList();
      var updateUser = users.First(x => x.Id == user.Id);
      
      if (updateUser == null)
        throw new UserNotFoundException($"UserId: {user.Id}");

      updateUser.LoginName = user.LoginName;
      updateUser.FirstName = user.FirstName;
      updateUser.LastName = user.LastName;
      updateUser.Password = user.Password; 

      this.fileDataAccess.Write(filePath, users);
    }
  }
}