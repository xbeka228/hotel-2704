using CommunityToolkit.Mvvm.ComponentModel;
using HotelManagement.Services;

namespace HotelManagement.ViewModels;

public class ViewModelBase : ObservableObject
{
    public Lang L => Lang.Instance;
}
