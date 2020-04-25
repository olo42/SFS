// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace Olo42.SerializingFileStorage.Abstractions
{
  public interface ISerializingFileStorage<T>
  {
    void Create(T obj);

    IEnumerable<T> Read();

    T Read(string id);

    void Update(T obj);

    void Delete(string id);
  }
}
