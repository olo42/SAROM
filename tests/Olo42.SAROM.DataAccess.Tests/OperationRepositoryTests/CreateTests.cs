// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework;
using Olo42.FileDataAccess.Contracts;
using Microsoft.Extensions.Configuration;
using Olo42.SAROM.DataAccess.Contracts;
using Moq;

namespace Olo42.SAROM.DataAccess.Tests.OperationRepositoryTests
{
  [TestFixture]
  public class CreateTests
  {
    private Mock<IFileDataAccess<Operation>> fileDataAccessMock;
    private OperationsRepository operationsRepository;

    [SetUp]
    public void Setup()
    {
      Mock<IConfiguration> configuration = new Mock<IConfiguration>();
      configuration
        .Setup(c => c.GetSection("SAROMSettings")["OperationStoragePath"])
        .Returns("testopertion.dat");
      configuration
        .Setup(c => c.GetSection("SAROMSettings")["OperationFileExtension"])
        .Returns(".sod");

      this.fileDataAccessMock = new Mock<IFileDataAccess<Operation>>();

      this.operationsRepository =
        new OperationsRepository(fileDataAccessMock.Object, configuration.Object);
    }

    [Test]
    public void Create_Does_Not_Throw()
    {
      // Arrange
      var operation = new Operation();

      // Act

      // Assert
      Assert.That(() => this.operationsRepository.Create(operation), Throws.Nothing);
    }

    [Test]
    public void Create_Calls_FileAccess_Create()
    {
      // Arrange
      var calls = 0;
      var operation = new Operation();
      this.fileDataAccessMock.Setup(x => x.Write(It.IsAny<string>(), operation))
        .Callback(() => calls++);

      // Act
      this.operationsRepository.Create(operation);

      // Assert
      Assert.That(calls, Is.EqualTo(1));
    }

    [Test]
    public void Create_Throws_If_Operation_Is_Null()
    {
      // Arrange
      Operation operation = null;

      // Act // Assert
      Assert.That(
        () => this.operationsRepository.Create(operation),
        Throws.ArgumentNullException);
    }
  }
}