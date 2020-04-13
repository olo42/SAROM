// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using NUnit.Framework;
using Olo42.SAROM.DataAccess.Contracts;

namespace Olo42.SAROM.DataAccess.Contract.Tests
{
  [TestFixture]
  public class OperationsIndexTests
  {
    [Test]
    public void Ctor_Initializes_New_Empty_List()
    {
      // Act
      var index = new OperationsIndex();

      // Assert
      Assert.That(index.OperationFiles, Is.Not.Null);
    }

    [Test]
    public void Add_Adds_OperationFile_To_OperationFiles()
    {
      // Arrange
      var index = new OperationsIndex();
      var operation = new Operation("Test", "1234", DateTime.Now);
      var operationFile = new OperationFile(operation, "fileName.test");
      
      // Act
      index.Add(operationFile);
      var result = index.OperationFiles.ToList();

      // Assert
      Assert.That(result.Count, Is.EqualTo(1));
    }
  }
}