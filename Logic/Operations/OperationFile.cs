// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Olo42.SAROM.Logic.Operations
{
  [Serializable]
  public class OperationFile
  {
    public Guid Id { get; }

    public string FileName { get; }

    public string Extension { get; }

    public string Name { get; }

    public string Number { get; }

    public DateTime Alert { get; }

    public EStatus Status { get; set; }

    public OperationFile(Operation operation, string fileName)
    {
      this.Id = operation.Id;
      this.Name = operation.Name;
      this.Number = operation.Number;
      this.Alert = operation.AlertDateTime;
      this.FileName = fileName;
    }
  }
}