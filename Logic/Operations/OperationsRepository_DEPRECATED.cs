// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Olo42.SAROM.Logic.Operations
{
  public class OperationsRepository_DEPRECATED
  {
    // private readonly IFileDataAccess<Operation> operationDataAccess;
    // private readonly IFileDataAccess<OperationsIndex> operationIndexDataAccess;
    private readonly IConfiguration configuration;
    private readonly string storagePath;

    public OperationsRepository_DEPRECATED(
      // IFileDataAccess<Operation> operationDataAccess,
      // IFileDataAccess<OperationsIndex> operationIndexDataAccess,
      IConfiguration configuration)
    {
      // this.operationDataAccess = operationDataAccess;
      // this.operationIndexDataAccess = operationIndexDataAccess;
      this.configuration = configuration;

      // this.storagePath = this.GetConfigValue(EConfigKey.StoragePath);
    }

    public void Create(Operation operation)
    {
      if (operation == null)
        throw new ArgumentNullException(nameof(operation));

      Directory.CreateDirectory(
        Path.Combine(this.storagePath, operation.Id.ToString()));

      var index = this.ReadIndex();
      // var fileName = 
      //   Path.Combine(operation.Id.ToString(), FileName.OPERATION);
      // index.Add(
      //   new OperationFile(operation, fileName));
     
      this.WriteIndex(index);
      this.Write(operation);
    }

    public void Delete(Guid id)
    {
      var operation = this.Read(id);
      File.Delete(this.GetFullFileName(operation.Id));

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
      var filePath = 
        Path.Combine(this.storagePath, operationFile.FileName);

      // return this.operationDataAccess.Read(filePath);
      throw new NotImplementedException();
    }

    public void Update(Operation operation)
    {
      var file = this.ReadIndex(operation.Id);
      if (file == null)
      {
        throw new KeyNotFoundException(
          $"Operation Id: {operation.Id} not found in index!");
      }

      file.Status = operation.Status;
      this.Write(operation);

      var index = this.ReadIndex();
      index.OperationFiles.ToList().Find(
        f => f.Id == operation.Id).Status = operation.Status;
      
      this.WriteIndex(index);
    }

    private string GetFullFileName(Guid operationId)
    {
      return Path.Combine(this.storagePath, ReadIndex(operationId).FileName);
    }

    // private string GetConfigValue(EConfigKey key)
    // {
    //   var section = EConfigSection.SAROMSettings.ToString();
    //   var keyString = key.ToString();
    //   var value = this.configuration.GetSection(section)[keyString];
    //   if (string.IsNullOrWhiteSpace(value))
    //   {
    //     throw new Exception(
    //       $"Configuration value for key {keyString} not found!");
    //   }

    //   return value;
    // }

    private void Write(Operation operation)
    {
      var fullFileName = this.GetFullFileName(operation.Id);

      // this.operationDataAccess.Write(fullFileName, operation);
    }

    private OperationsIndex ReadIndex()
    {
      // var indexFile = Path.Combine(this.storagePath, FileName.INDEX);
      OperationsIndex index = null;
      try
      {
        // index = this.operationIndexDataAccess.Read(indexFile);
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
      // var indexFile = Path.Combine(this.storagePath, FileName.INDEX);
      // this.operationIndexDataAccess.Write(indexFile, index);
    }
  }
}