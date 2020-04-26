// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace Olo42.SerializingFileStorage.Abstractions
{
  public interface ICryptoProviderAsync
  {
    Task<string> Decrypt(string str);
    Task<string> Encrypt(string str);
  }
}