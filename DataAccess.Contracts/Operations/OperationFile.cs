// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Olo42.SAROM.DataAccess.Contracts
{
  public class OperationFile
  {
    public string File { get; }

    public string Name { get; }

    public string Number { get; }

    public DateTime Alert { get; }

    public OperationFile(Operation operation, string file)
    {
      this.Name = operation.Name;
      this.Number = operation.Number;
      this.Alert = operation.AlertDateTime;
      this.File = file;
    }
  }
}