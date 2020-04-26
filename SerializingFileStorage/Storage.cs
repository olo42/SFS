// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using Olo42.SerializingFileStorage.Abstractions;

namespace Olo42.SerializingFileStorage
{
  public class Storage<T> : ISerializingFileStorage<T>
  {
    private readonly ISerialisationProvider<T> serialisationProvider;
    private readonly IFileAccessProvider fileAccessProvider;
    private readonly ICryptoProvider cryptoProvider;

    public Storage(
      ISerialisationProvider<T> serialisationProvider,
      IFileAccessProvider fileAccessProvider,
      ICryptoProvider cryptoProvider = null)
    {
      this.serialisationProvider =
        serialisationProvider ??
        throw new ArgumentNullException(nameof(serialisationProvider));

      this.fileAccessProvider =
        fileAccessProvider ?? throw new ArgumentNullException(nameof(fileAccessProvider));

      this.cryptoProvider = cryptoProvider;
    }

    public async Task<T> Read()
    {
      var fileContent = await this.fileAccessProvider.Read();
      if (this.cryptoProvider != null)
      {
        fileContent = await this.cryptoProvider.Decrypt(fileContent);
      }

      return await this.serialisationProvider.Deserialize(fileContent);
    }

    public async Task Write(T obj)
    {
      var objString = await this.serialisationProvider.Serialize(obj);
      if (this.cryptoProvider != null)
      {
        objString = await this.cryptoProvider.Encrypt(objString);
      }

      await this.fileAccessProvider.Write(objString);
    }

    public Task Delete()
    {
      throw new NotImplementedException();
    }
  }
}
