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
    Mock<IFileAccessProvider> fileAccessProvider;
    Mock<ICryptoProvider> cryptoProvider;
    Storage<TestObject> storage;
    #endregion

    [SetUp]
    public void Setup()
    {
      this.serialisationProvider = new Mock<ISerialisationProvider<TestObject>>();

      this.fileAccessProvider = new Mock<IFileAccessProvider>();

      this.cryptoProvider = new Mock<ICryptoProvider>();

      this.storage = new Storage<TestObject>(
        this.serialisationProvider.Object,
        this.fileAccessProvider.Object,
        this.cryptoProvider.Object);
    }

    #region Write
    [Test]
    public void Write_Calls_SerialisationProvider_Serialize()
    {
      // Arrange
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
      var obj = new TestObject();

      // Act
      this.storage.Write(obj).Wait();

      // Assert
      this.fileAccessProvider
        .Verify(fp => fp.Write(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void Write_Calls_CryptoProvider_Crypt()
    {
      // Arrange
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
      var storage = new Storage<TestObject>(
        this.serialisationProvider.Object,
        this.fileAccessProvider.Object);

      var obj = new TestObject();

      // Act
      storage.Write(obj).Wait();

      // Assert
      this.cryptoProvider
        .Verify(cp => cp.Encrypt(It.IsAny<string>()), Times.Never);
    }
    #endregion

    #region Read
    [Test]
    public void Read_Calls_SerialisationProvider_Deserialize()
    {
      // Act
      this.storage.Read().Wait();

      // Assert
      this.serialisationProvider
        .Verify(f => f.Deserialize(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void Read_Calls_FileProvider_Read()
    {
      // Act
      this.storage.Read().Wait();

      // Assert
      this.fileAccessProvider
        .Verify(f => f.Read(), Times.Once);
    }

    [Test]
    public void Read_Calls_CryploProvider_Decrypt()
    {
      // Act
      this.storage.Read().Wait();

      // Assert
      this.cryptoProvider
        .Verify(cp => cp.Decrypt(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void Read_Does_Not_Call_CryptoProvider_Encrypt_If_Provider_Is_Null()
    {
      // Arrange
      var storage = new Storage<TestObject>(
        this.serialisationProvider.Object,
        this.fileAccessProvider.Object);

      // Act
      storage.Read().Wait();

      // Assert
      this.cryptoProvider
        .Verify(cp => cp.Encrypt(It.IsAny<string>()), Times.Never);
    }
    #endregion

    #region Delete
    [Test]
    public void Delete_Calls_FileAccessProvider_Delete()
    {
      // Act
      this.storage.Delete().Wait();

      // Assert
      this.fileAccessProvider
        .Verify(fa => fa.Delete(), Times.Once);
    }
    #endregion
  }

  public class TestObject
  {
  }
}