using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HotelManagement.Models;
using HotelManagement.Services;

namespace HotelManagement.ViewModels;

public partial class AdminRoomsViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;

    [ObservableProperty]
    private ObservableCollection<Room> _rooms = new();

    [ObservableProperty]
    private string _message = "";

    [ObservableProperty]
    private bool _isMessageSuccess;

    public AdminRoomsViewModel(MainWindowViewModel main)
    {
        _main = main;
        LoadRooms();
    }

    [RelayCommand]
    private void AddRoom()
    {
        _main.Navigate(new AdminRoomEditViewModel(null, _main));
    }

    [RelayCommand]
    private void EditRoom(Room room)
    {
        _main.Navigate(new AdminRoomEditViewModel(room, _main));
    }

    [RelayCommand]
    private void DeleteRoom(Room room)
    {
        RoomService.DeleteRoom(room.Id);
        Message = L.RoomDeleted(room.Number);
        IsMessageSuccess = true;
        LoadRooms();
    }

    [RelayCommand]
    private void ToggleStatus(Room room)
    {
        RoomService.ToggleAvailability(room.Id);
        Message = L.RoomStatusChanged(room.Number, room.IsAvailable);
        IsMessageSuccess = true;
        LoadRooms();
    }

    [RelayCommand]
    private void Refresh()
    {
        LoadRooms();
        Message = "";
    }

    private void LoadRooms()
    {
        Rooms = new ObservableCollection<Room>(RoomService.GetAllRooms());
    }
}
