using System.Text.Json.Serialization;
using YellowPages.Domain.Entities;

namespace YellowPages.Application.TelephoneNumbers.Queries.GetTelephoneNumbers;

public class TelephoneNumberDto
{
    public TelephoneNumberDto()
    {
    }

    [JsonIgnore]
    public int Id { get; init; }


    [JsonPropertyName("Tel:")]
    public string? Number { get; init; }

    [JsonIgnore]
    public int AddressId { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<TelephoneNumber, TelephoneNumberDto>();
        }
    }
}
