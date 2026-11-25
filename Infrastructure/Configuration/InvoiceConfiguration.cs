using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

// fluent api för invoices
public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("invoices");
        builder.Property(i => i.Id).HasColumnName("id");
        builder.Property(i => i.InvoiceTypeId).HasColumnName("invoicetype_id");
        builder.Property(i => i.InvoiceNumber).HasColumnName("invoicenumber");
        builder.Property(i => i.OrderId).HasColumnName("order_id");
        // fakturadatum sätts av databasen, som orderdate
        builder.Property(i => i.InvoiceDate)
            .HasColumnName("invoicedate")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(i => i.DueDate).HasColumnName("duedate");

        // en order kan ha flera fakturor
        builder.HasOne(i => i.SalesOrder)
            .WithMany(o => o.Invoices)
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.InvoiceType)
            .WithMany(t => t.Invoices)
            .HasForeignKey(i => i.InvoiceTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
