using YellowPages.Application.Common.Interfaces;

namespace YellowPages.Application.People.Commands.UpdatePerson;

public record UpdatePersonCommand : IRequest
{
    public int Id { get; init; }

    public string? FullName { get; init; }
}

public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdatePersonCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.People
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.FullName = request.FullName;
        entity.Id = request.Id;

        await _context.SaveChangesAsync(cancellationToken);

    }
}
