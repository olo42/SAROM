// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Olo42.FileDataAccess;
using Olo42.FileDataAccess.Contracts;
using Olo42.SAROM.DataAccess;
using Olo42.SAROM.DataAccess.Contracts;

namespace Olo42.SAROM.DataAcces.Tests.UserRepositoryTests
{
  public class AddUser
  {
    Mock<IFileDataAccess<IEnumerable<User>>> fileDataAccess;
    private UserRepository userRepository;  

    [Test]
    public void DoNot_let_me_add_two_users_with_same_login_name()
    {
      // Arrange
      var user = new User { LoginName = "Peter" };
      this.fileDataAccess
        .Setup(x => x.Read(It.IsAny<string>()))
        .Returns(new List<User> { user });

      // Act
      // Assert
      var ex =
        Assert.Throws<DuplicateUserException>(() => userRepository.Add(user));
      Assert.That(ex.Message, Is.EqualTo("User already exist!"));
    }

    [SetUp]
    public void Setup()
    {
      Mock<IConfiguration> configuration = new Mock<IConfiguration>();
      configuration.Setup(
        c => c.GetSection("User")["FileStoragePath"]).Returns("testuser.dat");

      this.fileDataAccess =
        new Mock<IFileDataAccess<IEnumerable<User>>>();

      this.userRepository =
        new UserRepository(fileDataAccess.Object, configuration.Object);
    }

    [Test]
    public void WritesWhenUserIsAdded()
    {
      // Arrange
      short count = 0;
      this.fileDataAccess
        .Setup(x => x.Write(It.IsAny<string>(), It.IsAny<IEnumerable<User>>()))
        .Callback(() => count++);
      var user = new User();

      // Act
      this.userRepository.Add(user);

      // Assert
      Assert.That(count, Is.EqualTo(1));
    }
  }
}