// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
    private OperationsRepository operationsRepository;

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

      this.operationsRepository = new OperationsRepository(
        this.repositoryMock.Object, 
        this.configurationMock.Object);

      ISerialisalizer<Operation> serialisalizer = new JsonSerializer<Operation>();
      IFileAccess fileAccess = new PhysicalFile();
      fileRepository = new Repository<Operation>(serialisalizer, fileAccess);
    }

    [Test]
    public void Write_operation()
    {
      // Arrange
      var opr1 = new Operation("Operation 1", "1", DateTime.Now);
      this.repositoryMock.Setup(r => r.Write(It.IsAny<Uri>(), opr1));
      // Act
      this.operationsRepository.Write(opr1);

      // Assert
      this.repositoryMock.Verify(r => r.Write(It.IsAny<Uri>(), opr1), Times.Once);
    }

    [Test]
    public void Get_operation()
    {
      // Arrange
      var op = new Operation($"Operation 1", $"1", DateTime.Now);
      this.repositoryMock.Setup(r => r.Read(It.IsAny<Uri>())).Returns(Task.FromResult(op));
      
      // Act
      var result = this.operationsRepository.Get(op.Id.ToString()).Result;

      // Assert
      Assert.That(result, Is.EqualTo(op));
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

      // Act
      var operations = this.operationsRepository.Get().Result;

      // Assert
      Assert.That(operations.ToList().Count(), Is.EqualTo(3));

      // Cleanup
      Directory.Delete(this.operationsDir, true);
    }
  }
}