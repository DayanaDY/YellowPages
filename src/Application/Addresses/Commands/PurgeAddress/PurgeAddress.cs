using YellowPages.Application.Common.Interfaces;
using YellowPages.Application.Common.Security;
using YellowPages.Domain.Constants;

namespace YellowPages.Application.Addresses.Commands.PurgeAddress;

[Authorize(Roles = Roles.Administrator)]
[Authorize(Policy = Policies.CanPurge)]
public record PurgeAddressCommand : IRequest;

public class PurgeAddressCommandHandler : IRequestHandler<PurgeAddressCommand>
{
    private readonly IApplicationDbContext _context;

    public PurgeAddressCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(PurgeAddressCommand request, CancellationToken cancellationToken)
    {
        _context.Addresses.RemoveRange(_context.Addresses);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
