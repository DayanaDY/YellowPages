using YellowPages.Application.Common.Models;

namespace YellowPages.Application.Addresses.Queries.GetAddresses;

public class AddressVm
{
    public IReadOnlyCollection<AddressDto> Lists { get; init; } = Array.Empty<AddressDto>();
}
