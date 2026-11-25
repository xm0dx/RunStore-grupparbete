using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

// fluent api för addresses, flyttad hit från dbcontext
public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("addresses");
        builder.Property(a => a.Id).HasColumnName("id");
        builder.Property(a => a.AddressLine)
            .HasColumnName("addressline")
            .HasMaxLength(80)
            .IsUnicode(false)
            .IsRequired();
        builder.Property(a => a.PostalCodeId).HasColumnName("postalcode_id");
        builder.Property(a => a.AddressTypeId).HasColumnName("addresstype_id");

        // en adresstyp kan användas av många adresser
        builder.HasOne(a => a.AddressType)
            .WithMany(t => t.Addresses)
            .HasForeignKey(a => a.AddressTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
