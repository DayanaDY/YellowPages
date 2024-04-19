using YellowPages.Application.Common.Interfaces;

namespace YellowPages.Application.Addresses.Queries.GetAddresses;

public record GetAddressesByIdQuery : IRequest<AddressVm>
{
    public int? Id { get; init; }
}

public class GetAddressesByIdQueryHandler: IRequestHandler<GetAddressesByIdQuery, AddressVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAddressesByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AddressVm> Handle(GetAddressesByIdQuery request, CancellationToken cancellationToken)
    {
        return new AddressVm
        {
            Lists = await _context.Addresses
               .AsNoTracking()
               .ProjectTo<AddressDto>(_mapper.ConfigurationProvider)
               .Where(x => x.Id == request.Id)
               .OrderBy(t => t.Id)
               .ToListAsync(cancellationToken)
        };
    }
}
