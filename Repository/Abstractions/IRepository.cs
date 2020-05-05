// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;

namespace Olo42.SFS.Repository.Abstractions
{
  public interface IRepository<T>
  {
    Task Write(Uri path, T obj);

    Task<T> Read(Uri path);

    Task Delete(Uri path);
  }
}
