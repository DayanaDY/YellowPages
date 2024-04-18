namespace YellowPages.Application.TelephoneNumbers.Queries.GetTelephoneNumbers;

public class TelephoneNumberVm
{
    public IReadOnlyCollection<TelephoneNumberDto> Lists { get; init; } = Array.Empty<TelephoneNumberDto>();
}
