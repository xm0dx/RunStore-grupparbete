namespace Core.Entities;

// typ av adress, t.ex. faktura, leverans eller kontor
public class AddressType : BaseEntity
{
    public required string AddressTypeName { get; set; }
    public List<Address> Addresses { get; set; } = [];
}
