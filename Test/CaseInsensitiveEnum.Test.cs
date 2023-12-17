using System.Text.Json;
using Codibre.CaseInsensitiveEnum;
using Newtonsoft.Json.Serialization;

namespace Test
{
    
    
    [TestClass]
    public class CaseInsensitiveEnumConverterTest
    {   
        enum TestEnum {
            a,
            b,
            c,
        }

        class TestClass {
            public TestEnum Test { get; set; }
        }
        
            private JsonSerializerOptions options = new()
            {
                Converters = { new CaseInsensitiveEnumConverter() },
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

        [TestMethod]
        public void Should_Deserialize_Enum_First_Value() {
            var converted = JsonSerializer.Deserialize<TestEnum>(@"""A""", options);

            converted.Should().Be(TestEnum.a);
        }
        [TestMethod]
        public void Should_Deserialize_Enum_Second_Value() {
            var converted = JsonSerializer.Deserialize<TestEnum>(@"""B""", options);

            converted.Should().Be(TestEnum.b);
        }
        [TestMethod]
        public void Should_Deserialize_Enum_Any_Value() {
            var converted = JsonSerializer.Deserialize<TestEnum>(@"""C""", options);

            converted.Should().Be(TestEnum.c);
        }

        [TestMethod]
        public void Should_Deserialize_Enum_Property_First_Value() {
            var expectation = new
            {
                Test = TestEnum.a,
            };
            
            var converted = JsonSerializer.Deserialize<TestClass>(JsonSerializer.Serialize(expectation), options);

            converted.Should().BeEquivalentTo(expectation);
        }

        [TestMethod]
        public void Should_Deserialize_Enum_Property_Second_Value() {
            var expectation = new
            {
                Test = TestEnum.b,
            };

            var converted = JsonSerializer.Deserialize<TestClass>(JsonSerializer.Serialize(expectation), options);

            converted.Should().BeEquivalentTo(expectation);
        }

        [TestMethod]
        public void Should_Deserialize_Enum_Property_Any_Value() {
            var expectation = new
            {
                Test = TestEnum.c,
            };

            var converted = JsonSerializer.Deserialize<TestClass>(JsonSerializer.Serialize(expectation), options);

            converted.Should().BeEquivalentTo(expectation);
        }

        [TestMethod]
        public void Should_Deserialize_String_Enum_Property_First_Value() {
            var expectation = new
            {
                Test = TestEnum.a.ToString().ToUpper(),
            };
            
            var converted = JsonSerializer.Deserialize<TestClass>(JsonSerializer.Serialize(expectation), options);

            converted.Should().BeEquivalentTo(new
            {
                Test = TestEnum.a,
            });
        }

        [TestMethod]
        public void Should_Deserialize_String_Enum_Property_Second_Value() {
            var expectation = new
            {
                Test = TestEnum.b.ToString().ToUpper(),
            };

            var converted = JsonSerializer.Deserialize<TestClass>(JsonSerializer.Serialize(expectation), options);

            converted.Should().BeEquivalentTo(new
            {
                Test = TestEnum.b,
            });
        }

        [TestMethod]
        public void Should_Deserialize_String_Enum_Property_Any_Value() {
            var expectation = new
            {
                Test = TestEnum.c.ToString().ToUpper(),
            };

            var converted = JsonSerializer.Deserialize<TestClass>(JsonSerializer.Serialize(expectation), options);

            converted.Should().BeEquivalentTo(new
            {
                Test = TestEnum.c,
            });
        }

        [TestMethod]
        public void Should_Deserialize_String_Enum_ToString_First_Value() {
            var input = new TestClass()
            {
                Test = TestEnum.a,
            };
            
            var converted = JsonSerializer.Serialize(input, options);

            converted.Should().BeEquivalentTo("{\"test\":\"a\"}");
        }

        [TestMethod]
        public void Should_Serialize_String_Enum_ToString_Second_Value() {
            var input = new TestClass()
            {
                Test = TestEnum.b,
            };
            
            var converted = JsonSerializer.Serialize(input, options);

            converted.Should().BeEquivalentTo("{\"test\":\"b\"}");
        }

        [TestMethod]
        public void Should_Deserialize_String_Enum_ToString_Any_Value() {
            var input = new TestClass()
            {
                Test = TestEnum.c,
            };
            
            var converted = JsonSerializer.Serialize(input, options);

            converted.Should().BeEquivalentTo("{\"test\":\"c\"}");
        }
    }
}
