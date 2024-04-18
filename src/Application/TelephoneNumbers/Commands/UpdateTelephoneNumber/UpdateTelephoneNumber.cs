using YellowPages.Application.Common.Interfaces;

namespace YellowPages.Application.TelephoneNumbers.Commands.UpdateTelephoneNumber;

public record UpdateTelephoneNumberCommand : IRequest
{
    public int Id { get; init; }

    public string? Number { get; init; }
}

public class UpdateTelephoneNumberCommandHandler : IRequestHandler<UpdateTelephoneNumberCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTelephoneNumberCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateTelephoneNumberCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TelephoneNumbers
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Number = request.Number;

        await _context.SaveChangesAsync(cancellationToken);

    }
}
