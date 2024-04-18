using YellowPages.Application.Common.Interfaces;

namespace YellowPages.Application.TelephoneNumbers.Commands.CreateTelephoneNumber;

public class CreateTelephoneNumberCommandValidator : AbstractValidator<CreateTelephoneNumberCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateTelephoneNumberCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Number)
            .NotEmpty();

        RuleFor(v => v.AddressId)
           .NotEmpty();
    }
}
