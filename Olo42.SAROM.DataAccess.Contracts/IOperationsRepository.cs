// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.IO;

namespace Olo42.SAROM.DataAccess.Contracts
{
  public interface IOperationsRepository
  {
    void Create(Operation operation);

    IEnumerable<FileInfo> Read();

    User Read(string id);

    void Update(Operation operation); 

    void Delete(string id);
  }
}