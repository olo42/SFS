// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Olo42.SFS.FileAccess.Abstractions
{
  public interface IFileAccessProviderFactory
  {
    IFileAccess CreateFileAccess(string filePath);
  }
}