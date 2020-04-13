// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Olo42.FileDataAccess.Contracts;
using Olo42.SAROM.DataAccess.Contracts;

namespace Olo42.SAROM.DataAccess
{
  public class OperationsRepository : IOperationsRepository
  {
    private readonly IFileDataAccess<Operation> operationDataAccess;
    private readonly IFileDataAccess<OperationsIndex> operationIndexDataAccess;
    private readonly string operationFolderPath;
    private readonly string operationFileExtension;
    private readonly string operationsIndexFile;

    public OperationsRepository(
      IFileDataAccess<Operation> operationDataAccess,
      IFileDataAccess<OperationsIndex> operationIndexDataAccess,
      IConfiguration configuration)
    {
      this.operationDataAccess = operationDataAccess;
      this.operationIndexDataAccess = operationIndexDataAccess;
      this.operationFolderPath =
        configuration.GetSection("SAROMSettings")["OperationStoragePath"];
      if(string.IsNullOrWhiteSpace(this.operationFolderPath))
        throw new ArgumentNullException(nameof(this.operationFolderPath));

      this.operationFileExtension =
        configuration.GetSection("SAROMSettings")["OperationFileExtension"];
      if(string.IsNullOrWhiteSpace(this.operationFileExtension))
        throw new ArgumentNullException(nameof(this.operationFileExtension));

      this.operationsIndexFile = 
        configuration.GetSection("SAROMSettings")["OperationIndexFile"];
      if(string.IsNullOrWhiteSpace(this.operationsIndexFile))
        throw new ArgumentNullException(nameof(this.operationsIndexFile));
    }

    public void Create(Operation operation)
    {
      if (operation == null)
        throw new ArgumentNullException(nameof(operation));
      
      var operationFileName = this.CreateOperationFileName();
      var operationFullName = 
        Path.Combine(operationFolderPath, operationFileName);
      var operationIndex = this.GetIndex();
      operationIndex.Add(new OperationFile(operation, operationFileName));

      this.operationDataAccess.Write(operationFullName, operation);
      this.operationIndexDataAccess.Write(operationsIndexFile, operationIndex);
    }

    private string CreateOperationFileName()
    {
      return $"{Guid.NewGuid()}{this.operationFileExtension}";
    }

    public void Delete(string id)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<OperationFile> Read()
    {
      var index = this.GetIndex();
      
      return index.OperationFiles;
    }

    public User Read(string id)
    {
      throw new System.NotImplementedException();
    }

    public void Update(Operation operation)
    {
      throw new System.NotImplementedException();
    }
  
    private OperationsIndex GetIndex()
    {
      var index = this.operationIndexDataAccess.Read(this.operationsIndexFile);
      if (index == null)
      {
        index = new OperationsIndex();   
      }

      return index;
    }
  }
}