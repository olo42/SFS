// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Olo42.SerializingFileStorage.Abstractions
{
  public interface ICryptoProvider
  {
    string Decrypt(string encryptedString);
    string Encrypt(string str);
  }
}