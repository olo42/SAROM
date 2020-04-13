// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework;
using Olo42.FileDataAccess.Contracts;
using Microsoft.Extensions.Configuration;
using Olo42.SAROM.DataAccess.Contracts;
using Moq;
using System;
using System.Linq;

namespace Olo42.SAROM.DataAccess.Tests.OperationRepositoryTests
{
  [TestFixture]
  public class CreateTests
  {
    private Mock<IFileDataAccess<Operation>> operationDataAccessMock;
    private Mock<IFileDataAccess<OperationsIndex>> operationIndexDataAccessMock;
    private OperationsRepository operationsRepository;

    [SetUp]
    public void Setup()
    {
      Mock<IConfiguration> configurationMock = new Mock<IConfiguration>();
      configurationMock
        .Setup(c => c.GetSection("SAROMSettings")["OperationStoragePath"])
        .Returns("./");
      configurationMock
        .Setup(c => c.GetSection("SAROMSettings")["OperationFileExtension"])
        .Returns(".sod");
      configurationMock
        .Setup(c => c.GetSection("SAROMSettings")["OperationIndexFile"])
        .Returns("TestOperationIndex.dat");

      this.operationDataAccessMock = new Mock<IFileDataAccess<Operation>>();
      this.operationIndexDataAccessMock = 
        new Mock<IFileDataAccess<OperationsIndex>>();
        
      this.operationsRepository = new OperationsRepository(
        this.operationDataAccessMock.Object,
        this.operationIndexDataAccessMock.Object,
        configurationMock.Object);
    }

    [Test]
    public void Create_Does_Not_Throw()
    {
      // Arrange
      var operation = new Operation("TestOperation", "1234", DateTime.Now);

      // Act

      // Assert
      Assert.That(() => this.operationsRepository.Create(operation), Throws.Nothing);
    }

    [Test]
    public void Create_Calls_FileAccess_Create()
    {
      // Arrange
      var calls = 0;
      var operation = new Operation("TestOperation", "1234", DateTime.Now);
      this.operationDataAccessMock.Setup(x => x.Write(It.IsAny<string>(), operation))
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

    // Todo: Implement as integration test
    // [Test]
    // public void Create_Adds_Index_Entry()
    // {
    //   // Arrange
    //   var operation = new Operation("TestOperation", "1234", DateTime.Now);
    //   this.operationsRepository.Create(operation);

    //   // Act
    //   var index = operationsRepository.Read();
      
    //   // Assert
    //   Assert.That(index.First().Name, Is.EqualTo("TestOperation"));
    // }
  }
}