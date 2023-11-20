using System.Text.Json;
using System.Text.Json.Serialization;

namespace example_json_enum;

public record MyClass
{
    [System.Text.Json.Serialization.JsonPropertyName("conf_policy")]
    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter<MyEnum>))]
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

public class JsonStringEnumConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
{
    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (typeToConvert == typeof(MyEnum))
        {
            var type = reader.TokenType;
            if (type == JsonTokenType.String)
            {
                var stringValue = reader.GetString();

                if (stringValue == "explicit")
                    return (TEnum)(object)MyEnum.Explicit;
            }

            return default;
        }
            
        throw new InvalidOperationException();
    }

    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        if (value is MyEnum myEnum)
        {
            switch (myEnum)
            {
                case MyEnum.None:
                    writer.WriteStringValue("none");
                    break;
                case MyEnum.All:
                    writer.WriteStringValue("all");
                    break;
                case MyEnum.Explicit:
                    writer.WriteStringValue("explicit");
                    break;
            }
        }
        else
        {
            throw new InvalidOperationException();
        }
    }
}