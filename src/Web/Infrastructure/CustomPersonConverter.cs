using System.Text.Json;
using System.Text.Json.Serialization;
using YellowPages.Application.People.Queries.GetPeople;

namespace YellowPages.Web.Infrastructure;

public class CustomPersonConverter : JsonConverter<PersonDto>
{
    public override PersonDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Implement reading logic
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, PersonDto value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("Name", value.FullName);
        writer.WriteStartArray("Addresses");
        if (value.Addresses != null)
        {
            foreach (var address in value.Addresses)
            {
                var customAddressConverter = new CustomAddressConverter();
                customAddressConverter.Write(writer, address, options);
            }
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
