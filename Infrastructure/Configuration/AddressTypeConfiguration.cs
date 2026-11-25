using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

// fluent api för addresstypes
public class AddressTypeConfiguration : IEntityTypeConfiguration<AddressType>
{
    public void Configure(EntityTypeBuilder<AddressType> builder)
    {
        builder.ToTable("addresstypes");
        builder.Property(a => a.Id).HasColumnName("id");
        builder.Property(a => a.AddressTypeName)
            .HasColumnName("addresstype")
            .HasMaxLength(40)
            .IsRequired()
            .IsUnicode(false);
    }
}
