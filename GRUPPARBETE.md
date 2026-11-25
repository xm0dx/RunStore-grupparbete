# Grupparbete lektion 11

Uppgiften: repositories för produkter, kunder, leverantörer och beställningar, plus nya ViewModels och Views.

Det som ska gå att visa:
1. lista alla leverantörer
2. lägga till en ny leverantör
3. lista alla kunder (mockdata)
4. lista alla beställningar (mockdata)
5. lägga till och uppdatera en produkt

## Så hänger det ihop

Knapp i MainWindow -> MainWindowViewModel.GoToX -> XViewModel -> IXRepository -> MockXRepository -> MockData

- ViewModeln känner bara till gränssnittet, inte var datan kommer ifrån.
- `MockData` läser json-filerna i `Infrastructure/Data/TestData` en gång vid start.
- Vilket repository som används bestäms i `ServiceCollectionExtensions`. En rad per entitet.
- Vyn måste heta samma som viewmodeln utan "Model", annars hittar ViewLocator den inte.

## Vem gjorde vad

| Del | Personer | Filer |
|---|---|---|
| Leverantörer + startläge | Mick | ISupplierRepository, SupplierRepository, SuppliersViewModel, SuppliersView, MockData |
| Kunder | Marko, Oskar | ICustomerRepository, MockCustomerRepository, CustomerViewModel, CustomerView, customers.json |
| Beställningar | Jonathan, Alfred | ISalesOrderRepository, MockSalesOrderRepository, SalesOrdersViewModel, SalesOrdersView, salesorders.json |
| Produkter | Mick, Theo | AddAsync och UpdateAsync i repositories (Theo), formulär i ProductsViewModel och ProductsView (Mick) |
| Ihopkoppling | mcsnoopys m.fl. | knappar, PageViewsEnum, DI-rader, pull requests |

## Arbetssätt

En branch per par, pull request mot main när delen byggde och gick att klicka fram.
