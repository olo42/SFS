// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework;
using Olo42.SerializingFileStorage.SerialisationProvider.Json;

namespace SerializingFileStorage.SerialisationProvider.Json.Tests
{
  public class JsonSerializerTests
  {
    private JsonSerializer<TestObject> serializer;

    [SetUp]
    public void Setup()
    {
      this.serializer = new JsonSerializer<TestObject>();
    }

    [Test]
    public void Serializes_Object_To_String()
    {
      // Arrange
      var testObject = new TestObject
      {
        Identifer = 1234,
        Name = "test"
      };

      // Act
      var serialized = this.serializer.Serialize(testObject).Result;

      // Assert
      Assert.That(serialized, Is.EqualTo("{\"Identifer\":1234,\"Name\":\"test\"}"));
    }

    [Test]
    public void Derializes_String_To_Object()
    {
      // Arrange
      var serialized = "{\"Identifer\":1234,\"Name\":\"test\"}";

      // Act
      var obj = this.serializer.Deserialize(serialized).Result;

      // Assert
      Assert.That(obj, Is.InstanceOf<TestObject>());
      Assert.That(obj.Identifer, Is.EqualTo(1234));
      Assert.That(obj.Name, Is.EqualTo("test"));
    }
  }

  public class TestObject
  {
    public int Identifer { get; set; }
    public string Name { get; set; }
  }
}