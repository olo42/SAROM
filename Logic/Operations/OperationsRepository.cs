// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Olo42.SAROM.Logic.Configuration;
using Olo42.SFS.Repository.Abstractions;

namespace Olo42.SAROM.Logic.Operations
{
  public class OperationsRepository : IOperationsRepository
  {
    private IRepository<Operation> fileRepository;
    private IConfiguration configuration;

    public OperationsRepository(
      IRepository<Operation> fileRepository,
      IConfiguration configuration)
    {
      this.fileRepository = fileRepository;
      this.configuration = configuration;
    }

    public Task<Operation> Get(string id)
    {
      return this.fileRepository.Read(this.GetUri(id));
    }
    public async Task<IEnumerable<Operation>> Get()
    {
      var dir = this.GetOperationsDirectory();
      var files = Directory.GetFiles(dir, $"*{this.GetFileExtension()}");
      var operations = new List<Operation>();

      for (int i = 0; i < files.Length; i++)
      {
        try
        {
          var operation = await fileRepository.Read(new Uri(files[i]));
          operations.Add(operation);
        }
        catch (System.Exception)
        {
          // Log error
          // throw;
        }
      }

      return operations.AsEnumerable();
    }

    public Task Write(Operation operation)
    {
      return this.fileRepository.Write(this.GetUri(operation), operation);
    }

    public Task Delete(string id)
    {
      throw new NotImplementedException();
    }

    private string GetOperationsDirectory()
    {
      var section = EConfigSection.SAROMSettings.ToString();
      var key = EConfigKey.StoragePath.ToString();

      return this.configuration.GetSection(section)[key];
    }

    private Uri GetUri(Operation operation)
    {
      var path =
        Path.Combine(this.GetOperationsDirectory(), operation.Id + this.GetFileExtension());

      return new Uri(path);
    }

    private Uri GetUri(string id)
    {
      var path =
        Path.Combine(this.GetOperationsDirectory(), id + this.GetFileExtension());

      return new Uri(path);
    }

    private string GetFileExtension()
    {
      var section = EConfigSection.SAROMSettings.ToString();
      var key = EConfigKey.FileExtension.ToString();

      return this.configuration.GetSection(section)[key];
    }
  }
}