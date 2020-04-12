// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Olo42.SAROM.DataAccess.Contracts
{
  [Serializable]
  public class Operation
  {
    public Operation(string name, string number, DateTime alertDateTime)
    {
      Name = name;
      Number = number;
      AlertDateTime = alertDateTime;
    }

    public string Name { get; }
    public string Number { get; }
    public DateTime AlertDateTime { get; }
  }
}