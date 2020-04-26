// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Olo42.SerializingFileStorage.Abstractions
{
  public interface ISerialisationProvider<T>
  {
    T Deserialize(string serializedObject);
    string Serialize(T obj);
  }
}