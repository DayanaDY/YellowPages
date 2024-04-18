using YellowPages.Domain.Entities;

namespace YellowPages.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Person> People { get; }

    public DbSet<Address> Addresses { get; }

    public DbSet<TelephoneNumber> TelephoneNumbers { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
