namespace Core.Entities;

// entiteten som ef core mappar mot tabellen products
// kolumnnamn och typer sätts i ProductConfiguration med fluent api
public class Product : BaseEntity
{
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public required string ItemNumber { get; set; }
    public required string Name { get; set; }
    // decimal passar bättre för pengar än int
    public decimal Price { get; set; }

    // varje produkt har en leverantör
    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
}
