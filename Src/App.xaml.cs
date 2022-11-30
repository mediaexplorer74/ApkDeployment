// Decompiled with JetBrains decompiler
// Type: APKDeployment.App
// Assembly: APKDeployment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EF6458E0-6FC6-44C8-A8C3-981160F13A0C
// Assembly location: C:\Users\Admin\Desktop\APKDeployment.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;

namespace APKDeployment
{
  public partial class App : Application
  {
        //private bool _contentLoaded;

        
        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent1()
        {
          if (this._contentLoaded)
            return;
          this._contentLoaded = true;
          this.StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
          Application.LoadComponent((object) this, new Uri("/APKDeployment;component/app.xaml", UriKind.Relative));
        }

        /*
        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        [STAThread]
        public static void Main()
        {
          App app = new App();
          app.InitializeComponent();
          app.Run();
        }
        */

        public static void Main()
        {
            App app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
