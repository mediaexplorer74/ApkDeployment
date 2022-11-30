// APKDeployment.Converters.RevertConverter

using System;
using System.Globalization;
using System.Windows.Data;

namespace APKDeployment.Converters
{
    // RevertConverter
    public class RevertConverter : IValueConverter
  {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
        return (object)!(bool)value;
      }

      public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      throw new NotSupportedException();
    }
  }
}
