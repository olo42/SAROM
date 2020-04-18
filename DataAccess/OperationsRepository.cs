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

      this.Write(operation);

      var index = this.ReadIndex();
      index.Add(new OperationFile(operation, this.GetFileName(operation)));
      this.WriteIndex(index);
    }

    public void Delete(Guid id)
    {
      var operation = this.Read(id);
      File.Delete(this.GetFullFileName(operation));

      var index = this.ReadIndex();
      index.Remove(id);
      this.WriteIndex(index);
    }

    public IEnumerable<OperationFile> Read()
    {
      var index = this.ReadIndex();

      return index.OperationFiles;
    }

    public Operation Read(Guid id)
    {
      var operationFile = this.ReadIndex(id);
      var folderPath = this.GetConfigValue(EConfigKey.StoragePath);
      var fullFileName = Path.Combine(folderPath, operationFile.FileName);

      return this.operationDataAccess.Read(fullFileName);
    }

    public void Update(Operation operation)
    {
      var file = this.ReadIndex(operation.Id);
      if (file == null)
      {
        throw new KeyNotFoundException(
          $"Operation Id: {operation.Id} not found in index!");
      }

      this.Write(operation);
    }

    private string GetFileName(Operation operation)
    {
      var extension = this.GetConfigValue(EConfigKey.FileExtension);

      return $"{operation.Id}{extension}";
    }

    private string GetFullFileName(Operation operation)
    {
      var folderPath = this.GetConfigValue(EConfigKey.StoragePath);
      var file = this.GetFileName(operation);

      return Path.Combine(folderPath, file);
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

    private void Write(Operation operation)
    {
      var fullFileName = this.GetFullFileName(operation);

      this.operationDataAccess.Write(fullFileName, operation);
    }

    private OperationsIndex ReadIndex()
    {
      var indexFile = this.GetConfigValue(EConfigKey.IndexFile);
      OperationsIndex index = null;
      try
      {
        index = this.operationIndexDataAccess.Read(indexFile);
      }
      catch (System.IO.FileNotFoundException)
      {
        index = new OperationsIndex();
      }
      if (index == null)
      {
        index = new OperationsIndex();
      }

      return index;
    }

    private OperationFile ReadIndex(Guid id)
    {
      var index = this.ReadIndex();

      return index.OperationFiles.ToList().Find(x => x.Id == id);
    }

    private void WriteIndex(OperationsIndex index)
    {
      var file = this.GetConfigValue(EConfigKey.IndexFile);
      this.operationIndexDataAccess.Write(file, index);
    }
  }
}