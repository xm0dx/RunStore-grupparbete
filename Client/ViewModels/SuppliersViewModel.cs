using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Entities;
using Core.Interfaces;

namespace Client.ViewModels;

public partial class SuppliersViewModel : ViewModelBase
{
    private readonly ISupplierRepository? _supplierRepo;
    private readonly MainWindowViewModel? _mainWindowViewModel;

    [ObservableProperty]
    public partial ObservableCollection<Supplier> Suppliers { get; set; } = [];

    // texten i rutan för ny leverantör
    [ObservableProperty]
    public partial string NewSupplierName { get; set; } = string.Empty;

    public SuppliersViewModel()
    {
        PageTitle = "Leverantörer";
    }

    public SuppliersViewModel(ISupplierRepository repo, MainWindowViewModel mainWindowViewModel)
    {
        _supplierRepo = repo;
        _mainWindowViewModel = mainWindowViewModel;
        PageTitle = "Leverantörer";

        Init();
    }

    [RelayCommand]
    private async Task AddSupplier()
    {
        // tom text ska inte sparas
        if (string.IsNullOrWhiteSpace(NewSupplierName)) return;

        try
        {
            await _supplierRepo!.AddAsync(new Supplier { Name = NewSupplierName.Trim() });
            NewSupplierName = string.Empty;
            await LoadSuppliers();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private async void Init()
    {
        await LoadSuppliers();
    }

    private async Task LoadSuppliers()
    {
        try
        {
            _mainWindowViewModel!.IsBusy = true;
            if (_supplierRepo is not null)
            {
                Suppliers = new ObservableCollection<Supplier>(await _supplierRepo.ListAllAsync());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            _mainWindowViewModel!.IsBusy = false;
        }
    }
}
