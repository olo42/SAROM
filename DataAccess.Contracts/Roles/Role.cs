// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Olo42.SAROM.DataAccess.Contracts
{
  [Serializable]
  public class Role
  {
    public ERoleName Name { get; set; }
  }
}