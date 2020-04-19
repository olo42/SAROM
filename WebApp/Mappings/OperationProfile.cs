// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using AutoMapper;
using Olo42.SAROM.DataAccess.Contracts;
using Olo42.SAROM.WebApp.Models;

namespace Olo42.SAROM.WebApp.Mappings
{
  public class OperationProfile : Profile
  {
    public OperationProfile()
    {
      CreateMap<OperationIndexViewModel, OperationFile>();
    }
  }
}