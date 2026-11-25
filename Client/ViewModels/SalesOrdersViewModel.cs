using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Entities;
using Core.Interfaces;

namespace Client.ViewModels;

public partial class SalesOrdersViewModel : ViewModelBase
{
    private readonly ISalesOrderRepository? _salesOrderRepo;
    private readonly MainWindowViewModel? _mainWindowViewModel;
    private string _salesOrderId = string.Empty;

    [ObservableProperty]
    public partial bool ShowDialog { get; set; }

    [ObservableProperty]
    public partial ObservableCollection<SalesOrder> SalesOrders { get; set; } = [];

    public SalesOrdersViewModel()
    {
        PageTitle = "Våra produkter";
        Init();
    }

    public SalesOrdersViewModel(ISalesOrderRepository repo, MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _salesOrderRepo = repo;
        PageTitle = "Våra produkter";

        Init();
    }

    [RelayCommand]
    private async Task Delete(string id)
    {
        try
        {
            _salesOrderId = id;
            //  _mainWindowViewModel!.IsBusy = true;
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
        //  _mainWindowViewModel!.IsBusy = false;
    }

    [RelayCommand]
    private void Cancel()
    {
        Console.WriteLine("Jasååå, du ångrar dig, fegis!");
        ShowDialog = false;
        // _mainWindowViewModel!.IsBusy = false;
    }

    [RelayCommand]
    private async Task Edit(int id)
    {
        // testar FindByIdAsync, skriver bara ut namnet än så länge
        /*var salesOrder = await _salesOrderRepo!.FindByIdAsync(id);
        if (salesOrder is not null)
        {
            Console.WriteLine(salesOrder.Name);
        }
        else
        {
            Console.WriteLine("Hittade inget!!!");
        }*/
    }

    private async void Init()
    {
        await LoadSalesOrders();
    }

    private async Task LoadSalesOrders()
    {
        try
        {
            // _mainWindowViewModel!.IsBusy = true;
            if (_salesOrderRepo is not null)
            {
                SalesOrders = new ObservableCollection<SalesOrder>(
                    await _salesOrderRepo.ListAllAsync()
                );
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            // laddningssnurran ska alltid stängas av
            //  _mainWindowViewModel!.IsBusy = false;
        }
    }
}
