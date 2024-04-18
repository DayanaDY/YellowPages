using YellowPages.Application.Common.Interfaces;
using YellowPages.Application.Common.Security;

namespace YellowPages.Application.TelephoneNumbers.Queries.GetTelephoneNumbers;

[Authorize]
public record GetTelephoneNumbersQuery : IRequest<TelephoneNumberVm>;

public class GetTelephoneNumbersQueryHandler : IRequestHandler<GetTelephoneNumbersQuery, TelephoneNumberVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTelephoneNumbersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TelephoneNumberVm> Handle(GetTelephoneNumbersQuery request, CancellationToken cancellationToken)
    {
        return new TelephoneNumberVm
        {
            Lists = await _context.TelephoneNumbers
                .AsNoTracking()
                .ProjectTo<TelephoneNumberDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Id)
                .ToListAsync(cancellationToken)
        };
    }
}
