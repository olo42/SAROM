// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Olo42.SAROM.Logic.Configuration;
using Olo42.SAROM.Logic.Operations;
using Olo42.SFS.FileAccess.Abstractions;
using Olo42.SFS.FileAccess.Filesystem;
using Olo42.SFS.Repository;
using Olo42.SFS.Repository.Abstractions;
using Olo42.SFS.Serialisation.Abstractions;
using Olo42.SFS.Serialisation.Json;

namespace Tests
{
  public class OperationsRepositoryTests
  {

    private Mock<IRepository<Operation>> repositoryMock;
    private Mock<IConfiguration> configurationMock;
    private string operationsDir;
    private Repository<Operation> fileRepository;

    [SetUp]
    public void Setup()
    {
      this.repositoryMock = new Mock<IRepository<Operation>>();  
      this.configurationMock = new Mock<IConfiguration>();

      var section = EConfigSection.SAROMSettings.ToString();
      var key = EConfigKey.StoragePath.ToString();

      this.operationsDir = Path.Combine(Path.GetTempPath(), "SFSTestdir");

      this.configurationMock
        .Setup(c => c.GetSection(section)[key])
        .Returns(this.operationsDir);

      ISerialisalizer<Operation> serialisalizer = new JsonSerializer<Operation>();
      IFileAccess fileAccess = new PhysicalFile();
      fileRepository = new Repository<Operation>(serialisalizer, fileAccess);
    }

    [Test]
    public void Store_Operations()
    {
      // Arrange
      var opr1 = new Operation("Operation 1", "1", DateTime.Now);

      // Act
      

      // Assert

    }

    [Test]
    public void Read_all_operations()
    {
      // Arrange
      for (int i = 0; i < 3; i++)
      {
        var op = new Operation($"Operation {i}", $"{i}", DateTime.Now);    
        var path = Path.Combine(this.operationsDir, op.Id.ToString()+".dat");
        this.fileRepository.Write(new Uri(path), op);
      }

      var operationsRepository = new OperationsRepository(
        this.repositoryMock.Object, 
        this.configurationMock.Object);

      // Act
      var operations = operationsRepository.Get().Result;

      // Assert
      Assert.That(operations.ToList().Count(), Is.EqualTo(3));

      // Cleanup
      Directory.Delete(this.operationsDir, true);
    }
  }
}