using YellowPages.Application.Common.Interfaces;
using YellowPages.Application.Common.Security;
using YellowPages.Domain.Constants;

namespace YellowPages.Application.People.Commands.PurgePerson;

[Authorize(Roles = Roles.Administrator)]
[Authorize(Policy = Policies.CanPurge)]
public record PurgePersonCommand : IRequest;

public class PurgePersonCommandHandler : IRequestHandler<PurgePersonCommand>
{
    private readonly IApplicationDbContext _context;

    public PurgePersonCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(PurgePersonCommand request, CancellationToken cancellationToken)
    {
        _context.People.RemoveRange(_context.People);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
