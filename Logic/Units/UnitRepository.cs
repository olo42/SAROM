// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Olo42.SAROM.Logic.Operations
{
  public class UnitsRepository : IUnitsRepository
  {
    private readonly IOperationsRepository operationsRepository;

    public UnitsRepository(IOperationsRepository operationsRepository)
    {
      this.operationsRepository = operationsRepository;
    }

    public async Task Delete(string operationId, string unitId)
    {
      var operation = await this.GetOperationAsync(operationId);
      operation.Units.Remove(this.GetUnit(operation, unitId));

      await this.operationsRepository.Write(operation);
    }

    public async Task<IEnumerable<Unit>> Get(string operationId)
    {
      var operation = await this.GetOperationAsync(operationId);

      return operation.Units;
    }

    public async Task<Unit> Get(string operationId, string unitId)
    {
      var operation = await this.GetOperationAsync(operationId);

      return this.GetUnit(operation, unitId);
    }

    public async Task Write(string operationId, Unit unit)
    {
      var operation = await this.GetOperationAsync(operationId);
      if (unit.Id == null)
      {
        unit.Id = Guid.NewGuid().ToString();
      }

      var origin = this.GetUnit(operation, unit.Id);
      if (origin == null)
      {
        operation.Units.Add(unit);
      }
      else
      {
        this.Update(origin, unit);
      }
      
      await this.operationsRepository.Write(operation);
    }

    private void Update(Unit origin, Unit unit)
    {
      origin.Name = unit.Name;
      origin.AreaSeeker = unit.AreaSeeker;
      origin.DebrisSearcher = unit.DebrisSearcher;
      origin.GroupLeader = unit.GroupLeader;
      origin.Helpers = unit.Helpers;
      origin.Mantrailer = unit.Mantrailer;
      origin.PagerNumber = unit.PagerNumber;
      origin.WaterLocators = unit.WaterLocators;
    }

    private async Task<Operation> GetOperationAsync(string operationId)
    {
      return await this.operationsRepository.Get(operationId);
    }

    private Unit GetUnit(Operation operation, string unitId)
    {
      if (operation.Units == null)
      {
        operation.Units = new List<Unit>();

        return null;
      }

      return operation.Units?.Find(x => x.Id == unitId);
    }
  }
}