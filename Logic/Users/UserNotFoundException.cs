// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.Serialization;

namespace Olo42.SAROM.Logic.Users
{
  public class UserNotFoundException : Exception
  {
    public UserNotFoundException()
    {
    }

    public UserNotFoundException(string message) : base(message)
    {
    }

    public UserNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}