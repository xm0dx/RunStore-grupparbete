# Grupparbete lektion 11

Uppgiften från lärarens pdf: bygg vidare på RunStore med repositories för produkter, kunder,
leverantörer och beställningar, plus nya ViewModels och Views.

Det här ska gå att visa upp:
1. lista alla leverantörer
2. lägga till en ny leverantör
3. lista alla kunder (mockdata)
4. lista alla beställningar (mockdata)
5. lägga till och uppdatera en produkt

Vi kör utan databas. Testdatan ligger i listor i minnet (`Infrastructure/Data/MockData.cs`).

## Så hänger det ihop

Knapp i MainWindow -> MainWindowViewModel.GoToX -> XViewModel -> IXRepository -> MockXRepository

För varje ny sak (kunder, beställningar) behövs:
1. ett gränssnitt i `Core/Interfaces`, kopiera `ISupplierRepository.cs`
2. ett mock-repository i `Infrastructure/Repositories`, kopiera `MockSupplierRepository.cs`
3. en viewmodel i `Client/ViewModels`, kopiera `SuppliersViewModel.cs`
4. en vy i `Client/Views`, kopiera `SuppliersView.axaml` och `.axaml.cs`
5. en rad i `PageViewsEnum`, två rader i `ServiceCollectionExtensions`, en GoTo-metod i
   `MainWindowViewModel` och en knapp i `MainWindow.axaml`. Sök på "Suppliers" så ser ni var.

Vyn måste heta samma som viewmodeln fast utan "Model": `CustomersViewModel` -> `CustomersView`.
Annars hittar ViewLocator den inte.

## Leverantörer är klara, använd dem som mall

Lägg till i mock-repositoryt:

```csharp
public Task<bool> AddAsync(Supplier supplier)
{
    supplier.Id = MockData.Suppliers.Count + 1;   // nästa id
    MockData.Suppliers.Add(supplier);
    return Task.FromResult(true);
}
```

Och i viewmodeln:

```csharp
[RelayCommand]
private async Task AddSupplier()
{
    if (string.IsNullOrWhiteSpace(NewSupplierName)) return;
    await _supplierRepo!.AddAsync(new Supplier { Name = NewSupplierName.Trim() });
    NewSupplierName = string.Empty;
    await LoadSuppliers();   // läs om listan så den nya syns
}
```

## Kunder och beställningar med mockdata

Ett mock-repository är bara en klass med en lista i sig:

```csharp
public class MockCustomerRepository : ICustomerRepository
{
    private readonly List<Customer> _customers = new List<Customer>
    {
        new Customer { Id = 1, FirstName = "Anna", LastName = "Svensson", Email = "anna@mail.com", Phone = "0701234567" },
        new Customer { Id = 2, FirstName = "Erik", LastName = "Karlsson", Email = "erik@mail.com" }
    };

    public Task<IReadOnlyList<Customer>> ListAllAsync()
    {
        IReadOnlyList<Customer> list = _customers;
        return Task.FromResult(list);
    }
}
```

Registreras i `ServiceCollectionExtensions`:

```csharp
collection.AddScoped<ICustomerRepository, MockCustomerRepository>();
```

Beställningar på samma sätt. Sätt `Customer` på varje `SalesOrder` så vyn kan visa kundens namn,
och lägg ett par `OrderItems` i `Items`.

## Lägga till och uppdatera produkt

Edit-knappen i produktlistan anropar redan `FindByIdAsync`. Kvar att göra:
1. `AddAsync` och `UpdateAsync` i `IProductRepository` och `MockProductRepository`. Update = hitta
   produkten på id i `MockData.Products` och skriv över värdena.
2. I `ProductsViewModel`: egenskaper för formuläret (ItemNumber, Name, Price, SupplierId), en bool
   `ShowForm`, och ett `Save`-kommando som kör Add om Id är 0 annars Update.
3. I `ProductsView.axaml`: en Popup till, som delete-dialogen fast med en TextBox per fält.

## Uppdelning, 8 personer i 4 par

Varje par har en egen branch. En i paret gör Core + Infrastructure, den andra ViewModel + View.

- Kunder, branch `kunder`: ICustomerRepository + MockCustomerRepository / CustomersViewModel + CustomersView
- Beställningar, branch `bestallningar`: ISalesOrderRepository + MockSalesOrderRepository / SalesOrdersViewModel + SalesOrdersView
- Produkter, branch `produkter`: AddAsync + UpdateAsync i MockProductRepository / formulär i ProductsViewModel + ProductsView
- Leverantörer + ihopkoppling, branch `leverantorer`: UpdateAsync för leverantör, menyknappar, raderna i
  PageViewsEnum och ServiceCollectionExtensions, granska pull requests, kolla att allt bygger

Bra att hålla sig till:
- bara paret "ihopkoppling" ändrar i PageViewsEnum, ServiceCollectionExtensions och MainWindowViewModel,
  de andra skriver i sin pull request vilka rader som ska in
- ingen pushar till main, pull request och någon annan godkänner
- kör `dotnet build` i Client innan ni pushar
