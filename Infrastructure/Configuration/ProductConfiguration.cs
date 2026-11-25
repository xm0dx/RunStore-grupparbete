using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

// fluent api för products, samma namn och typer som attributen hade
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.Property(c => c.Id)
                .HasColumnName("id");

        builder.Property(c => c.Description)
                .HasColumnName("description")
                .HasMaxLength(2048)
                .IsUnicode(false);

        builder.Property(c => c.ImageUrl)
                .HasColumnName("imageurl")
                .HasMaxLength(128)
                .IsUnicode(false);

        builder.Property(c => c.ItemNumber)
                .HasColumnName("itemnumber")
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsRequired();

        // artikelnummer ska vara unikt, som i createtables.sql
        builder.HasIndex(c => c.ItemNumber).IsUnique();

        // HasMaxLength + IsUnicode(false) ger varchar(80)
        builder.Property(c => c.Name)
                .HasColumnName("productname")
                .HasMaxLength(80)
                .IsRequired()
                .IsUnicode(false);

        // precision 18,2 = max 18 siffror varav 2 decimaler
        builder.Property(c => c.Price)
                .HasColumnName("price")
                .HasPrecision(18, 2)
                .IsRequired();

        builder.Property(c => c.SupplierId)
                .HasColumnName("supplier_id");

        // check constraint, databasen säger nej till negativa priser
        builder.ToTable(c => c.HasCheckConstraint(
            "CK_Product_Price_NonNegative",
            "price >= 0"
        ));
    }
}
