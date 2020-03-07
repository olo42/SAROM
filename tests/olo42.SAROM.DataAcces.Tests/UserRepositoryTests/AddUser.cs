// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;
using Olo42.FileDataAccess;
using Olo42.FileDataAccess.Contracts;
using Olo42.SAROM.DataAccess;
using Olo42.SAROM.DataAccess.Contracts;

namespace Olo42.SAROM.DataAcces.Tests.UserRepositoryTests
{
  public class AddUser
  {
    private const string FILE_PATH = "testusers.dat";
    private UserRepository userRepository;

    [Test]
    public void DoNot_let_me_add_two_users_with_same_login_name()
    {
      // Arrange
      var userOne = new User { LoginName = "Peter" };
      var userTwo = new User { LoginName = "Peter" };

      // Act
      userRepository.Add(userOne);

      // Assert
      var ex =
        Assert.Throws<DuplicateUserException>(() => userRepository.Add(userTwo));
      Assert.That(ex.Message, Is.EqualTo("User already exist!"));
    }

    [SetUp]
    public void Setup()
    {
      IFileDataAccess<IEnumerable<User>> fileDataAccess =
        new FormatterDataAccess<IEnumerable<User>>(new BinaryFormatter());

      this.userRepository =
        new UserRepository(fileDataAccess, FILE_PATH);
    }

    [Test]
    public void WritesWhenUserIsAdded()
    {
      // Arrange
      var user = new User();

      // Act
      this.userRepository.Add(user);

      // Assert
      Assert.That(File.Exists(FILE_PATH));
    }
  }
}