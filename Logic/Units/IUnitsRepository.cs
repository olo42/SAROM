// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Olo42.SAROM.Logic.Operations
{
  public interface IUnitsRepository
  {
    Task<IEnumerable<Unit>> Get(string operationId);

    Task<Unit> Get(string operationId, string unitId);

    Task Write(string operationId, Unit unit);

    Task Delete(string operationId, string unitId);
  }
}