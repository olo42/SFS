// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Olo42.SerializingFileAccess.Abstractions
{
  public interface ISerializingFileAccess<T>
  {
    void Create(T obj);

    T Read();

    void Update(T obj);

    void Delete();
  }
}
