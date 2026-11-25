namespace Core.Entities;

// leverantör, kopplas till produkter
// tabell och kolumnnamn sätts med fluent api i dbcontext istället för attribut
public class Supplier : BaseEntity
{
    public required string Name { get; set; }

    // en leverantör kan ha många produkter
    public List<Product> Products { get; set; } = [];
    // en leverantör kan ha flera adresser, t.ex. kontor och lager
    public List<Address> Addresses { get; set; } = [];
}
