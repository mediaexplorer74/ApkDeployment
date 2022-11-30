// Decompiled with JetBrains decompiler
// Type: APKDeployment.Properties.Resources
// Assembly: APKDeployment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EF6458E0-6FC6-44C8-A8C3-981160F13A0C
// Assembly location: C:\Users\Admin\Desktop\APKDeployment.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace APKDeployment.Properties
{
  [CompilerGenerated]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (APKDeployment.Properties.Resources.resourceMan == null)
          APKDeployment.Properties.Resources.resourceMan = new ResourceManager("APKDeployment.Properties.Resources", typeof (APKDeployment.Properties.Resources).Assembly);
        return APKDeployment.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => APKDeployment.Properties.Resources.resourceCulture;
      set => APKDeployment.Properties.Resources.resourceCulture = value;
    }
  }
}
