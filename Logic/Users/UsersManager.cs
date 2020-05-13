// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Olo42.SAROM.Logic.Configuration;
using Olo42.SFS.Repository.Abstractions;

namespace Olo42.SAROM.Logic.Users
{
  public class UsersManager : IUserManager
  {
    private IRepository<IEnumerable<User>> repository;
    private IConfiguration configuration;
    private Uri uri;

    public UsersManager(
      IRepository<IEnumerable<User>> repository,
      IConfiguration configuration)
    {
      this.repository = repository;
      this.configuration = configuration;
      this.uri = GetUri(configuration);
    }

    private Uri GetUri(IConfiguration configuration)
    {
      var section = EConfigSection.SAROMSettings.ToString();
      var key = EConfigKey.UsersFile.ToString();

      return new Uri(configuration.GetSection(section)[key]);
    }

    public bool FileExists { get => File.Exists(this.uri.AbsolutePath); }

    public async Task<IEnumerable<User>> Get()
    {
      return await this.repository.Read(this.uri);
    }

    public async Task<User> Get(string id)
    {
      var users = await this.Get();
      var user = users?.ToList()?.Find(u => u.Id == id);  

      if (user == null)
      {
        throw new UserNotFoundException();
      }

      return user;
    }

    public async Task Store(User user)
    {
      var users = (await this.Get()).ToList();

      if (users.Contains(user))
      {
        var updateUser = users.Single(u => u.Id == user.Id);
        updateUser.LoginName = user.LoginName;
        updateUser.FirstName = user.FirstName;
        updateUser.LastName = user.LastName;
        updateUser.Password = user.Password;
        updateUser.Roles = user.Roles;
      }
      else
      {
        users.Add(user);
      }

      await this.repository.Write(this.uri, users);
    }

    public async Task Delete(string id)
    {
      var users = (await this.Get()).ToList();
      var user = await this.Get(id);

      if (user != null)
      {
        users.Remove(user);
        await this.repository.Write(this.uri, users);
      }
    }
  }
}
