using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HotelManagement.Models;
using HotelManagement.Services;

namespace HotelManagement.ViewModels;

public partial class BookingFormViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;
    public Room Room { get; }

    [ObservableProperty]
    private string _guestName = "";

    [ObservableProperty]
    private string _guestPhone = "+7";

    [ObservableProperty]
    private string _errorMessage = "";

    [ObservableProperty]
    private string _successMessage = "";

    [ObservableProperty]
    private bool _isSuccess;

    public BookingFormViewModel(Room room, MainWindowViewModel main)
    {
        Room = room;
        _main = main;
    }

    partial void OnGuestPhoneChanged(string value)
    {
        // Оставляем только цифры и +
        var filtered = new string(value.Where(c => char.IsDigit(c) || c == '+').ToArray());
        // + допускается только в начале
        if (filtered.Length > 0 && filtered[0] == '+')
            filtered = "+" + new string(filtered.Substring(1).Where(char.IsDigit).ToArray());
        else
            filtered = new string(filtered.Where(char.IsDigit).ToArray());

        if (filtered != value)
            GuestPhone = filtered;
    }

    [RelayCommand]
    private void Submit()
    {
        if (string.IsNullOrWhiteSpace(GuestName))
        {
            ErrorMessage = L.EnterName;
            return;
        }
        if (string.IsNullOrWhiteSpace(GuestPhone))
        {
            ErrorMessage = L.EnterPhone;
            return;
        }

        // Считаем только цифры
        int digitCount = GuestPhone.Count(char.IsDigit);
        if (digitCount != 11)
        {
            ErrorMessage = L.PhoneDigitsError;
            return;
        }

        BookingService.CreateBooking(Room.Id, GuestName.Trim(), GuestPhone.Trim(), Room.Class);
        ErrorMessage = "";
        SuccessMessage = L.BookingSuccess;
        IsSuccess = true;
    }

    [RelayCommand]
    private void GoBack()
    {
        _main.Navigate(new GuestRoomsViewModel(_main));
    }
}
