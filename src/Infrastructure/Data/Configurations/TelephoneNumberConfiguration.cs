using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YellowPages.Domain.Entities;

namespace YellowPages.Infrastructure.Data.Configurations;

public class TelephoneNumberConfiguration : IEntityTypeConfiguration<TelephoneNumber>
{
    public void Configure(EntityTypeBuilder<TelephoneNumber> builder)
    {
       builder.Property(t => t.Number)
           .IsRequired();

        // Configure relationship with Address
        builder
                .HasOne(t => t.Address)
                .WithMany(a => a.TelephoneNumbers)
                .HasForeignKey(t => t.AddressId)
                .IsRequired(); // Make AddressId required
    }
}
