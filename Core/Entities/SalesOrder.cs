namespace Core.Entities;

// order, hör alltid till en kund
public class SalesOrder : BaseEntity
{
    public required string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    // raderna på ordern
    public List<OrderItems> Items { get; set; } = [];

    // fakturorna på ordern
    public List<Invoice> Invoices { get; set; } = [];
}
