using YellowPages.Application.Common.Interfaces;
using YellowPages.Application.Common.Security;
using YellowPages.Domain.Constants;

namespace YellowPages.Application.TelephoneNumbers.Commands.PurgeTelephoneNumber;

[Authorize(Roles = Roles.Administrator)]
[Authorize(Policy = Policies.CanPurge)]
public record PurgeTelephoneNumberCommand : IRequest;

public class PurgeTelephoneNumberCommandHandler : IRequestHandler<PurgeTelephoneNumberCommand>
{
    private readonly IApplicationDbContext _context;

    public PurgeTelephoneNumberCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(PurgeTelephoneNumberCommand request, CancellationToken cancellationToken)
    {
        _context.TelephoneNumbers.RemoveRange(_context.TelephoneNumbers);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
