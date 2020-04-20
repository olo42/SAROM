// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Olo42.FileDataAccess.Contracts;
using Olo42.SAROM.DataAccess.Contracts;

namespace Olo42.SAROM.DataAccess.Tests
{
  [TestFixture]
  public class OperationRepositoryTests
  {
    private Mock<IFileDataAccess<Operation>> dataAccessMock;
    private Mock<IFileDataAccess<OperationsIndex>> indexDataAccessMock;
    private Mock<IConfiguration> configurationMock;
    private OperationsRepository repository;

    [SetUp]
    public void Setup()
    {
      this.configurationMock = new Mock<IConfiguration>();
      var section = EConfigSection.SAROMSettings.ToString();
      this.configurationMock
        .Setup(c => c.GetSection(section)[EConfigKey.StoragePath.ToString()])
        .Returns("./testStorage");

      this.dataAccessMock = new Mock<IFileDataAccess<Operation>>();
      this.indexDataAccessMock = new Mock<IFileDataAccess<OperationsIndex>>();

      this.repository = new OperationsRepository(
        this.dataAccessMock.Object,
        this.indexDataAccessMock.Object,
        configurationMock.Object);
    }

    #region Create
    [Test]
    public void Create_Does_Not_Throw()
    {
      // Arrange
      var operation = new Operation("TestOperation", "1234", DateTime.Now);
      var operationFile = new OperationFile(
        operation,
        Path.Combine(operation.Id.ToString(),
        FileName.OPERATION));
      var index = new OperationsIndex();
      index.Add(operationFile);
      this.indexDataAccessMock
        .Setup(x => x.Read(It.IsAny<string>()))
        .Returns(index);

      // Assert
      Assert.That(() => this.repository.Create(operation), Throws.Nothing);
    }

    [Test]
    public void Create_Calls_FileAccess_Create()
    {
      // Arrange
      var operation = new Operation("TestOperation", "1234", DateTime.Now);
      var operationFile = new OperationFile(
        operation,
        Path.Combine(operation.Id.ToString(),
        FileName.OPERATION));
      var index = new OperationsIndex();
      index.Add(operationFile);
      this.indexDataAccessMock
        .Setup(x => x.Read(It.IsAny<string>()))
        .Returns(index);

      // Act
      this.repository.Create(operation);

      // Assert
      this.dataAccessMock.Verify(
        x => x.Write(It.IsAny<string>(), operation), Times.AtLeastOnce);
    }

    [Test]
    public void Create_Throws_If_Operation_Is_Null()
    {
      // Assert
      Assert.That(() => this.repository.Create(null), Throws.ArgumentNullException);
    }
    #endregion

    #region Read
    [Test]
    public void Read_All_Does_Not_Throw()
    {
      // Assert
      Assert.That(() => this.repository.Read(), Throws.Nothing);
    }

    [Test]
    public void Read_Returns_Operaton()
    {
      // Arrange
      var operation = new Operation("Test", "1234", DateTime.Now);
      this.dataAccessMock
        .Setup(x => x.Read(It.IsAny<string>())).Returns(operation);
      var index = new OperationsIndex();
      var file = new OperationFile(operation, "TestOperationIndex.dat");
      index.Add(file);
      this.indexDataAccessMock
        .Setup(x => x.Read(It.IsAny<string>())).Returns(index);

      // Act
      var result = this.repository.Read(operation.Id);

      // Assert
      Assert.That(operation, Is.Not.Null);
    }
    #endregion

    #region Update
    [Test]
    public void Update_Calls_Index_DataAccess_Read()
    {
      // Arrange
      var operation = new Operation("Test", "1234", DateTime.Now);
      var fileName = $"{operation.Id}.sod";

      var index = new OperationsIndex();
      index.Add(new OperationFile(operation, fileName));
      this.indexDataAccessMock
        .Setup(x => x.Read(It.IsAny<string>()))
        .Returns(index);

      // Act
      this.repository.Update(operation);

      // Assert
      this.indexDataAccessMock
        .Verify(x => x.Read(It.IsAny<string>()), Times.AtLeastOnce);
    }

    [Test]
    public void Update_Throws_If_Operation_Was_Not_Found_In_Index()
    {
      // Arrange
      var operation = new Operation("Test", "1234", DateTime.Now);
      var fileName = $"{operation.Id}.sod";

      var index = new OperationsIndex();
      index.Add(new OperationFile(operation, fileName));
      this.indexDataAccessMock
        .Setup(x => x.Read(It.IsAny<string>()))
        .Returns(index);

      // Act // Assert
      Assert.Throws<KeyNotFoundException>(
        () => this.repository.Update(new Operation("Test 2", "0000", DateTime.Now)));
    }

    [Test]
    public void Update_Calls_DataAccess_Write()
    {
      // Arrange
      var operation = new Operation("Test", "1234", DateTime.Now);
      var fileName = $"{operation.Id}.sod";

      var index = new OperationsIndex();
      index.Add(new OperationFile(operation, fileName));
      this.indexDataAccessMock
        .Setup(x => x.Read(It.IsAny<string>()))
        .Returns(index);

      // Act
      this.repository.Update(operation);

      // Assert
      this.dataAccessMock
        .Verify(x => x.Write(It.IsAny<string>(), operation), Times.AtLeastOnce);
    }
    #endregion
  }
}