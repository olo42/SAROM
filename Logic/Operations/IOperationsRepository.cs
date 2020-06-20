// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Olo42.SAROM.Logic.Operations
{
  public interface IOperationsRepository
  {
    Task<IEnumerable<Operation>> Get();

    Task<Operation> Get(string id);

    Task Write(Operation operation);

    Task Delete(string id);
  }
}