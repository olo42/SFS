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
    private readonly IFileProvider fileProvider;
    private readonly ICryptoProvider cryptoProvider;

    public Storage(
      ISerialisationProvider<T> serialisationProvider,
      IFileProvider fileProvider,
      ICryptoProvider cryptoProvider = null)
    {
      this.serialisationProvider =
        serialisationProvider ??
        throw new ArgumentNullException(nameof(serialisationProvider));

      this.fileProvider =
        fileProvider ?? throw new ArgumentNullException(nameof(fileProvider));

      this.cryptoProvider = cryptoProvider;
    }

    public Task<T> Read()
    {
      return this.serialisationProvider.Deserialize("test");


    }

    public async Task Write(T obj)
    {
      var objString = await this.serialisationProvider.Serialize(obj);
      if(this.cryptoProvider != null)
      {
        objString = await this.cryptoProvider.Encrypt(objString);
      }

      await this.fileProvider.Write(objString);
    }

    public Task Delete()
    {
      throw new NotImplementedException();
    }
  }
}
