# Grupparbete lektion 11, repositories

Uppgiften (från lärarens pdf): bygg vidare på RunStore. Gränssnitt + repository för produkter, kunder,
leverantörer och beställningar, och nya ViewModels + Views i Avalonia.

Ska gå att visa upp:
1. Lista alla leverantörer
2. Lägga till en ny leverantör
3. Lista alla kunder (mockdata)
4. Lista alla beställningar (mockdata)
5. Lägga till och uppdatera en produkt

## Hur allt hänger ihop

Samma mönster som produkterna från lektion 10, en gång per entitet:

```
MainWindow-knapp -> MainWindowViewModel.GoToX -> PageViewsEnum.X -> XViewModel -> IXRepository -> XRepository -> DbContext
   (axaml)              (Client)                   (Client)          (Client)        (Core)       (Infrastructure)
```

Per entitet behövs alltså fem saker:

| Nr | Fil | Projekt | Kopiera från |
|---|---|---|---|
| 1 | `Interfaces/IXRepository.cs` | Core | `IProductRepository.cs` |
| 2 | `Repositories/XRepository.cs` | Infrastructure | `ProductRepository.cs` |
| 3 | `ViewModels/XViewModel.cs` | Client | `ProductsViewModel.cs` |
| 4 | `Views/XView.axaml` + `.axaml.cs` | Client | `ProductsView.axaml` |
| 5 | rad i `PageViewsEnum`, `ServiceCollectionExtensions`, `MainWindowViewModel`, knapp i `MainWindow.axaml` | Client | leta efter "Products" |

ViewLocator hittar vyn själv: `SuppliersViewModel` -> `SuppliersView`. Namnen måste matcha.

## Leverantörer är redan gjorda som exempel

Titta på dessa filer, det är exakt mallen för resten:
- `Core/Interfaces/ISupplierRepository.cs`
- `Infrastructure/Repositories/SupplierRepository.cs`
- `Client/ViewModels/SuppliersViewModel.cs`
- `Client/Views/SuppliersView.axaml`

Lägga till funkar så här i repositoryt:

```csharp
public async Task<bool> AddAsync(Supplier supplier)
{
    context.Suppliers.Add(supplier);          // läggs i minnet
    return await context.SaveChangesAsync() > 0;   // skrivs till databasen, true om en rad sparades
}
```

och i viewmodeln:

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

## Mockdata för kunder och beställningar

Tabellerna customers och salesorders är tomma. Istället för att fylla databasen gör man ett
"låtsas-repository" som returnerar en lista hårdkodad i C#. ViewModeln märker ingen skillnad,
det är hela poängen med gränssnittet.

```csharp
// Infrastructure/Repositories/MockCustomerRepository.cs
public class MockCustomerRepository : ICustomerRepository
{
    private readonly List<Customer> _customers =
    [
        new() { Id = 1, FirstName = "Anna", LastName = "Svensson", Email = "anna@mail.com", Phone = "0701234567" },
        new() { Id = 2, FirstName = "Erik", LastName = "Karlsson", Email = "erik@mail.com" }
    ];

    public Task<IReadOnlyList<Customer>> ListAllAsync()
        => Task.FromResult<IReadOnlyList<Customer>>(_customers);
}
```

I `ServiceCollectionExtensions` registreras mocken istället för en riktig:

```csharp
collection.AddScoped<ICustomerRepository, MockCustomerRepository>();
```

När databasen har kunder byter man bara raden till `CustomerRepository`. Inget annat ändras.

För beställningar: `MockSalesOrderRepository` med några `SalesOrder` som har `Customer` satt
(så vyn kan visa kundens namn) och ett par `Items`.

## Uppdatera produkt

Produkter har redan Edit-knappen som anropar `FindByIdAsync`. Nästa steg:
1. `UpdateAsync(Product product)` i `IProductRepository` + `ProductRepository`:
   `context.Products.Update(product); return await context.SaveChangesAsync() > 0;`
2. `AddAsync(Product product)` på samma sätt som leverantörer.
3. I `ProductsViewModel`: egenskaper för formuläret (`ItemNumber`, `Name`, `Price`, `SupplierId`),
   `ShowForm` som visar/döljer, `Save`-kommando som väljer Add eller Update beroende på om `Id == 0`.
4. I vyn: ett `Popup` som i delete-dialogen, med `TextBox Classes="form"` per fält.

Leverantören väljs enklast med en `ComboBox ItemsSource="{Binding Suppliers}" SelectedItem=...`,
så produktvyn behöver både `IProductRepository` och `ISupplierRepository` i konstruktorn.

## Förslag på uppdelning i gruppen (8 personer, 4 par)

Varje par har en egen branch. Inom paret gör en person databasdelen (Core + Infrastructure) och en
person Avalonia-delen (ViewModel + View). Klar = pull request mot `main`.

| Par | Branch | Person 1 (Core + Infrastructure) | Person 2 (Client) |
|---|---|---|---|
| Kunder | `kunder` | `ICustomerRepository` + `MockCustomerRepository` med 5-6 kunder | `CustomersViewModel` + `CustomersView` (lista: namn, e-post, telefon) |
| Beställningar | `bestallningar` | `ISalesOrderRepository` + `MockSalesOrderRepository` med 4-5 ordrar som har `Customer` och `Items` | `SalesOrdersViewModel` + `SalesOrdersView` (lista: ordernummer, datum, kund, antal rader) |
| Produkter | `produkter` | `AddAsync` + `UpdateAsync` i `IProductRepository` och `ProductRepository` | formulär i `ProductsViewModel` + `Popup` i `ProductsView`, ComboBox för leverantör |
| Leverantörer + ihopkoppling | `leverantorer` | `UpdateAsync` för leverantör + granska andras pull requests | menyknappar, `PageViewsEnum`, DI-rader i `ServiceCollectionExtensions`, `MainWindowViewModel`, testa att allt bygger efter varje merge |

Regler som sparar tid:
- `PageViewsEnum`, `ServiceCollectionExtensions` och `MainWindowViewModel` ändras bara av paret
  Leverantörer + ihopkoppling. De andra paren skriver i sin pull request vilka rader som ska in.
- Ingen pushar direkt till `main`. Pull request, en annan person godkänner, sedan merge.
- Hämta `main` ofta: `git pull origin main` i din branch så du inte hamnar långt efter.

## Checklista innan man säger klart
- `dotnet build` utan fel från `Client/`
- Ny ViewModel registrerad med `AddTransient` och finns i `switch` i ServiceCollectionExtensions
- Vyn heter samma som viewmodeln minus "Model"
- Repository registrerat med `AddScoped<IXRepository, XRepository>()`
- `Include()` där vyn visar något från en annan tabell (`Supplier.Name`, `Customer.FirstName`)
