using System.Text.Encodings.Web;
using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

// testdata i minnet, läses från json-filerna en gång när programmet startar
// alla mock-repositories delar på samma listor så att ändringar syns överallt
public static class MockData
{
    public static List<Supplier> Suppliers { get; } = [];
    public static List<Product> Products { get; } = [];
    public static List<SalesOrder> SalesOrders { get; } = [];
    public static List<Customer> Customers { get; } = [];

    // static constructor, körs av sig själv första gången någon använder MockData
    static MockData()
    {
        var jsonOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        };

        var supplierData = File.ReadAllText("../Infrastructure/Data/TestData/suppliers.json");
        var suppliers = JsonSerializer.Deserialize<List<Supplier>>(supplierData, jsonOptions) ?? [];

        // json har inga id, så vi numrerar själva från 1
        var id = 1;
        foreach (var supplier in suppliers)
        {
            supplier.Id = id++;
            Suppliers.Add(supplier);
        }

        var productData = File.ReadAllText("../Infrastructure/Data/TestData/products.json");
        var products = JsonSerializer.Deserialize<List<Product>>(productData, jsonOptions) ?? [];

        id = 1;
        foreach (var product in products)
        {
            product.Id = id++;
            // koppla ihop produkten med sin leverantör, i databasen gör Include det jobbet
            product.Supplier = Suppliers.FirstOrDefault(s => s.Id == product.SupplierId);
            Products.Add(product);
        }

        //Customers
        var customerData = File.ReadAllText("../Infrastructure/Data/TestData/customers.json");
        var customers = JsonSerializer.Deserialize<List<Customer>>(customerData, jsonOptions) ?? [];
        id = 1;
        foreach (var customer in customers)
        {
            customer.Id = id++;
            Customers.Add(customer);
        }

        var salesOrderData = File.ReadAllText("../Infrastructure/Data/TestData/salesorders.json");
        var salesOrders =
            JsonSerializer.Deserialize<List<SalesOrder>>(salesOrderData, jsonOptions) ?? [];

        id = 1;
        foreach (var salesOrder in salesOrders)
        {
            salesOrder.Id = id++;
            salesOrder.Customer = Customers.First(c => c.Id == salesOrder.CustomerId);
            SalesOrders.Add(salesOrder);
        }
    }
}
