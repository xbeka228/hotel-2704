using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HotelManagement.Models;
using HotelManagement.Services;

namespace HotelManagement.ViewModels;

public partial class GuestRoomsViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;

    [ObservableProperty]
    private ObservableCollection<Room> _rooms = new();

    [ObservableProperty]
    private string _selectedClass = "Standart";

    [ObservableProperty]
    private bool _isStandart = true;

    [ObservableProperty]
    private bool _isComfort;

    [ObservableProperty]
    private bool _isLuxe;

    public GuestRoomsViewModel(MainWindowViewModel main)
    {
        _main = main;
        LoadRooms();
    }

    [RelayCommand]
    private void SelectClass(string roomClass)
    {
        SelectedClass = roomClass;
        IsStandart = roomClass == "Standart";
        IsComfort = roomClass == "Comfort";
        IsLuxe = roomClass == "Luxe";
        LoadRooms();
    }

    [RelayCommand]
    private void OpenRoom(Room room)
    {
        _main.Navigate(new RoomDetailViewModel(room, _main));
    }

    private void LoadRooms()
    {
        Rooms = new ObservableCollection<Room>(RoomService.GetRoomsByClass(SelectedClass));
    }
}
