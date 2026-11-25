namespace Core.Entities;

// orderrad, kopplar ihop en order med en produkt
// har inget eget id, nyckeln är order + produkt tillsammans
public class OrderItems
{
    public int SalesOrderId { get; set; }
    public int ProductId { get; set; }
    public SalesOrder SalesOrder { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    // rabatt är frivillig, därför nullable
    public decimal? Discount { get; set; }
}
