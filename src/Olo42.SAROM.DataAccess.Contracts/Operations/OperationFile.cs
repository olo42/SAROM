// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Olo42.SAROM.DataAccess.Contracts
{
  public class OperationFile
  {
    public string FileName
    {
      get
      {
        return $"{this.OperationName}"
          + $"_{this.OperationNumber}"
          + $"_{this.AlertDateTime.ToString("yyyy-MM-dd_HH-MM")}"
          + $"{this.FileExtension}";
      }
    }

    public string OperationName { get; }

    public string OperationNumber { get; }

    public DateTime AlertDateTime { get; }

    public string FileExtension { get; }

    public OperationFile(
      string name, string number, DateTime alert, string extension)
    {
      this.OperationName = name;
      this.OperationNumber = number;
      this.AlertDateTime = alert;
      this.FileExtension = extension;
    }
    public OperationFile(Operation operation, string fileExtension)
    {
      this.OperationName = operation.Name;
      this.OperationNumber = operation.Number;
      this.AlertDateTime = operation.AlertDateTime;
      this.FileExtension = fileExtension;
    }
  }
}