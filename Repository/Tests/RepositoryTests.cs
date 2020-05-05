// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
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
    private Uri uri;
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

      this.uri = new Uri(Path.Combine(
        Path.GetTempPath(),
        "SFSTestdir",
        "testfile.dat"));
    }

    #region Write
    [Test]
    public void Write_Calls_Serialisalizer_Serialize()
    {
      // Arrange
      var obj = new TestObject();

      // Act
      this.storage.Write(this.uri, obj).Wait();

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
      this.storage.Write(this.uri, obj).Wait();

      // Assert
      this.fileAccess
        .Verify(
          fp => fp.Write(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void Write_Calls_Crypto_Enrypt()
    {
      // Arrange
      var obj = new TestObject();

      // Act
      this.storage.Write(this.uri, obj).Wait();

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
      storage.Write(this.uri, obj).Wait();

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
      this.storage.Read(this.uri).Wait();

      // Assert
      this.serialisalizer
        .Verify(f => f.Deserialize(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void Read_Calls_FileAccess_Read()
    {
      // Act
      this.storage.Read(this.uri).Wait();

      // Assert
      this.fileAccess
        .Verify(f => f.Read(this.uri.AbsolutePath), Times.Once);
    }

    [Test]
    public void Read_Calls_Crypto_Decrypt()
    {
      // Act
      this.storage.Read(this.uri).Wait();

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
      storage.Read(this.uri).Wait();

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
      this.storage.Delete(this.uri).Wait();

      // Assert
      this.fileAccess
        .Verify(fa => fa.Delete(this.uri.AbsolutePath), Times.Once);
    }
    #endregion
  }

  public class TestObject
  {
  }
}