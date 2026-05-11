using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HotelManagement.Services;

namespace HotelManagement.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;
    public string RequiredRole { get; }

    [ObservableProperty]
    private string _login = "";

    [ObservableProperty]
    private string _password = "";

    [ObservableProperty]
    private string _errorMessage = "";

    public string RoleTitle => RequiredRole == "Персонал" ? L.Staff : L.Admin;

    public LoginViewModel(string requiredRole, MainWindowViewModel main)
    {
        RequiredRole = requiredRole;
        _main = main;
    }

    [RelayCommand]
    private void TryLogin()
    {
        if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = L.LoginRequired;
            return;
        }

        if (AuthService.ValidateLogin(Login, Password, RequiredRole))
        {
            ErrorMessage = "";
            _main.OnLoginSuccess(RequiredRole);
        }
        else
        {
            ErrorMessage = L.LoginError;
        }
    }

    [RelayCommand]
    private void GoBack()
    {
        _main.NavigateToGuestCommand.Execute(null);
    }
}
