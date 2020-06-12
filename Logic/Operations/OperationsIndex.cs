// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace Olo42.SAROM.Logic.Operations
{
  [Serializable]
  public class OperationsIndex
  {
    private readonly List<OperationFile> operationFiles;

    public IEnumerable<OperationFile> OperationFiles
    {
      get { return this.operationFiles; }
    }

    public OperationsIndex()
    {
      if (this.OperationFiles == null)
        this.operationFiles = new List<OperationFile>();
    }

    public void Add(OperationFile operationFile)
    {
      this.operationFiles.Add(operationFile);
    }

    public void Remove(Guid id)
    {
      var operationFile = this.operationFiles.Find(x=>x.Id == id);
      
      this.operationFiles.Remove(operationFile);
    }
  }
}