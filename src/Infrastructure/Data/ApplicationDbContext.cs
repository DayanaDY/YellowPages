using System.Reflection;
using YellowPages.Application.Common.Interfaces;
using YellowPages.Domain.Entities;
using YellowPages.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace YellowPages.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Person> People => Set<Person>();

    public DbSet<Address> Addresses => Set<Address>();

    public DbSet<TelephoneNumber> TelephoneNumbers => Set<TelephoneNumber>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
