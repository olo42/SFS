// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json;
using System.Threading.Tasks;
using Olo42.SerializingFileStorage.SerialisationProvider.Abstractions;

namespace Olo42.SerializingFileStorage.SerialisationProvider.Json
{
  public class JsonSerializer<T> : ISerialisationProvider<T>
  {
    public Task<T> Deserialize(string serializedObject) 
      => Task.FromResult(JsonSerializer.Deserialize<T>(serializedObject));

    public Task<string> Serialize(T obj) 
      => Task.FromResult(JsonSerializer.Serialize<T>(obj));
  }
}
