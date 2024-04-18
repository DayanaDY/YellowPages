using System.Text.Json.Serialization;
using YellowPages.Application.Addresses.Queries.GetAddresses;
using YellowPages.Domain.Entities;

namespace YellowPages.Application.TelephoneNumbers.Queries.GetTelephoneNumbers;

public class TelephoneNumberDto
{
    public TelephoneNumberDto()
    {
        ////Items = Array.Empty<AddressDto>();
    }

    [JsonIgnore]
    public int Id { get; init; }


    [JsonPropertyName("Tel:")]
    public string? Number { get; init; }

    [JsonIgnore]
    public int AddressId { get; init; }

    ////public IReadOnlyCollection<AddressDto> Items { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<TelephoneNumber, TelephoneNumberDto>();
        }
    }
}
