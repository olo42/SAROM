// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Olo42.FileDataAccess.Contracts;
using Olo42.SAROM.DataAccess;
using Olo42.SAROM.DataAccess.Contracts;

namespace Olo42.SAROM.DataAcces.Tests.UserRepositoryTests
{
  [TestFixture]
  public class GetUser
  {
    private const string FILE_PATH = "testusers.dat";
    private Mock<IFileDataAccess<IEnumerable<User>>> fileDataAccessMock;
    private UserRepository userRepository;

    [Test]
    public void GetUserDoesNotThrow()
    {
      // Arrange
      fileDataAccessMock
        .Setup(x => x.Read(It.IsAny<string>()))
        .Returns(new List<User> { new User { LoginName = "Jonny" } });

      // Act // Assert
      Assert.That(() => userRepository.Get("Jonny"), Throws.Nothing);
    }

    [Test]
    public void NoUserFound()
    {
      // Arrange
      fileDataAccessMock
        .Setup(x => x.Read(It.IsAny<string>()))
        .Returns(new List<User> { new User { LoginName = "Jonny" } });

      // Act 
      var result = userRepository.Get("Susi");

      // Assert
      Assert.That(result, Is.Null);
    }

    [Test]
    public void GetAllUsers()
    {
      // Arrange
      fileDataAccessMock
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


    [SetUp]
    public void Setup()
    {
      Mock<IConfiguration> configuration = new Mock<IConfiguration>();
      configuration.Setup(
        c => c.GetSection("User")["FileStoragePath"]).Returns("testuser.dat");

      this.fileDataAccessMock = new Mock<IFileDataAccess<IEnumerable<User>>>();

      this.userRepository =
        new UserRepository(fileDataAccessMock.Object, configuration.Object);
    }
  }
}