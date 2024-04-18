using YellowPages.Application.Common.Interfaces;

namespace YellowPages.Application.TelephoneNumbers.Commands.UpdateTelephoneNumber;

public class UpdateTelephoneNumberCommandValidator : AbstractValidator<UpdateTelephoneNumberCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTelephoneNumberCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Number)
            .NotEmpty();
    }

    public async Task<bool> BeUniqueTitle(UpdateTelephoneNumberCommand model, string number, CancellationToken cancellationToken)
    {
        return await _context.TelephoneNumbers
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.Number != number, cancellationToken);
    }
}
