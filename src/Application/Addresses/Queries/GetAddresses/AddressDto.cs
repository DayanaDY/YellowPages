using System.Text.Json.Serialization;
using YellowPages.Application.People.Queries.GetPeople;
using YellowPages.Application.TelephoneNumbers.Queries.GetTelephoneNumbers;
using YellowPages.Domain.Entities;
using YellowPages.Domain.Enums;

namespace YellowPages.Application.Addresses.Queries.GetAddresses;

public class AddressDto
{
    [JsonIgnore]
    public int Id { get; init; }
    public string? Street { get; init; }

    public string? City { get; init; }
    public string? PostalCode { get; init; }

    public AddressType? AddressType { get; init; }

    public List<TelephoneNumberDto> TelephoneNumbers { get; init; } = new List<TelephoneNumberDto>();

    [JsonIgnore]
    public int PersonId { get; init; }

    [JsonIgnore]
    public PersonDto Person { get; init; } = new PersonDto();


    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Address, AddressDto>();
        }
    }
}
