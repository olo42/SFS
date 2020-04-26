// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Olo42.SerializingFileStorage.Abstractions
{
  public interface ITextWriter
  {
    void Write(string str);
    string Read();
  }
}