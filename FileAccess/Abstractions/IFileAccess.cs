// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace Olo42.SFS.FileAccess.Abstractions
{
  public interface IFileAccess
  {
    Task<string> GetPhysicalPath();

    Task Write(string content);

    Task<string> Read();

    Task Delete();
  }
}