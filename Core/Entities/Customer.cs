namespace Core.Entities;

// kund, en kund kan ha många ordrar
public class Customer : BaseEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public List<SalesOrder> SalesOrders { get; set; } = [];
    // en kund kan ha flera adresser
    public List<Address> Addresses { get; set; } = [];
}
