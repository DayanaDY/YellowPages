using System.Text.Json.Serialization;
using YellowPages.Application.Addresses.Queries.GetAddresses;

namespace YellowPages.Application.People.Queries.GetPeople;

public class PersonDto
{
    public PersonDto()
    {
        Addresses = Array.Empty<AddressDto>();
    }

    [JsonIgnore]
    public int Id { get; init; }

    ////[JsonPropertyName("Name")]
    public string? FullName { get; init; }

    public IReadOnlyCollection<AddressDto> Addresses { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Person, PersonDto>()
                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses));
        }
    }
}
