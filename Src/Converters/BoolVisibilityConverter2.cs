// Decompiled with JetBrains decompiler
// Type: APKDeployment.Converters.BoolVisibilityConverter2
// Assembly: APKDeployment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EF6458E0-6FC6-44C8-A8C3-981160F13A0C
// Assembly location: C:\Users\Admin\Desktop\APKDeployment.exe

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace APKDeployment.Converters
{
  public class BoolVisibilityConverter2 : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      bool? nullable1 = value as bool?;
      if (nullable1.HasValue)
      {
        bool? nullable2 = nullable1;
        return (object) (Visibility) ((!nullable2.GetValueOrDefault() ? 0 
                    : (nullable2.HasValue ? 1 : 0)) != 0 ? 2 : 0);
      }

      if (value != null && value.ToString() == "0")
        return (object) Visibility.Visible;

      return value != null && !string.IsNullOrEmpty(value.ToString()) 
                ? (object) Visibility.Collapsed 
                : (object) Visibility.Visible;
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
