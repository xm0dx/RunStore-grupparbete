using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Entities;
using Infrastructure.Migrations;

namespace Client.ViewModels;

public partial class CustomerViewModel : ViewModelBase
{
    private readonly ICustomerRepository? _customerRepo;
    private readonly MainWindowViewModel? _mainWindowViewModel;
    private string _customerId = string.Empty;

    [ObservableProperty]
    public partial bool ShowDialog { get; set; }

    [ObservableProperty]
    public partial ObservableCollection<Customer> Customer { get; set; } = [];

    public CustomerViewModel()
    {
        PageTitle = "Våra Kunder";
        Init();
    }

    public CustomerViewModel(ICustomerRepository repo, MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _customerRepo = repo;
        PageTitle = "Våra Kunder";

        Init();
    }

    [RelayCommand]
    private async Task Delete(string id)
    {
        try
        {
            _customerId = id;
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
    private async Task Edit(int id)
    {
        // testar FindByIdAsync, skriver bara ut namnet än så länge
        var customer = await _customerRepo!.FindByIdAsync(id);
        if (customer is not null)
        {
            Console.WriteLine(customer);
        }
        else
        {
            Console.WriteLine("Hittade inget!!!");
        }
    }

    private async void Init()
    {
        await LoadCustomers();
    }

    private async Task LoadCustomers()
    {
        try
        {
            _mainWindowViewModel!.IsBusy = true;
            if (_customerRepo is not null)
            {
                Customer = new ObservableCollection<Customer>(await _customerRepo.ListAllAsync());
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