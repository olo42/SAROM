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
    public void Create_File_From_Operation()
    {
      // Arrange
      var operation = new Operation("Test", "1234", DateTime.Now);
      var operationFileName = "Operation.dat";

      // Act
      var operationFile = new OperationFile(operation, operationFileName);

      // Assert
      Assert.That(operationFile.Name, Is.EqualTo("Test"));
      Assert.That(operationFile.Number, Is.EqualTo("1234"));
      Assert.That(operationFile.Alert, Is.EqualTo(operation.AlertDateTime));
    }
  }
}