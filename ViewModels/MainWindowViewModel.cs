using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HotelManagement.Services;

namespace HotelManagement.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private object? _currentView;

    [ObservableProperty]
    private string _currentRole = "";

    [ObservableProperty]
    private bool _isLoggedIn;

    public MainWindowViewModel()
    {
        NavigateToGuest();
    }

    [RelayCommand]
    private void ToggleLanguage()
    {
        Lang.Instance.Toggle();
        // Re-set role label
        OnPropertyChanged(nameof(L));
        if (CurrentRole == "Гость" || CurrentRole == "Қонақ")
            CurrentRole = L.Guest;
        else if (CurrentRole == "Персонал" || CurrentRole == "Қызметкер")
            CurrentRole = L.Staff;
        else if (CurrentRole == "Админ" || CurrentRole == "Әкімші")
            CurrentRole = L.Admin;

        // Recreate current view to refresh all text
        var oldView = CurrentView;
        if (oldView is GuestRoomsViewModel)
            CurrentView = new GuestRoomsViewModel(this);
        else if (oldView is StaffBookingsViewModel)
            CurrentView = new StaffBookingsViewModel(this);
        else if (oldView is AdminRoomsViewModel)
            CurrentView = new AdminRoomsViewModel(this);
        else if (oldView is LoginViewModel lvm)
            CurrentView = new LoginViewModel(lvm.RequiredRole, this);
        // other views just update L
    }

    [RelayCommand]
    private void NavigateToGuest()
    {
        CurrentRole = L.Guest;
        IsLoggedIn = false;
        CurrentView = new GuestRoomsViewModel(this);
    }

    [RelayCommand]
    private void NavigateToStaffLogin()
    {
        CurrentRole = "";
        IsLoggedIn = false;
        CurrentView = new LoginViewModel("Персонал", this);
    }

    [RelayCommand]
    private void NavigateToAdminLogin()
    {
        CurrentRole = "";
        IsLoggedIn = false;
        CurrentView = new LoginViewModel("Админ", this);
    }

    public void OnLoginSuccess(string role)
    {
        CurrentRole = role == "Персонал" ? L.Staff : L.Admin;
        IsLoggedIn = true;
        if (role == "Персонал")
            CurrentView = new StaffBookingsViewModel(this);
        else if (role == "Админ")
            CurrentView = new AdminRoomsViewModel(this);
    }

    [RelayCommand]
    private void Logout()
    {
        NavigateToGuest();
    }

    public void Navigate(object viewModel)
    {
        CurrentView = viewModel;
    }
}
