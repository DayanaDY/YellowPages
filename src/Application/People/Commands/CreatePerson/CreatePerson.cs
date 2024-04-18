using YellowPages.Application.Common.Interfaces;

namespace YellowPages.Application.People.Commands.CreatePerson;

public record CreatePersonCommand : IRequest<int>
{
    public string? FullName { get; init; }
}

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreatePersonCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var entity = new Person();

        entity.FullName = request.FullName;

        _context.People.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
