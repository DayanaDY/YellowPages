using YellowPages.Application.Common.Interfaces;

namespace YellowPages.Application.People.Commands.DeletePerson;

public record DeletePersonCommand(int Id) : IRequest;

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand>
{
    private readonly IApplicationDbContext _context;

    public DeletePersonCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.People
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.People.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
