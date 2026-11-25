# RunStore, grupparbete lektion 11

Avalonia-app mot PostgreSQL (Neon) med EF Core. Startläget är slutet av lektion 10 plus en färdig
leverantörsvy som exempel. Uppgiften och uppdelningen står i `GRUPPARBETE.md`.

## Komma igång

1. Klona och gå in i Client-mappen:
   ```
   git clone https://github.com/xm0dx/RunStore-grupparbete.git
   cd RunStore-grupparbete/Client
   ```
2. Lägg in din egen anslutningssträng till Neon (den hamnar i din hemmapp, inte i repot):
   ```
   dotnet user-secrets set "ConnectionStrings:pgsql" "Host=...;Database=runstoredb;Username=...;Password=...;SSL Mode=Require"
   ```
3. Skapa tabellerna i din databas:
   ```
   dotnet ef database update -p ../Infrastructure -s .
   ```
4. Testdata: avkommentera blocket med `context.Database.Migrate()` och `SeedData.AddData(context)`
   i `App.axaml.cs`, kör appen en gång, kommentera tillbaka.
5. Kör:
   ```
   dotnet run
   ```

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
