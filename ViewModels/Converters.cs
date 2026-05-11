using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using HotelManagement.Services;

namespace HotelManagement.ViewModels;

public class StatusConverter : IValueConverter
{
    public static readonly StatusConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool b) return Lang.Instance.StatusText(b);
        return "—";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}

public class StatusColorConverter : IValueConverter
{
    public static readonly StatusColorConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool b)
            return b ? new SolidColorBrush(Color.Parse("#27AE60")) : new SolidColorBrush(Color.Parse("#E74C3C"));
        return new SolidColorBrush(Colors.Gray);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}

public class StatusBgConverter : IValueConverter
{
    public static readonly StatusBgConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool b)
            return b ? new SolidColorBrush(Color.Parse("#D5F5E3")) : new SolidColorBrush(Color.Parse("#FADBD8"));
        return new SolidColorBrush(Colors.LightGray);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}

public class MessageColorConverter : IValueConverter
{
    public static readonly MessageColorConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool b)
            return b ? new SolidColorBrush(Color.Parse("#27AE60")) : new SolidColorBrush(Color.Parse("#E74C3C"));
        return new SolidColorBrush(Colors.Gray);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
