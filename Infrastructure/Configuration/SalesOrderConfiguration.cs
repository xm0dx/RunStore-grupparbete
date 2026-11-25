using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

// fluent api för salesorders
public class SalesOrderConfiguration : IEntityTypeConfiguration<SalesOrder>
{
    public void Configure(EntityTypeBuilder<SalesOrder> builder)
    {
        builder.ToTable("salesorders");

        builder.Property(o => o.Id)
            .HasColumnName("id");

        builder.Property(o => o.OrderNumber)
            .HasColumnName("ordernumber")
            .HasMaxLength(15)
            .IsUnicode(false)
            .IsRequired();

        builder.HasIndex(o => o.OrderNumber).IsUnique();

        // databasen sätter datumet själv om vi inte skickar med något
        builder.Property(o => o.OrderDate)
            .HasColumnName("orderdate")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(o => o.CustomerId)
            .HasColumnName("customer_id");
    }
}
