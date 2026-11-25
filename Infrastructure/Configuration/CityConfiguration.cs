using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

// fluent api för cities
public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("cities");
        builder.Property(c => c.Id).HasColumnName("id");
        builder.Property(c => c.Name)
            .HasColumnName("city")
            .HasMaxLength(60)
            .IsUnicode(false)
            .IsRequired();
        // samma ort ska bara finnas en gång
        builder.HasIndex(c => c.Name).IsUnique();
    }
}
