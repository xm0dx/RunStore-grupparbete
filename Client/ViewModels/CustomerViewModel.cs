using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
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
    public partial ObservableCollection<Customer> Customer { get; set; }

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

        Init()
    }

}