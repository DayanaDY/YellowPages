using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YellowPages.Domain.Entities;

namespace YellowPages.Infrastructure.Data.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.Property(t => t.AddressType)
           .IsRequired();
        builder.Property(t => t.Street)
           .IsRequired();
        builder.Property(t => t.City)
          .IsRequired();
        builder.Property(t => t.PostalCode)
          .IsRequired();

        builder
            .HasMany(a => a.TelephoneNumbers)
            .WithOne(t => t.Address)
            .HasForeignKey(t => t.AddressId);
    }
}
