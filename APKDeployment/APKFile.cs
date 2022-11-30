// APKFile

using System.Windows;
using System.Windows.Media;

namespace APKDeployment
{
  public class APKFile : DependencyObject
  {
    public static readonly DependencyProperty 
            DurationProperty = DependencyProperty.Register(
                nameof (Duration), typeof (string), typeof (APKFile), 
                new PropertyMetadata((object) string.Empty));
    public static readonly DependencyProperty 
            StateForegroundProperty = DependencyProperty.Register(
                nameof (StateForeground), typeof (Brush), typeof (APKFile), 
                new PropertyMetadata(new BrushConverter().ConvertFrom((object) "Green")));

    public static readonly DependencyProperty 
            DeployStateProperty = DependencyProperty.Register(
                nameof (DeployState), typeof (string), typeof (APKFile), new 
                PropertyMetadata((object) string.Empty));

    public static readonly DependencyProperty 
            IsDeployingProperty = DependencyProperty.Register(
                nameof (IsDeploying), typeof (bool), typeof (APKFile), 
                new PropertyMetadata((object) false));

    public static readonly DependencyProperty 
            IsDeployedProperty = DependencyProperty.Register(
                nameof (IsDeployed), typeof (bool), typeof (APKFile), 
                new PropertyMetadata((object) false));

    public string Path { get; set; }

    public string Size { get; set; }

    public string State { get; set; }

    public string Duration
    {
      get => (string) this.GetValue(APKFile.DurationProperty);
      set => this.SetValue(APKFile.DurationProperty, (object) value);
    }

    public Brush StateForeground
    {
      get => (Brush) this.GetValue(APKFile.StateForegroundProperty);
      set => this.SetValue(APKFile.StateForegroundProperty, (object) value);
    }

    public string DeployState
    {
      get => (string) this.GetValue(APKFile.DeployStateProperty);
      set => this.SetValue(APKFile.DeployStateProperty, (object) value);
    }

    public bool IsDeploying
    {
      get => (bool) this.GetValue(APKFile.IsDeployingProperty);
      set => this.SetValue(APKFile.IsDeployingProperty, (object) value);
    }

    public bool IsDeployed
    {
      get => (bool) this.GetValue(APKFile.IsDeployedProperty);
      set => this.SetValue(APKFile.IsDeployedProperty, (object) value);
    }
  }
}
