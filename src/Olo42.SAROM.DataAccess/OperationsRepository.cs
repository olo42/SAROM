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
    private readonly IFileDataAccess<Operation> fileDataAccess;
    private readonly string filePath;
    private readonly string fileExtension;

    public OperationsRepository(
      IFileDataAccess<Operation> fileDataAccess,
      IConfiguration configuration)
    {
      this.fileDataAccess = fileDataAccess;
      this.filePath =
        configuration.GetSection("SAROMSettings")["OperationStoragePath"];
      this.fileExtension =
        configuration.GetSection("SAROMSettings")["OperationFileExtension"];
    }

    public void Create(Operation operation)
    {
      if (operation == null)
        throw new ArgumentNullException(nameof(operation));

      this.fileDataAccess.Write(filePath, operation);
    }

    public void Delete(string id)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<FileInfo> Read()
    {
      var directoryInfo = new DirectoryInfo(this.filePath);
      var fileInfos = this.fileDataAccess.GetFiles(directoryInfo);
      foreach (var fileInfo in fileInfos)
      {
        if (Path.GetExtension(fileInfo.Name) == this.fileExtension)
          yield return fileInfo;
      }
    }

    public User Read(string id)
    {
      throw new System.NotImplementedException();
    }

    public void Update(Operation operation)
    {
      throw new System.NotImplementedException();
    }
  }
}