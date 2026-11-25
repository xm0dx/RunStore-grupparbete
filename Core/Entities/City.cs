namespace Core.Entities;

// ort, en ort har många postnummer
public class City : BaseEntity
{
    public required string Name { get; set; }
    public List<PostalCode> PostalCodes { get; set; } = [];
}
