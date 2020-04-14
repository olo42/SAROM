// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Olo42.FileDataAccess.Contracts;
using Olo42.SAROM.DataAccess.Contracts;

namespace Olo42.SAROM.DataAccess
{
  public class OperationsRepository : IOperationsRepository
  {
    private readonly IFileDataAccess<Operation> operationDataAccess;
    private readonly IFileDataAccess<OperationsIndex> operationIndexDataAccess;
    private readonly IConfiguration configuration;

    public OperationsRepository(
      IFileDataAccess<Operation> operationDataAccess,
      IFileDataAccess<OperationsIndex> operationIndexDataAccess,
      IConfiguration configuration)
    {
      this.operationDataAccess = operationDataAccess;
      this.operationIndexDataAccess = operationIndexDataAccess;
      this.configuration = configuration;
    }

    public void Create(Operation operation)
    {
      if (operation == null)
        throw new ArgumentNullException(nameof(operation));

      var operationFileName = this.CreateFileName(operation.Id);
      var operationFullFileName = this.CreateFullFileName(operationFileName);

      this.operationDataAccess.Write(operationFullFileName, operation);

      this.SaveToIndex(operation, operationFileName);
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
      var fileName = this.GetFileNameFromIndex(operation);
      var fullFileName = this.CreateFullFileName(fileName);

      this.operationDataAccess.Write(fullFileName, operation);
    }

    private string CreateFileName(Guid id)
    {
      var fileExtension = 
        this.GetConfigValue(EConfigKey.FileExtension);

      return $"{id}{fileExtension}";
    }

    private string CreateFullFileName(string fileName)
    {
      var folderPath = this.GetConfigValue(EConfigKey.StoragePath);

      return Path.Combine(folderPath, fileName);
    }

    private string GetConfigValue(EConfigKey key)
    {
      var section = EConfigSection.SAROMSettings.ToString();
      var keyString = key.ToString();
      var value = this.configuration.GetSection(section)[keyString];
      if (string.IsNullOrWhiteSpace(value))
      {
        throw new Exception(
          $"Configuration value for key {keyString} not found!");
      }

      return value;
    }

    private OperationsIndex GetIndex()
    {
      var indexFile = this.GetConfigValue(EConfigKey.IndexFile);
      var index = this.operationIndexDataAccess.Read(indexFile);
      if (index == null)
      {
        index = new OperationsIndex();
      }

      return index;
    }

    private string GetFileNameFromIndex(Operation operation)
    {
      var index = this.GetIndex();
      var operationFiles = 
        index.OperationFiles.Where(x => x.Id == operation.Id);
      if(operationFiles.Count() == 0)
        throw new KeyNotFoundException($"Operation {operation.Name} not found!");

      return operationFiles.First().FileName;
    }

    private void SaveToIndex(Operation operation, string fileName)
    {
      var operationIndex = this.GetIndex();
      operationIndex.Add(new OperationFile(operation, fileName));

      var indexFile = this.GetConfigValue(EConfigKey.IndexFile);
      this.operationIndexDataAccess.Write(indexFile, operationIndex);
    }
  }
}