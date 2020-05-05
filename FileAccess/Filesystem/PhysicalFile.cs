// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using System.Threading.Tasks;
using Olo42.SFS.FileAccess.Abstractions;

namespace Olo42.SFS.FileAccess.Filesystem
{
  public class PhysicalFile : IFileAccess
  {
    public Task Delete(string path)
    {
      File.Delete(path);

      return Task.CompletedTask; 
    }

    public Task<string> Read(string path)
    {
      return File.ReadAllTextAsync(path);
    }

    public Task Write(string path, string content)
    {
      this.CreateDirectoryIfNotExists(path);

      return File.WriteAllTextAsync(path, content);
    }

    private void CreateDirectoryIfNotExists(string path)
    {
      var dir = Path.GetDirectoryName(path);
      if (!Directory.Exists(dir))
      {
        Directory.CreateDirectory(dir);
      }
    }
  }
}