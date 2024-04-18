using YellowPages.Application.Common.Interfaces;

namespace YellowPages.Application.People.Commands.UpdatePerson;

public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdatePersonCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.FullName)
            .NotEmpty();
    }

    public async Task<bool> BeUniqueTitle(UpdatePersonCommand model, string fullName, CancellationToken cancellationToken)
    {
        return await _context.People
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.FullName != fullName, cancellationToken);
    }
}
