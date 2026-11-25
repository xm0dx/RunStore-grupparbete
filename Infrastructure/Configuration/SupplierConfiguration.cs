using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

// fluent api för suppliers i en egen klass så dbcontext inte blir så lång
public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("suppliers");

        builder.Property(p => p.Name)
                .HasColumnName("suppliername")
                .HasMaxLength(80)
                .IsUnicode(false)
                .IsRequired();

        builder.Property(p => p.Id)
                .HasColumnName("id");

        // två leverantörer får inte ha samma namn
        builder.HasIndex(n => n.Name).IsUnique();

        // en leverantör har många produkter
        // restrict = man får inte ta bort en leverantör som har produkter
        builder.HasMany(s => s.Products)
            .WithOne(p => p.Supplier)
            .HasForeignKey(p => p.SupplierId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        // många-till-många mellan leverantör och adress, samma sätt som för kunder
        builder.HasMany(s => s.Addresses)
            .WithMany(a => a.Suppliers)
            .UsingEntity("supplieraddresses",
                r => r.HasOne(typeof(Address)).WithMany().HasForeignKey("address_id").OnDelete(DeleteBehavior.Restrict),
                l => l.HasOne(typeof(Supplier)).WithMany().HasForeignKey("supplier_id").OnDelete(DeleteBehavior.Restrict)
            );
    }
}
