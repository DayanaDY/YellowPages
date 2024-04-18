using YellowPages.Application.Common.Interfaces;

namespace YellowPages.Application.Addresses.Commands.CreateAddress;

public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateAddressCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Street)
            .NotEmpty();

        RuleFor(v => v.City)
            .NotEmpty();

        RuleFor(v => v.PersonId)
            .NotEmpty();

        RuleFor(v => v.AddressType)
            .NotEmpty();
    }
}
