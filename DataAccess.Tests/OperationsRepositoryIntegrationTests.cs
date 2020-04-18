// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Olo42.FileDataAccess;
using Olo42.FileDataAccess.Contracts;
using Olo42.SAROM.DataAccess.Contracts;

namespace Olo42.SAROM.DataAccess.Tests.Integration
{
  [TestFixture]
  public class OperationsRepositoryIntegrationTests
  {
    private IFileDataAccess<Operation> operationDataAccess;
    private IFileDataAccess<OperationsIndex> operationIndexDataAccess;
    private Mock<IConfiguration> configurationMock;
    private OperationsRepository repository;
    private string testDir;

    [SetUp]
    public void Setup()
    {
      this.testDir = "TestFiles";
      Directory.CreateDirectory(this.testDir);

      var formatter = new BinaryFormatter();
      this.operationDataAccess = new FormatterDataAccess<Operation>(formatter);
      this.operationIndexDataAccess =
        new FormatterDataAccess<OperationsIndex>(formatter);

      this.configurationMock = new Mock<IConfiguration>();
      var section = EConfigSection.SAROMSettings.ToString();
      this.configurationMock
        .Setup(c => c.GetSection(section)[EConfigKey.StoragePath.ToString()])
        .Returns(this.testDir);
      this.configurationMock
        .Setup(c => c.GetSection(section)[EConfigKey.FileExtension.ToString()])
        .Returns(".dat");
      this.configurationMock
        .Setup(c => c.GetSection(section)[EConfigKey.IndexFile.ToString()])
        .Returns(Path.Combine(this.testDir, "TestOperationIndex.dat"));

      this.repository = new OperationsRepository(
        operationDataAccess,
        operationIndexDataAccess,
        configurationMock.Object);
    }

    [Test]
    public void Create_Add_To_Index()
    {
      // Arrange
      var operation = new Operation("Test", "1234", DateTime.Now);

      // Act
      this.repository.Create(operation);
      var index = this.repository.Read().ToList();

      // Assert
      Assert.That(index.Find(x => x.Id == operation.Id), Is.Not.Null);
    }

    [Test]
    public void Create_Creates_Operation_File()
    {
      // Arrange
      var operation = new Operation("Test", "1234", DateTime.Now);
      
      // Act
      this.repository.Create(operation);
      
      // Assert
      Assert.That(File.Exists(this.GetFullPath(operation)), Is.True);
    }

    [Test]
    public void Delete_Operaton_Removes_From_Index()
    {
      // Arrange
      var operation = new Operation("Test", "1234", DateTime.Now);
      this.repository.Create(operation);

      // Act
      this.repository.Delete(operation.Id);
      var index = this.repository.Read().ToList();

      // Assert
      Assert.That(index.Find(x => x.Id == operation.Id), Is.Null);
    }

    [Test]
    public void Delete_Operaton_Removes_Opertation_File()
    {
      // Arrange
      var operation = new Operation("Test", "1234", DateTime.Now);
      this.repository.Create(operation);
      var fullPath = this.GetFullPath(operation);

      // Act
      this.repository.Delete(operation.Id);

      // Assert
      Assert.That(File.Exists(fullPath), Is.False);
    }

    [TearDown]
    public void TearDown()
    {
      Directory.Delete(this.testDir, true);
    }

    private string GetFullPath(Operation operation)
    {
      var storagePath = this.configurationMock.Object.GetSection(
        EConfigSection.SAROMSettings.ToString())
        [EConfigKey.StoragePath.ToString()];
      var file = 
        this.repository.Read().ToList().Find(x => x.Id == operation.Id);
      
      return Path.Combine(storagePath, file.FileName);
    }
  }
}