﻿using System;
using System.ComponentModel;
using System.Globalization;
#if __WPF__
using System.Windows;
using System.Windows.Data;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
#endif

namespace Wokhan.UI.BindingConverters
{
    public sealed class ValueToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value == null 
               || ((value is bool || value is bool?) && !(bool)value)
               || (value is string && string.IsNullOrEmpty((string)value))
               // I guess there is a better way...
               || 0.Equals(value) || 0d.Equals(value) || 0f.Equals(value)
               ? Visibility.Collapsed : Visibility.Visible);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => Convert(value, targetType, parameter, String.Empty);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => ConvertBack(value, targetType, parameter, String.Empty);
    }
}
