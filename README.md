# RunStore, grupparbete lektion 11

Avalonia-app med EF Core, uppdelad i Core, Infrastructure och Client.
Appen kör mot testdata i minnet, ingen databas behövs.

## Köra

```
cd Client
dotnet run
```

Kör från Client-mappen, json-filerna läses med sökväg därifrån.

## Mappar

- `Core`: entiteter och gränssnitt
- `Infrastructure`: repositories, MockData, json-filer, EF-kod (DbContext, migreringar)
- `Client`: Avalonia, ViewModels, Views, DI i `Helpers/ServiceCollectionExtensions.cs`

## Köra mot databas istället

Sätt anslutningssträngen med `dotnet user-secrets set "ConnectionStrings:pgsql" "..."`,
kör `dotnet ef database update -p ../Infrastructure -s .` och byt mock-raderna i
`ServiceCollectionExtensions.cs` till `ProductRepository` och `SupplierRepository`.
