// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Olo42.SerializingFileStorage.Abstractions;

namespace Olo42.SerializingFileStorage
{
  public class Storage<T> : ISerializingFileStorage<T>
  {
    public void Create(T obj)
    {
      throw new NotImplementedException();
    }

    public void Delete(string id)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<T> Read()
    {
      throw new NotImplementedException();
    }

    public T Read(string id)
    {
      throw new NotImplementedException();
    }

    public void Update(T obj)
    {
      throw new NotImplementedException();
    }
  }
}
