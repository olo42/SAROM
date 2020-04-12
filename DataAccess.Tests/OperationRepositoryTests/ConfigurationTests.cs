// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework;
using Olo42.FileDataAccess.Contracts;
using Microsoft.Extensions.Configuration;
using Olo42.SAROM.DataAccess.Contracts;
using Moq;
using System.IO;
using System.Linq;

namespace Olo42.SAROM.DataAccess.Tests.OperationRepositoryTests
{
  [TestFixture]
  public class ConfigurationTest
  {
    private Mock<IFileDataAccess<Operation>> fileDataAccessMock;

    [SetUp]
    public void Setup()
    {
      this.fileDataAccessMock = new Mock<IFileDataAccess<Operation>>();
    }

    [Test]
    public void OperationRepository_Throws_If_OperationStoragePath_Is_Not_Set()
    {
      // Arrange
      Mock<IConfiguration> configuration = new Mock<IConfiguration>();
      configuration
        .Setup(c => c.GetSection("SAROMSettings")["OperationFileExtension"])
        .Returns(".sod");

      // Act // Assert
      Assert.That(
        () => new OperationsRepository(
          fileDataAccessMock.Object, configuration.Object),
        Throws.ArgumentNullException);
    }

    [Test]
    public void OperationRepository_Throws_If_OperationFileExtension_Is_Not_Set()
    {
      // Arrange
      Mock<IConfiguration> configuration = new Mock<IConfiguration>();
      configuration
        .Setup(c => c.GetSection("SAROMSettings")["OperationStoragePath"])
        .Returns("./");

      // Act // Assert
      Assert.That(
        () => new OperationsRepository(
          fileDataAccessMock.Object, configuration.Object),
        Throws.ArgumentNullException);
    }
  }
}