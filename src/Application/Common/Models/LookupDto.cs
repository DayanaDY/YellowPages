using YellowPages.Domain.Entities;

namespace YellowPages.Application.Common.Models;

public class LookupDto
{
    public int Id { get; init; }

    public string? Title { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Person, LookupDto>();
            CreateMap<Address, LookupDto>();
            CreateMap<TelephoneNumber, LookupDto>();
        }
    }
}
