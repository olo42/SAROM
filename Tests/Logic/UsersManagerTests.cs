// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Olo42.SAROM.Logic.Configuration;
using Olo42.SAROM.Logic.Users;
using Olo42.SFS.Repository.Abstractions;

namespace Olo42.SAROM.Tests.Logic
{
  [TestFixture]
  public class UsersManagerTests
  {
    private Mock<IRepository<IEnumerable<User>>> repository;
    private Uri uri;
    private Mock<IConfiguration> configuration;

    [SetUp]
    public void SetUp()
    {
      this.repository = new Mock<IRepository<IEnumerable<User>>>();

      this.uri = new Uri(Path.Combine(
        Path.GetTempPath(),
        "SFSTestdir",
        "testfile.dat"));

      this.configuration = new Mock<IConfiguration>();
      var section = EConfigSection.SAROMSettings.ToString();
      var key = EConfigKey.UsersFile.ToString();

      this.configuration
        .Setup(c => c.GetSection(section)[key])
        .Returns(this.uri.AbsolutePath);
    }

    [Test]
    public void FileExists_Returns_False_If_File_Not_Exists()
    {
      // Arrange
      var nonExistingFileUri =
       new Uri(Path.Combine(Path.GetTempPath(), "nodir", "nofile.dat"));

      this.configuration
        .Setup(c => c.GetSection(It.IsAny<string>())[It.IsAny<string>()])
        .Returns(nonExistingFileUri.AbsolutePath);

      var manager =
        new UsersManager(this.repository.Object, configuration.Object);

      // Act
      var result = manager.FileExists;

      // Assert
      Assert.That(result, Is.False);
    }

    [Test]
    public void FileExists_Returns_True_If_File_Exists()
    {
      // Arrange
      var uri =
       new Uri(Path.Combine(Path.GetTempPath(), "myfile.dat"));
      File.Create(uri.AbsolutePath);

      var manager =
        new UsersManager(this.repository.Object, configuration.Object);

      // Act
      var result = manager.FileExists;

      // Assert
      Assert.That(result, Is.False);

      // CleanUp
      if (File.Exists(uri.AbsolutePath))
      {
        File.Delete(uri.AbsolutePath);
      }
    }

    [Test]
    public void Get_All_Users()
    {
      // Arrange
      IEnumerable<User> users = new List<User>
        {
          new User { LoginName = "Jonny" },
          new User { LoginName = "Susi" },
          new User { LoginName = "Peter" },
        };

      this.repository
        .Setup(r => r.Read(It.IsAny<Uri>()))
        .Returns(Task.FromResult(users));

      var manager =
        new UsersManager(this.repository.Object, configuration.Object);

      // Act
      var result = manager.Get().Result;

      // Assert
      Assert.That(result.Count(), Is.EqualTo(3));
      Assert.That(result.Select(x => x.LoginName), Does.Contain("Jonny"));
      Assert.That(result.Select(x => x.LoginName), Does.Contain("Susi"));
      Assert.That(result.Select(x => x.LoginName), Does.Contain("Peter"));
    }

    [Test]
    public void Get_User_By_Identifier()
    {
      // Arrange
      var user = new User();
      var users = new List<User>();
      users.Add(user);

      this.repository
        .Setup(r => r.Read(It.IsAny<Uri>()))
        .Returns(Task.FromResult((IEnumerable<User>)users));

      var manager =
        new UsersManager(this.repository.Object, configuration.Object);

      // Act
      var result = manager.Get(user.Id).Result;

      // Assert
      Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void Get_User_Throws_UserNotFound()
    {
      // Arrange
      var user = new User();
      IEnumerable<User> users = new List<User> { user };

      this.repository
        .Setup(r => r.Read(It.IsAny<Uri>()))
        .Returns(Task.FromResult(users));

      var manager =
        new UsersManager(this.repository.Object, configuration.Object);

      // Act // Assert
      var exception =
        Assert.ThrowsAsync<UserNotFoundException>(
          () => manager.Get("12345678"));
    }

    [Test]
    public void Get_User_Throws_UserNotFound_If_User_List_Is_Empty()
    {
      // Arrange
      IEnumerable<User> users = null;

      this.repository
        .Setup(r => r.Read(It.IsAny<Uri>()))
        .Returns(Task.FromResult(users));

      var manager =
        new UsersManager(this.repository.Object, configuration.Object);

      // Act // Assert
      var exception =
        Assert.ThrowsAsync<UserNotFoundException>(
          () => manager.Get("12345678"));
    }

    [Test]
    public void Store_User_Calls_Repositories_Write_Method()
    {
      // Arrange
      var user = new User();

      var manager =
        new UsersManager(this.repository.Object, configuration.Object);

      // Act
      manager.Store(user).Wait();

      // Assert
      this.repository.Verify(
        r => r.Write(It.IsAny<Uri>(), It.IsAny<IEnumerable<User>>()),
        Times.Once);
    }

    [Test]
    public void Store_Upates_If_User_Already_Exists()
    {
      // Arrange
      var user = new User();
      IEnumerable<User> users = new List<User> { user };

      this.repository
        .Setup(r => r.Read(It.IsAny<Uri>()))
        .Returns(Task.FromResult(users));

      var manager =
        new UsersManager(this.repository.Object, configuration.Object);

      // Act
      manager.Store(user).Wait();

      // Assert
      Assert.That((() => manager.Get(user.Id).Wait()), Throws.Nothing);
      this.repository.Verify(
        r => r.Write(It.IsAny<Uri>(), It.IsAny<IEnumerable<User>>()),
        Times.Once);
    }

    [Test]
    public void Delete()
    {
      // Arrange
      var user = new User();
      IEnumerable<User> users = new List<User> { user };

      this.repository
        .Setup(r => r.Read(It.IsAny<Uri>()))
        .Returns(Task.FromResult(users));

      var manager =
        new UsersManager(this.repository.Object, configuration.Object);

      // Act
      manager.Delete(user.Id).Wait();

      // Assert
      this.repository.Verify(
        r => r.Write(this.uri, It.IsAny<IEnumerable<User>>()), 
        Times.Once());
    }
  }
}