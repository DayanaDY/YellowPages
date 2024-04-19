using System.Text.Json;
using System.Text.Json.Serialization;
using YellowPages.Application.TelephoneNumbers.Queries.GetTelephoneNumbers;

namespace YellowPages.Web.Infrastructure;

public class CustomTelephoneNumberConverter : JsonConverter<TelephoneNumberDto>
{
    public override TelephoneNumberDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Implement reading logic
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, TelephoneNumberDto value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("Tel", value.Number);
        writer.WriteEndObject();
    }
}
