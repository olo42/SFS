// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Olo42.SerializingFileAccess.Abstractions;

namespace SerializingFileAccess
{
  public class FileAccess<T> : ISerializingFileAccess<T>
  {
    public void Create(T obj)
    {
      throw new NotImplementedException();
    }

    public void Delete()
    {
      throw new NotImplementedException();
    }

    public T Read()
    {
      throw new NotImplementedException();
    }

    public void Update(T obj)
    {
      throw new NotImplementedException();
    }
  }
}
