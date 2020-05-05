// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using NUnit.Framework;

namespace Olo42.SFS.FileAccess.Filesystem.Tests
{
  [TestFixture]
  public class PhysicalFileTests
  {
    string filePath;


    [SetUp]
    public void Setup()
    {
      this.filePath = Path.Combine(
        Path.GetTempPath(),
        "SFSTestdir",
        "testfile.dat");
    }

    #region Write
    [Test]
    public void Write_Creates_Directory_and_rFile_If_Not_Exist()
    {
      // Arrange
      PhysicalFile file = new PhysicalFile(filePath);

      // Act
      file.Write("Some text");

      // Assert
      Assert.That(File.Exists(this.filePath), Is.True);
    }

    [Test]
    public void Write_Writes_Content_To_File()
    {
      // Arrange
      PhysicalFile file = new PhysicalFile(filePath);

      // Act
      file.Write("Some text").Wait();
      var contentResult = File.ReadAllText(this.filePath);

      // Assert
      Assert.That(contentResult, Is.EqualTo("Some text"));
    }
    #endregion
    
    [TearDown]
    public void TearDown()
    {
      var dir = Path.GetDirectoryName(this.filePath);
      if(Directory.Exists(dir))
      {
        Directory.Delete(dir, true);
      }
    }
  }
}