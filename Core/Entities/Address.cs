namespace Core.Entities;

// adress, en adress kan delas av flera kunder och leverantörer
public class Address : BaseEntity
{
    public required string AddressLine { get; set; }
    // varje adress har ett postnummer
    public int PostalCodeId { get; set; }
    public PostalCode PostalCode { get; set; } = null!;
    // vilken typ av adress det är
    public int AddressTypeId { get; set; }
    public AddressType AddressType { get; set; } = null!;
    public List<Customer> Customers { get; set; } = [];
    public List<Supplier> Suppliers { get; set; } = [];
}
