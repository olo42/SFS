// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using System.Threading.Tasks;
using Olo42.SFS.FileAccess.Abstractions;

namespace Olo42.SFS.FileAccess.Filesystem
{
  public class PhysicalFile : IFileAccess
  {
    private string filePath;

    public PhysicalFile(string filePath)
    {
      this.filePath = filePath;
    }

    public Task Delete()
    {
      throw new System.NotImplementedException();
    }

    public Task<string> GetPhysicalPath()
    {
      return Task.FromResult(this.filePath);
    }

    public Task<string> Read()
    {
      throw new System.NotImplementedException();
    }

    public Task Write(string content)
    {
      this.CreateDirectoryIfNotExists();

      return File.WriteAllTextAsync(this.filePath, content);
    }

    private void CreateDirectoryIfNotExists()
    {
      var dir = Path.GetDirectoryName(this.filePath);
      if (!Directory.Exists(dir))
      {
        Directory.CreateDirectory(dir);
      }
    }
  }
}