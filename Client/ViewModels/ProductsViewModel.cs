using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class ProductsViewModel : ViewModelBase
{
    private readonly IProductRepository? _productRepo;
    private readonly ISupplierRepository? _supplierRepo;
    private readonly MainWindowViewModel? _mainWindowViewModel;
    private string _productId = string.Empty;
    // id på produkten som redigeras, 0 betyder ny produkt
    private int _editId;

    [ObservableProperty]
    public partial bool ShowDialog { get; set; }

    [ObservableProperty]
    public partial ObservableCollection<Product> Products { get; set; } = [];

    // leverantörer till comboboxen i formuläret
    [ObservableProperty]
    public partial ObservableCollection<Supplier> Suppliers { get; set; } = [];

    // formuläret för ny/ändra produkt
    [ObservableProperty]
    public partial bool ShowForm { get; set; }

    [ObservableProperty]
    public partial string ItemNumber { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Name { get; set; } = string.Empty;

    [ObservableProperty]
    public partial decimal Price { get; set; }

    [ObservableProperty]
    public partial Supplier? SelectedSupplier { get; set; }

    public ProductsViewModel()
    {
        PageTitle = "Våra produkter";
        Init();
    }
    public ProductsViewModel(IProductRepository repo, ISupplierRepository supplierRepo, MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _productRepo = repo;
        _supplierRepo = supplierRepo;
        PageTitle = "Våra produkter";

        Init();
    }

    [RelayCommand]
    private async Task Delete(string id)
    {
        try
        {
            _productId = id;
            _mainWindowViewModel!.IsBusy = true;
            ShowDialog = true;
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
        }
    }

    [RelayCommand]
    private async Task Confirm()
    {
        ShowDialog = false;
        _mainWindowViewModel!.IsBusy = false;
    }

    [RelayCommand]
    private void Cancel()
    {
        Console.WriteLine("Jasååå, du ångrar dig, fegis!");
        ShowDialog = false;
        _mainWindowViewModel!.IsBusy = false;
    }

    [RelayCommand]
    private void NewProduct()
    {
        // tomt formulär
        _editId = 0;
        ItemNumber = string.Empty;
        Name = string.Empty;
        Price = 0;
        SelectedSupplier = null;
        ShowForm = true;
    }

    [RelayCommand]
    private async Task Edit(int id)
    {
        var product = await _productRepo!.FindByIdAsync(id);
        if (product is null)
        {
            Console.WriteLine("Hittade inget!!!");
            return;
        }

        // fyll formuläret med produktens värden
        _editId = product.Id;
        ItemNumber = product.ItemNumber;
        Name = product.Name;
        Price = product.Price;
        SelectedSupplier = Suppliers.FirstOrDefault(s => s.Id == product.SupplierId);
        ShowForm = true;
    }

    [RelayCommand]
    private void CancelForm()
    {
        ShowForm = false;
    }

    [RelayCommand]
    private async Task Save()
    {
        // måste ha artikelnummer, namn och leverantör
        if (string.IsNullOrWhiteSpace(ItemNumber) || string.IsNullOrWhiteSpace(Name) || SelectedSupplier is null) return;

        try
        {
            var product = new Product
            {
                Id = _editId,
                ItemNumber = ItemNumber.Trim(),
                Name = Name.Trim(),
                Price = Price,
                SupplierId = SelectedSupplier.Id
            };

            if (_editId == 0)
            {
                await _productRepo!.AddAsync(product);
            }
            else
            {
                await _productRepo!.UpdateAsync(product);
            }

            ShowForm = false;
            // läs om listan så ändringen syns
            await LoadProducts();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private async void Init()
    {
        await LoadProducts();
        await LoadSuppliers();
    }

    private async Task LoadSuppliers()
    {
        if (_supplierRepo is not null)
        {
            Suppliers = new ObservableCollection<Supplier>(await _supplierRepo.ListAllAsync());
        }
    }

    private async Task LoadProducts()
    {
        try
        {
            _mainWindowViewModel!.IsBusy = true;
            if (_productRepo is not null)
            {
                Products = new ObservableCollection<Product>(await _productRepo.ListAllAsync());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            // laddningssnurran ska alltid stängas av
            _mainWindowViewModel!.IsBusy = false;
        }
    }
}





