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
    private Mock<IFileDataAccess<Operation>> fileDataAccessMock;
    private OperationsRepository operationsRepository;

    [SetUp]
    public void Setup()
    {
      Mock<IConfiguration> configuration = new Mock<IConfiguration>();
      configuration
        .Setup(c => c.GetSection("SAROMSettings")["OperationStoragePath"])
        .Returns("./");
      // configuration
      //   .Setup(c => c.GetSection("SAROMSettings")["OperationFileExtension"])
      //   .Returns(".sod");

      this.fileDataAccessMock = new Mock<IFileDataAccess<Operation>>();

      this.operationsRepository =
        new OperationsRepository(fileDataAccessMock.Object, configuration.Object);
    }

    [Test]
    public void Read_All_Does_Not_Throw()
    {
      // Assert
      Assert.That(() => this.operationsRepository.Read(), Throws.Nothing);
    }

    [Test]
    public void Read_Returns_FileInfo()
    {
      //Arrange
      var fileName = "Operation.sod";
      var fileInfos = new FileInfo[1];
      fileInfos[0] = new FileInfo(fileName);

      this.fileDataAccessMock
        .Setup(x => x.GetFiles(It.IsAny<DirectoryInfo>())).Returns(fileInfos);

      // Act
      var result = this.operationsRepository.Read();

      // Assert
      Assert.That(result.First().Name, Is.Not.Null);
    }

    [Test]
    public void Read_Returns_FileInfo_For_Files_With_Appropriate_Extension()
    {
      //Arrange
      var fileWithAppropriateExtension = new FileInfo("Appropriate.sod");
      var fileWithInAppropriateExtension = new FileInfo("InAppropriate.ext");
      FileInfo[] fileInfos =
        {fileWithAppropriateExtension, fileWithInAppropriateExtension};

      this.fileDataAccessMock
        .Setup(x => x.GetFiles(It.IsAny<DirectoryInfo>())).Returns(fileInfos);

      // Act
      var result = this.operationsRepository.Read();

      // Assert
      Assert.That(result.Count, Is.EqualTo(1));
      Assert.That(result.First().Name, Is.EqualTo("Appropriate.sod"));
    }
  }
}