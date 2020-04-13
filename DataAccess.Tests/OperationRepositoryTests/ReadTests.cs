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
  public class ReadTests
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
    public void Read_All_Does_Not_Throw()
    {
      // Assert
      Assert.That(() => this.operationsRepository.Read(), Throws.Nothing);
    }
  }
}