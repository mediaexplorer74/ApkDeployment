// Decompiled with JetBrains decompiler
// Type: APKDeployment.Converters.RevertConverter
// Assembly: APKDeployment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EF6458E0-6FC6-44C8-A8C3-981160F13A0C
// Assembly location: C:\Users\Admin\Desktop\APKDeployment.exe

using System;
using System.Globalization;
using System.Windows.Data;

namespace APKDeployment.Converters
{
  public class RevertConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (object) !(bool) value;

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
