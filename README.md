# RunStore, grupparbete lektion 11

Avalonia-app mot PostgreSQL (Neon) med EF Core. Startläget är slutet av lektion 10 plus en färdig
leverantörsvy som exempel. Uppgiften och uppdelningen står i `GRUPPARBETE.md`.

## Komma igång

Appen kör mot testdata i minnet (`Infrastructure/Data/MockData.cs` läser json-filerna i
`Infrastructure/Data/TestData`). Ingen databas behövs.

```
git clone https://github.com/xm0dx/RunStore-grupparbete.git
cd RunStore-grupparbete/Client
dotnet run
```

Kör alltid från `Client`-mappen, sökvägen till json-filerna utgår därifrån.
Ändringar (ny leverantör osv) finns kvar tills appen stängs, sedan läses json om.

Vill man köra mot sin egen Neon-databas istället: sätt anslutningssträngen med
`dotnet user-secrets set "ConnectionStrings:pgsql" "..."`, kör
`dotnet ef database update -p ../Infrastructure -s .` och byt till `ProductRepository` och
`SupplierRepository` i `Client/Helpers/ServiceCollectionExtensions.cs`.

## Arbetsflöde

```
git checkout kunder            # din grupps branch (kunder, bestallningar, produkter, leverantorer)
git pull origin kunder
... koda ...
git add .
git commit -m "Lista kunder i CustomersView"
git push origin kunder
```

När delen är klar: öppna en pull request mot `main` på GitHub, låt någon annan godkänna, merge.
`main` går inte att pusha till direkt.

## Mappar

| Mapp | Innehåll |
|---|---|
| `Core` | entiteter och gränssnitt (`Interfaces/`) |
| `Infrastructure` | DbContext, konfigurationsklasser, repositories, migreringar, SeedData |
| `Client` | Avalonia: ViewModels, Views, DI i `Helpers/ServiceCollectionExtensions.cs` |
| `Queries` | sql att testa i Neon |
