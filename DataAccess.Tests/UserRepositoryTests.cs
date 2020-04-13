// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Olo42.FileDataAccess.Contracts;
using Olo42.SAROM.DataAccess.Contracts;

namespace Olo42.SAROM.DataAccess.Tests
{
  [TestFixture]
  public class UserRepositoryTests
  {
    Mock<IFileDataAccess<IEnumerable<User>>> dataAccessMock;
    private UserRepository userRepository;

    [SetUp]
    public void Setup()
    {
      Mock<IConfiguration> configuration = new Mock<IConfiguration>();
      var section = EConfigSection.SAROMSettings.ToString();
      var key = EConfigKey.UsersFile.ToString();
      configuration
        .Setup(c => c.GetSection(section)[key])
        .Returns("testuser.dat");

      this.dataAccessMock =
        new Mock<IFileDataAccess<IEnumerable<User>>>();

      this.userRepository =
        new UserRepository(dataAccessMock.Object, configuration.Object);
    }

    #region Add
    [Test]
    public void Add_Calls_Data_Access()
    {
      // Arrange
      short count = 0;
      this.dataAccessMock
        .Setup(x => x.Write(It.IsAny<string>(), It.IsAny<IEnumerable<User>>()))
        .Callback(() => count++);
      var user = new User();

      // Act
      this.userRepository.Add(user);

      // Assert
      Assert.That(count, Is.EqualTo(1));
    }

    [Test]
    public void Add_Throws_If_User_Already_Exists()
    {
      // Arrange
      var user = new User { LoginName = "Peter" };
      this.dataAccessMock
        .Setup(x => x.Read(It.IsAny<string>()))
        .Returns(new List<User> { user });

      // Act
      // Assert
      var ex =
        Assert.Throws<DuplicateUserException>(() => userRepository.Add(user));
      Assert.That(ex.Message, Is.EqualTo("User already exist!"));
    }
    #endregion

    #region Get
    [Test]
    public void GetUserDoesNotThrow()
    {
      // Arrange
      var user = new User();
      dataAccessMock
        .Setup(x => x.Read(It.IsAny<string>()))
        .Returns(new List<User> { user });

      // Act // Assert
      Assert.That(() => userRepository.Get(user.Id), Throws.Nothing);
    }

    [Test]
    public void NoUserFound()
    {
      // Arrange
      dataAccessMock
        .Setup(x => x.Read(It.IsAny<string>()))
        .Returns(new List<User> { new User() });

      // Act 
      var result = userRepository.Get(Guid.NewGuid().ToString());

      // Assert
      Assert.That(result, Is.Null);
    }

    [Test]
    public void GetAllUsers()
    {
      // Arrange
      dataAccessMock
        .Setup(x => x.Read(It.IsAny<string>()))
        .Returns(
          new List<User>
          {
            new User { LoginName = "Jonny" },
            new User { LoginName = "Susi" },
            new User { LoginName = "Peter" },
          });

      // Act 
      var result = userRepository.Get();

      // Assert
      Assert.That(result.Count(), Is.EqualTo(3));
      Assert.That(result.Select(x => x.LoginName), Does.Contain("Jonny"));
      Assert.That(result.Select(x => x.LoginName), Does.Contain("Susi"));
      Assert.That(result.Select(x => x.LoginName), Does.Contain("Peter"));
    }
    #endregion
  }
}