using YellowPages.Application.Common.Interfaces;

namespace YellowPages.Application.Addresses.Commands.UpdateAddress;

public record UpdateAddressCommand : IRequest
{
    public int Id { get; init; }

    public string? Street { get; init; }
}

public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateAddressCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Addresses
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Street = request.Street;

        await _context.SaveChangesAsync(cancellationToken);

    }
}
