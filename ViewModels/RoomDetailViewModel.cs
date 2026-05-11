using System;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HotelManagement.Models;

namespace HotelManagement.ViewModels;

public partial class RoomDetailViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;
    private readonly string[] _photoNames;
    private int _currentPhotoIndex;

    public Room Room { get; }

    [ObservableProperty]
    private Bitmap? _currentPhoto;

    [ObservableProperty]
    private string _photoLabel = "1 / 3";

    public RoomDetailViewModel(Room room, MainWindowViewModel main)
    {
        Room = room;
        _main = main;
        _photoNames = new[] { room.Photo1, room.Photo2, room.Photo3 };
        LoadPhoto(0);
    }

    private void LoadPhoto(int index)
    {
        _currentPhotoIndex = index;
        PhotoLabel = $"{index + 1} / {_photoNames.Length}";
        try
        {
            var uri = new Uri($"avares://HotelManagement/Assets/Photos/{_photoNames[index]}");
            using var stream = Avalonia.Platform.AssetLoader.Open(uri);
            CurrentPhoto = new Bitmap(stream);
        }
        catch
        {
            CurrentPhoto = null;
        }
    }

    [RelayCommand]
    private void NextPhoto()
    {
        var next = (_currentPhotoIndex + 1) % _photoNames.Length;
        LoadPhoto(next);
    }

    [RelayCommand]
    private void PrevPhoto()
    {
        var prev = (_currentPhotoIndex - 1 + _photoNames.Length) % _photoNames.Length;
        LoadPhoto(prev);
    }

    [RelayCommand]
    private void BookThis()
    {
        _main.Navigate(new BookingFormViewModel(Room, _main));
    }

    [RelayCommand]
    private void GoBack()
    {
        _main.Navigate(new GuestRoomsViewModel(_main));
    }
}
