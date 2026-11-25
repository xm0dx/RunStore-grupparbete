namespace Core.Entities;

// typ av faktura, t.ex. vanlig faktura eller kreditfaktura
public class InvoiceType : BaseEntity
{
    public required string InvoiceTypeName { get; set; }
    public List<Invoice> Invoices { get; set; } = [];
}
