using YellowPages.Application.Addresses.Queries.GetAddresses;

namespace YellowPages.Application.People.Queries.GetPeople;

public class PersonDto
{
    public PersonDto()
    {
        Items = Array.Empty<AddressDto>();
    }

    public int Id { get; init; }

    public string? FullName { get; init; }

    public IReadOnlyCollection<AddressDto> Items { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Person, PersonDto>();
        }
    }
}
