using System.Text.Json;
using System.Text.Json.Serialization;
using YellowPages.Application.Addresses.Queries.GetAddresses;

namespace YellowPages.Web.Infrastructure;

public class CustomAddressConverter : JsonConverter<AddressDto>
{
    public override AddressDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Implement reading logic
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, AddressDto value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("addressType", value.AddressType.ToString());
        writer.WriteString("Address", value.Street);
        writer.WriteString("City", value.City);
        writer.WriteStartArray("telephoneNumbers");

        foreach (var tel in value.TelephoneNumbers)
        {
            var customTelephoneNumberConverter = new CustomTelephoneNumberConverter();
            customTelephoneNumberConverter.Write(writer, tel, options);
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
