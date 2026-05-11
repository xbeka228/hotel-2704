using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HotelManagement.Models;
using HotelManagement.Services;

namespace HotelManagement.ViewModels;

public partial class AdminRoomEditViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;
    private readonly Room? _existingRoom;

    public bool IsEditing => _existingRoom != null;
    public string Title => IsEditing ? L.EditRoomTitle(_existingRoom!.Number) : L.AddRoomTitle;

    [ObservableProperty]
    private string _roomNumber = "";

    [ObservableProperty]
    private int _selectedClassIndex;

    [ObservableProperty]
    private string _price = "";

    [ObservableProperty]
    private string _description = "";

    [ObservableProperty]
    private string _photo1 = "";

    [ObservableProperty]
    private string _photo2 = "";

    [ObservableProperty]
    private string _photo3 = "";

    [ObservableProperty]
    private string _errorMessage = "";

    public string[] RoomClasses { get; } = { "Standart", "Comfort", "Luxe" };

    public AdminRoomEditViewModel(Room? existingRoom, MainWindowViewModel main)
    {
        _main = main;
        _existingRoom = existingRoom;

        if (existingRoom != null)
        {
            RoomNumber = existingRoom.Number.ToString();
            SelectedClassIndex = Array.IndexOf(RoomClasses, existingRoom.Class);
            if (SelectedClassIndex < 0) SelectedClassIndex = 0;
            Price = existingRoom.Price.ToString("F0");
            Description = existingRoom.Description;
            Photo1 = existingRoom.Photo1;
            Photo2 = existingRoom.Photo2;
            Photo3 = existingRoom.Photo3;
        }
    }

    [RelayCommand]
    private void Save()
    {
        if (!int.TryParse(RoomNumber, out int num))
        {
            ErrorMessage = L.InvalidRoomNumber;
            return;
        }
        if (!decimal.TryParse(Price, out decimal price) || price <= 0)
        {
            ErrorMessage = L.InvalidPrice;
            return;
        }

        var room = new Room
        {
            Id = _existingRoom?.Id ?? 0,
            Number = num,
            Class = RoomClasses[SelectedClassIndex],
            Price = price,
            Description = Description ?? "",
            IsAvailable = _existingRoom?.IsAvailable ?? true,
            Photo1 = Photo1 ?? "",
            Photo2 = Photo2 ?? "",
            Photo3 = Photo3 ?? "",
        };

        try
        {
            if (IsEditing)
                RoomService.UpdateRoom(room);
            else
                RoomService.AddRoom(room);

            _main.Navigate(new AdminRoomsViewModel(_main));
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error: {ex.Message}";
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        _main.Navigate(new AdminRoomsViewModel(_main));
    }
}
