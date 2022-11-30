// APKDeployment.Converters.BoolVisibilityConverter

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace APKDeployment.Converters
{
  // BoolVisibilityConverter
  public class BoolVisibilityConverter : IValueConverter
  {
    
    //Convert
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      bool? nullable1 = value as bool?;

      if (nullable1.HasValue)
      {
        bool? nullable2 = nullable1;

        return (object) (Visibility) ((!nullable2.GetValueOrDefault() 
                    ? 0 
                    : (nullable2.HasValue ? 1 : 0)) != 0 ? 0 : 2);
      }
      
      if (value != null && value.ToString() == "0")
        return (object) Visibility.Collapsed;
      
      return value != null && !string.IsNullOrEmpty(value.ToString()) 
                ? (object) Visibility.Visible 
                : (object) Visibility.Collapsed;
    }

    public object ConvertBack
    (
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture
    )
    {
      throw new NotSupportedException();
    }
  }
}
