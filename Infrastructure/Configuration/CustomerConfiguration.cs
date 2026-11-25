using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

// fluent api för customers, samma längder som i createtables.sql från lektion 3
public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("customers");

        builder.Property(c => c.Id)
            .HasColumnName("id");

        builder.Property(c => c.FirstName)
            .HasColumnName("firstname")
            .IsRequired()
            .HasMaxLength(20)
            .IsUnicode(false);

        builder.Property(c => c.LastName)
            .HasColumnName("lastname")
            .IsRequired()
            .HasMaxLength(40)
            .IsUnicode(false);

        builder.Property(c => c.Email)
            .HasColumnName("email")
            .IsRequired()
            .HasMaxLength(128)
            .IsUnicode(false);

        // samma mejl får bara finnas en gång
        builder.HasIndex(c => c.Email).IsUnique();

        builder.Property(c => c.Phone)
            .HasColumnName("phone")
            .HasMaxLength(15)
            .IsUnicode(false);

        // en kund har många ordrar, varje order har en kund
        builder.HasMany(c => c.SalesOrders)
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerId)
            .IsRequired();

        // många-till-många mellan kund och adress
        // ef core skapar kopplingstabellen customeraddresses åt oss
        builder.HasMany(c => c.Addresses)
            .WithMany(a => a.Customers)
            .UsingEntity("customeraddresses",
                // höger sida: en adress kan finnas på många rader i kopplingstabellen
                r => r.HasOne(typeof(Address)).WithMany().HasForeignKey("address_id").OnDelete(DeleteBehavior.Restrict),
                // vänster sida: en kund kan finnas på många rader i kopplingstabellen
                l => l.HasOne(typeof(Customer)).WithMany().HasForeignKey("customer_id").OnDelete(DeleteBehavior.Restrict)
            );
    }
}
