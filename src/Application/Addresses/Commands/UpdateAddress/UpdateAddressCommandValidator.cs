using YellowPages.Application.Common.Interfaces;

namespace YellowPages.Application.Addresses.Commands.UpdateAddress;

public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateAddressCommandValidator(IApplicationDbContext context)
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

    public async Task<bool> BeUniqueTitle(UpdateAddressCommand model, string street, CancellationToken cancellationToken)
    {
        return await _context.Addresses
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.Street != street, cancellationToken);
    }
}
