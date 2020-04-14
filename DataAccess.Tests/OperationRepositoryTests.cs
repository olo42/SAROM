// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
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
        .Returns("./");
      this.configurationMock
        .Setup(c => c.GetSection(section)[EConfigKey.FileExtension.ToString()])
        .Returns(".sod");
      this.configurationMock
        .Setup(c => c.GetSection(section)[EConfigKey.IndexFile.ToString()])
        .Returns("TestOperationIndex.dat");

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

      // Act

      // Assert
      Assert.That(() => this.repository.Create(operation), Throws.Nothing);
    }

    [Test]
    public void Create_Calls_FileAccess_Create()
    {
      // Arrange
      var calls = 0;
      var operation = new Operation("TestOperation", "1234", DateTime.Now);
      this.dataAccessMock.Setup(x => x.Write(It.IsAny<string>(), operation))
        .Callback(() => calls++);

      // Act
      this.repository.Create(operation);

      // Assert
      Assert.That(calls, Is.EqualTo(1));
    }

    [Test]
    public void Create_Throws_If_Operation_Is_Null()
    {
      // Assert
      Assert.That(() => this.repository.Create(null), Throws.ArgumentNullException);
    }

    [TestCase(EConfigKey.FileExtension)]
    [TestCase(EConfigKey.IndexFile)]
    [TestCase(EConfigKey.StoragePath)]
    public void Create_Throws_If_Key_Is_Not_Set_In_Configuration(EConfigKey key)
    {
      // Arrange
      var section = EConfigSection.SAROMSettings.ToString();
      this.configurationMock
        .Setup(x => x.GetSection(section)[key.ToString()])
        .Returns("");
      var operation = new Operation("Test", "1234", DateTime.Now);

      // Act // Assert
      var exception =
        Assert.Throws<Exception>(() => this.repository.Create(operation));
      Assert.That(
        exception.Message,
        Is.EqualTo($"Configuration value for key {key.ToString()} not found!")
      );
    }
    #endregion

    #region Read
    [Test]
    public void Read_All_Does_Not_Throw()
    {
      // Assert
      Assert.That(() => this.repository.Read(), Throws.Nothing);
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
      this.indexDataAccessMock.Verify(x => x.Read(It.IsAny<string>()), Times.Once);
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
        .Verify(x => x.Write(It.IsAny<string>(), operation), Times.Once);
    }
    #endregion
  }
}