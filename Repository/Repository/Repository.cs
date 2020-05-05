// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using Olo42.SFS.Crypto.Abstractions;
using Olo42.SFS.FileAccess.Abstractions;
using Olo42.SFS.Repository.Abstractions;
using Olo42.SFS.Serialisation.Abstractions;

namespace Olo42.SFS.Repository
{
  public class Repository<T> : IRepository<T>
  {
    private readonly ISerialisalizer<T> serializer;
    private readonly IFileAccess fileAccess;
    private readonly ICrypto crypto;

    public Repository(
      ISerialisalizer<T> serializer,
      IFileAccess fileAccess,
      ICrypto crypto = null)
    {
      this.serializer =
        serializer ??
        throw new ArgumentNullException(nameof(serializer));

      this.fileAccess =
        fileAccess ?? 
        throw new ArgumentNullException(nameof(fileAccess));

      this.crypto = crypto;
    }

    public bool IsCryptoEnabled => this.crypto != null;

    public async Task<T> Read(Uri uri)
    {
      var fileContent = await this.fileAccess.Read(uri.AbsolutePath);
      if (this.IsCryptoEnabled)
      {
        fileContent = await this.crypto.Decrypt(fileContent);
      }

      return await this.serializer.Deserialize(fileContent);
    }

    public async Task Write(Uri uri, T obj)
    {
      var objString = await this.serializer.Serialize(obj);
      if (this.IsCryptoEnabled)
      {
        objString = await this.crypto.Encrypt(objString);
      }

      await this.fileAccess.Write(uri.AbsolutePath, objString);
    }

    public Task Delete(Uri uri)
    {
      return this.fileAccess.Delete(uri.AbsolutePath);
    }
  }
}
