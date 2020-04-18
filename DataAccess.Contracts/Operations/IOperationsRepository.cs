// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace Olo42.SAROM.DataAccess.Contracts
{
  public interface IOperationsRepository
  {
    void Create(Operation operation);

    IEnumerable<OperationFile> Read();

    Operation Read(Guid id);

    void Update(Operation operation); 

    void Delete(Guid id);
  }
}