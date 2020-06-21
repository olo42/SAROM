// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace Olo42.SAROM.Logic.Operations
{
  [Serializable]
  public class Operation
  {
    public Operation()
    { }

    public Operation(string name, string number, DateTime alertDateTime)
    {
      this.Id = Guid.NewGuid().ToString();
      this.Name = name;
      this.Number = number;
      this.Alert = alertDateTime;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }
    public DateTime Alert { get; set; }
    public bool IsClosed { get; set; }
    public string ClosingReport { get; set; }
    public string Headquarter { get; set; }
    public string HeadquarterContact { get; set; }
    public string PoliceContact { get; set; }
    public string PoliceContactPhone { get; set; }
    public List<Unit> Units { get; set; }
    public List<OperationAction> OperationActions { get; set; }
    public string OperationLeader { get; set; }
    public EStatus Status { get; set; }
  }
}