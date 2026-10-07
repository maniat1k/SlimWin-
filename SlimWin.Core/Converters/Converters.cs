using System;
using System.Globalization;
using Avalonia.Data.Converters;
using SlimWin.Core;

namespace SlimWin.Core.Converters;

public class StatusToBrushConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not ActionStatus status)
            return Avalonia.Media.Brushes.Transparent;

        return status switch
        {
            ActionStatus.Pending => Avalonia.Media.Brushes.Gray,
            ActionStatus.Running => Avalonia.Media.Brushes.DodgerBlue,
            ActionStatus.Completed => Avalonia.Media.Brushes.LimeGreen,
            ActionStatus.Failed => Avalonia.Media.Brushes.Red,
            ActionStatus.Cancelled => Avalonia.Media.Brushes.Orange,
            _ => Avalonia.Media.Brushes.Transparent
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}

public class ResultToBrushConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not ActionStatus status)
            return Avalonia.Media.Brushes.Transparent;

        return status switch
        {
            ActionStatus.Completed => Avalonia.Media.Brushes.LimeGreen,
            ActionStatus.Failed => Avalonia.Media.Brushes.Red,
            ActionStatus.Cancelled => Avalonia.Media.Brushes.Orange,
            _ => Avalonia.Media.Brushes.Transparent
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}

public class StringToBoolConverter : IValueConverter
{
    public static readonly StringToBoolConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return !string.IsNullOrWhiteSpace(value as string);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
