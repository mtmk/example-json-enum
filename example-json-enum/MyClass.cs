using System.Text.Json.Serialization;

namespace example_json_enum;

public record MyClass
{
    [System.Text.Json.Serialization.JsonPropertyName("conf_policy")]
    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
    public MyEnum MyEnum { get; set; } = MyEnum.None;    
}

public enum MyEnum
{
    [System.Runtime.Serialization.EnumMember(Value = @"none")]
    None = 0,

    [System.Runtime.Serialization.EnumMember(Value = @"all")]
    All = 1,

    [System.Runtime.Serialization.EnumMember(Value = @"explicit")]
    Explicit = 2,
}

[JsonSerializable(typeof(MyClass))]
[JsonSerializable(typeof(MyEnum))]
// [JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower)]
public partial class SerContext : JsonSerializerContext
{
}