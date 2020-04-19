// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace Olo42.SAROM.DataAccess.Contracts
{
  public class Unit
  {
    public int AreaSeeker { get; set; }

    public int DebrisSearcher { get; set; }

    public string GroupLeader { get; set; }

    public int Helpers { get; set; }

    public string Id { get; set; }

    public int Mantrailer { get; set; }

    public string Name { get; set; }

    public string OperationId { get; set; }

    public string PagerNumber { get; set; }

    public int WaterLocators { get; set; }
  }
}