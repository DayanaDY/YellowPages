using YellowPages.Application.Common.Interfaces;

namespace YellowPages.Application.TelephoneNumbers.Commands.DeleteTelephoneNumber;

public record DeleteTelephoneNumberCommand(int Id) : IRequest;

public class DeleteTelephoneNumberCommandHandler : IRequestHandler<DeleteTelephoneNumberCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTelephoneNumberCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteTelephoneNumberCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TelephoneNumbers
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.TelephoneNumbers.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
