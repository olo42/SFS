// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Olo42.SerializingFileStorage.Abstractions;

namespace Olo42.SerializingFileStorage.Tests
{
  public class StorageTests
  {
    #region Fields
    Mock<ISerialisationProvider<TestObject>> serialisationProvider;
    Mock<IFileProvider> fileProvider;
    Mock<ICryptoProvider> cryptoProvider;
    Storage<TestObject> storage;
    string testDirectory;
    string testFilePath;
    #endregion

    [SetUp]
    public void Setup()
    {
      this.serialisationProvider = new Mock<ISerialisationProvider<TestObject>>();
      this.fileProvider = new Mock<IFileProvider>();
      this.cryptoProvider = new Mock<ICryptoProvider>();

      this.storage = new Storage<TestObject>(
        this.serialisationProvider.Object,
        this.fileProvider.Object,
        this.cryptoProvider.Object);

      this.testDirectory = Path.Combine(Path.GetTempPath(), "./testdir");
      Directory.CreateDirectory(this.testDirectory);

      this.testFilePath = Path.Combine(this.testDirectory, "TestFile.dat");
    }

    #region Write
    [Test]
    public void Write_Calls_SerialisationProvider_Serialize()
    {
      // Arrange
      this.fileProvider
        .Setup(f => f.GetPhysicalPath())
        .Returns(Task.FromResult(this.testFilePath));
      var obj = new TestObject();

      // Act
      this.storage.Write(obj).Wait();

      // Assert
      this.serialisationProvider
        .Verify(f => f.Serialize(obj), Times.Once);
    }

    [Test]
    public void Write_Calls_FileProvider_Write()
    {
      // Arrange
      this.fileProvider
        .Setup(f => f.GetPhysicalPath())
        .Returns(Task.FromResult(this.testFilePath));
      var obj = new TestObject();

      // Act
      this.storage.Write(obj).Wait();

      // Assert
      this.fileProvider
        .Verify(fp => fp.Write(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void Write_Calls_CryptoProvider_Crypt()
    {
      // Arrange
      this.fileProvider
        .Setup(f => f.GetPhysicalPath())
        .Returns(Task.FromResult(this.testFilePath));
      var obj = new TestObject();

      // Act
      this.storage.Write(obj).Wait();

      // Assert
      this.cryptoProvider
        .Verify(cp => cp.Encrypt(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void Write_Does_Not_Call_CryptoProvider_Encrypt_If_Provider_Is_Null()
    {
      // Arrange
      this.fileProvider
        .Setup(f => f.GetPhysicalPath())
        .Returns(Task.FromResult(this.testFilePath));
      var storage = new Storage<TestObject>(
        this.serialisationProvider.Object,
        this.fileProvider.Object);

      var obj = new TestObject();

      // Act
      storage.Write(obj).Wait();

      // Assert
      this.cryptoProvider
        .Verify(cp => cp.Encrypt(It.IsAny<string>()), Times.Never);
    }
    #endregion

    [TearDown]
    public void TearDown()
    {
      if (Directory.Exists(this.testDirectory))
      {
        Directory.Delete(this.testDirectory, true);
      }
    }
  }

  public class TestObject
  {
  }
}