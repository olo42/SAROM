// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.Serialization;

namespace Olo42.SAROM.DataAccess.Contracts
{
  public class DuplicateUserException : Exception
  {
    public DuplicateUserException()
    {
    }

    public DuplicateUserException(string message) : base(message)
    {
    }

    public DuplicateUserException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected DuplicateUserException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}