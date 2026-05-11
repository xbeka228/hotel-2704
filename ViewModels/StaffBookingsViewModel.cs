using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HotelManagement.Models;
using HotelManagement.Services;

namespace HotelManagement.ViewModels;

public partial class StaffBookingsViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;

    [ObservableProperty]
    private ObservableCollection<Booking> _bookings = new();

    [ObservableProperty]
    private string _statusFilter = "Ожидает";

    [ObservableProperty]
    private string _message = "";

    [ObservableProperty]
    private bool _isMessageSuccess;

    [ObservableProperty]
    private decimal _receiptDays = 1;

    [ObservableProperty]
    private Booking? _selectedBooking;

    [ObservableProperty]
    private bool _showReceiptPanel;

    public StaffBookingsViewModel(MainWindowViewModel main)
    {
        _main = main;
        LoadBookings();
    }

    [RelayCommand]
    private void FilterAll()
    {
        StatusFilter = "";
        LoadBookings();
    }

    [RelayCommand]
    private void FilterPending()
    {
        StatusFilter = "Ожидает";
        LoadBookings();
    }

    [RelayCommand]
    private void FilterConfirmed()
    {
        StatusFilter = "Подтверждено";
        LoadBookings();
    }

    [RelayCommand]
    private void Confirm(Booking booking)
    {
        if (BookingService.ConfirmBooking(booking.Id))
        {
            Message = L.BookingConfirmed(booking.Id);
            IsMessageSuccess = true;
        }
        else
        {
            Message = L.ConfirmFailed;
            IsMessageSuccess = false;
        }
        LoadBookings();
    }

    [RelayCommand]
    private void Reject(Booking booking)
    {
        if (BookingService.RejectBooking(booking.Id))
        {
            Message = L.BookingRejected(booking.Id);
            IsMessageSuccess = true;
        }
        else
        {
            Message = L.RejectFailed;
            IsMessageSuccess = false;
        }
        LoadBookings();
    }

    [RelayCommand]
    private void OpenReceiptPanel(Booking booking)
    {
        SelectedBooking = booking;
        ReceiptDays = 1;
        ShowReceiptPanel = true;
    }

    [RelayCommand]
    private void CloseReceiptPanel()
    {
        ShowReceiptPanel = false;
        SelectedBooking = null;
    }

    [RelayCommand]
    private void GenerateReceipt()
    {
        if (SelectedBooking == null || ReceiptDays < 1)
        {
            Message = L.EnterDays;
            IsMessageSuccess = false;
            return;
        }

        try
        {
            var path = ReceiptService.GenerateReceipt(SelectedBooking, (int)ReceiptDays);
            Message = L.ReceiptSaved(path);
            IsMessageSuccess = true;
            ShowReceiptPanel = false;

            Process.Start(new ProcessStartInfo { FileName = path, UseShellExecute = true });
        }
        catch (Exception ex)
        {
            Message = $"Error: {ex.Message}";
            IsMessageSuccess = false;
        }
    }

    [RelayCommand]
    private void Refresh()
    {
        LoadBookings();
        Message = "";
    }

    private void LoadBookings()
    {
        var filter = string.IsNullOrEmpty(StatusFilter) ? null : StatusFilter;
        Bookings = new ObservableCollection<Booking>(BookingService.GetBookings(filter));
    }
}
