using YellowPages.Application.Common.Models;

namespace YellowPages.Application.People.Queries.GetPeople;

public class PersonVm
{
    public IReadOnlyCollection<PersonDto> Lists { get; init; } = Array.Empty<PersonDto>();
}
