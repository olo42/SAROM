// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using AutoMapper;
using NUnit.Framework;
using Olo42.SAROM.Logic.Operations;
using Olo42.SAROM.WebApp.Models;

namespace Olo42.SAROM.Tests.WebApp
{
  [TestFixture]
  public class MappingTests
  {
    [Test]
    public void OperationIndexViewModel_Operation_Test()
    {
      var configuration = 
        new MapperConfiguration(cfg => cfg.CreateMap<Operation, OperationIndexViewModel>());

      configuration.AssertConfigurationIsValid();
    }
  }

}


