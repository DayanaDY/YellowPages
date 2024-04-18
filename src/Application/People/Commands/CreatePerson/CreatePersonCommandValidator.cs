using YellowPages.Application.Common.Interfaces;

namespace YellowPages.Application.People.Commands.CreatePerson;

public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    private readonly IApplicationDbContext _context;

    public CreatePersonCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.FullName)
            .NotEmpty();
    }
}
