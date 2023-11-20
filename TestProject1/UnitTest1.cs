using System.Text.Json;
using example_json_enum;
using Xunit.Abstractions;

namespace TestProject1;

public class UnitTest1
{
    private readonly ITestOutputHelper _output;

    public UnitTest1(ITestOutputHelper output) => _output = output;

    [Fact]
    public void Test1()
    {
        var config = new MyClass{MyEnum = MyEnum.Explicit};

        var json = JsonSerializer.Serialize(config, new SerContext(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = {  }
        }).MyClass);

        _output.WriteLine(json);
    }
}