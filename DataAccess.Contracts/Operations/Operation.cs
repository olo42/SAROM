// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Olo42.SAROM.DataAccess.Contracts
{
  [Serializable]
  public class Operation
  {
    public Operation()
    {}
    public Operation(string name, string number, DateTime alertDateTime)
    {
      this.Id = Guid.NewGuid();
      this.Name = name;
      this.Number = number;
      this.AlertDateTime = alertDateTime;

    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }
    public DateTime AlertDateTime { get; set; }
    public bool IsClosed { get; set; }
    public string ClosingReport { get; set; }
  }
}