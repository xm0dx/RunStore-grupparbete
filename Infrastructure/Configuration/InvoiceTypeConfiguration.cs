using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

// fluent api för invoicetypes
public class InvoiceTypeConfiguration : IEntityTypeConfiguration<InvoiceType>
{
    public void Configure(EntityTypeBuilder<InvoiceType> builder)
    {
        builder.ToTable("invoicetypes");
        builder.Property(i => i.Id).HasColumnName("id");
        builder.Property(i => i.InvoiceTypeName)
            .HasColumnName("invoicetype")
            .HasMaxLength(20)
            .IsRequired()
            .IsUnicode(false);
    }
}
