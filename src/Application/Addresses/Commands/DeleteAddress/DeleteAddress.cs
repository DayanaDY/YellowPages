using YellowPages.Application.Common.Interfaces;

namespace YellowPages.Application.Addresses.Commands.DeleteAddress;

public record DeleteAddressCommand(int Id) : IRequest;

public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteAddressCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Addresses
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Addresses.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
