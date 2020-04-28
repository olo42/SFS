// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace Olo42.SFS.Serialisation.Abstractions
{
  public interface ISerialisalizer<T>
  {
    Task<T> Deserialize(string serializedObject);
    Task<string> Serialize(T obj);
  }
}