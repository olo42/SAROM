// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using NUnit.Framework;
using Olo42.SAROM.DataAccess.Contracts;

namespace Olo42.SAROM.DataAccess.Contract.Tests
{
  [TestFixture]
  public class OperationFileTests
  {
    [Test]
    public void Create_File()
    {
      // Arrange
      var alert = DateTime.Now;

      // Act
      var operationFile =
        new OperationFile("Test", "1234", alert, ".sod");

      // Assert
      Assert.That(operationFile.OperationName, Is.EqualTo("Test"));
      Assert.That(operationFile.OperationNumber, Is.EqualTo("1234"));
      Assert.That(operationFile.AlertDateTime, Is.EqualTo(alert));
      Assert.That(operationFile.FileExtension, Is.EqualTo(".sod"));
    }

    [Test]
    public void Create_File_From_Operation()
    {
      // Arrange
      var operation = BuildOperation("Test", "1234", DateTime.Now);
      var fileExtension = ".sod";

      // Act
      var operationFile = new OperationFile(operation, fileExtension);

      // Assert
      Assert.That(operationFile.OperationName, Is.EqualTo("Test"));
      Assert.That(operationFile.OperationNumber, Is.EqualTo("1234"));
      Assert.That(
        operationFile.AlertDateTime, Is.EqualTo(operation.AlertDateTime));
    }

    [Test]
    public void FileName()
    {
      // Arrange
      var alert = DateTime.Now;
      var operationFile = new OperationFile("Test", "1234", alert, ".sod");
      var alertDateInFileName = alert.ToString("yyyy-MM-dd_HH-MM");

      // Act
      var result = operationFile.FileName;

      // Assert
      Assert.That(result, Is.EqualTo($"Test_1234_{alertDateInFileName}.sod"));
    }

    private static Operation BuildOperation(
      string name, string number, DateTime alert)
    {
      return new Operation(name, number, alert);
    }
  }
}