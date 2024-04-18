using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YellowPages.Infrastructure.Data.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.Property(t => t.FullName)
           .IsRequired();

        builder
            .HasMany(p => p.Addresses)
            .WithOne(a => a.Person)
            .HasForeignKey(a => a.PersonId);
    }
}
