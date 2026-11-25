using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

// fluent api för postalcodes
public class PostalCodeConfiguration : IEntityTypeConfiguration<PostalCode>
{
    public void Configure(EntityTypeBuilder<PostalCode> builder)
    {
        builder.ToTable("postalcodes");

        builder.Property(p => p.Id).HasColumnName("id");

        // svenska postnummer är fem siffror
        builder.Property(p => p.Code)
            .HasColumnName("postalcode")
            .HasMaxLength(5)
            .IsRequired()
            .IsUnicode(false);

        builder.Property(p => p.CityId).HasColumnName("city_id");

        builder.HasIndex(p => p.Code).IsUnique();

        // ett postnummer har många adresser
        // restrict = man får inte ta bort ett postnummer som används
        builder.HasMany(p => p.Addresses)
            .WithOne(a => a.PostalCode)
            .HasForeignKey(a => a.PostalCodeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
