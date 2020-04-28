// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Moq;
using NUnit.Framework;
using Olo42.SFS.Crypto.Abstractions;
using Olo42.SFS.FileAccess.Abstractions;
using Olo42.SFS.Serialisation.Abstractions;

namespace Olo42.SFS.Repository.Tests
{
  public class RepositoryTests
  {
    #region Fields
    Mock<ISerialisalizer<TestObject>> serialisalizer;
    Mock<IFileAccess> fileAccess;
    Mock<ICrypto> crypto;
    Repository<TestObject> storage;
    #endregion

    [SetUp]
    public void Setup()
    {
      this.serialisalizer = new Mock<ISerialisalizer<TestObject>>();

      this.fileAccess = new Mock<IFileAccess>();

      this.crypto = new Mock<ICrypto>();

      this.storage = new Repository<TestObject>(
        this.serialisalizer.Object,
        this.fileAccess.Object,
        this.crypto.Object);
    }

    #region Write
    [Test]
    public void Write_Calls_Serialisalizer_Serialize()
    {
      // Arrange
      var obj = new TestObject();

      // Act
      this.storage.Write(obj).Wait();

      // Assert
      this.serialisalizer
        .Verify(f => f.Serialize(obj), Times.Once);
    }

    [Test]
    public void Write_Calls_FileAccess_Write()
    {
      // Arrange
      var obj = new TestObject();

      // Act
      this.storage.Write(obj).Wait();

      // Assert
      this.fileAccess
        .Verify(fp => fp.Write(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void Write_Calls_Crypto_Enrypt()
    {
      // Arrange
      var obj = new TestObject();

      // Act
      this.storage.Write(obj).Wait();

      // Assert
      this.crypto
        .Verify(cp => cp.Encrypt(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void Write_Does_Not_Call_Encrypt_If_Crypto_Is_Null()
    {
      // Arrange
      var storage = new Repository<TestObject>(
        this.serialisalizer.Object,
        this.fileAccess.Object);

      var obj = new TestObject();

      // Act
      storage.Write(obj).Wait();

      // Assert
      this.crypto
        .Verify(cp => cp.Encrypt(It.IsAny<string>()), Times.Never);
    }
    #endregion

    #region Read
    [Test]
    public void Read_Calls_Serializer_Deserialize()
    {
      // Act
      this.storage.Read().Wait();

      // Assert
      this.serialisalizer
        .Verify(f => f.Deserialize(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void Read_Calls_FileAccess_Read()
    {
      // Act
      this.storage.Read().Wait();

      // Assert
      this.fileAccess
        .Verify(f => f.Read(), Times.Once);
    }

    [Test]
    public void Read_Calls_Crypto_Decrypt()
    {
      // Act
      this.storage.Read().Wait();

      // Assert
      this.crypto
        .Verify(cp => cp.Decrypt(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void Read_Does_Not_Call_Encrypt_If_Crypto_Is_Null()
    {
      // Arrange
      var storage = new Repository<TestObject>(
        this.serialisalizer.Object,
        this.fileAccess.Object);

      // Act
      storage.Read().Wait();

      // Assert
      this.crypto
        .Verify(cp => cp.Encrypt(It.IsAny<string>()), Times.Never);
    }
    #endregion

    #region Delete
    [Test]
    public void Delete_Calls_FileAccess_Delete()
    {
      // Act
      this.storage.Delete().Wait();

      // Assert
      this.fileAccess
        .Verify(fa => fa.Delete(), Times.Once);
    }
    #endregion
  }

  public class TestObject
  {
  }
}