// MainWindow

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Text;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



// APKDeployment namespace 
namespace APKDeployment
{

    // MainWindow class
    public partial class MainWindow : Window
    {

        //IsPairedProperty
        public static readonly DependencyProperty IsPairedProperty 
            = DependencyProperty.Register(nameof(IsPaired),
                   typeof(bool), typeof(MainWindow),
                   new PropertyMetadata((object)false));

        // DeviceNameProperty 
        public static readonly DependencyProperty DeviceNameProperty 
            = DependencyProperty.Register(nameof(DeviceName),
                    typeof(string), typeof(MainWindow),
                    new PropertyMetadata((object)string.Empty));

        // DependencyProperty FilesProperty
        public static readonly DependencyProperty FilesProperty 
            = DependencyProperty.Register(nameof(Files),
                    typeof(ObservableCollection<APKFile>), typeof(MainWindow),
                    new PropertyMetadata((PropertyChangedCallback)null));

        // bool IsPaired
        public bool IsPaired
        {
            get => (bool)this.GetValue(MainWindow.IsPairedProperty);
            set => this.SetValue(MainWindow.IsPairedProperty, (object)value);
        }

        // string DeviceName
        public string DeviceName
        {
            get => (string)this.GetValue(MainWindow.DeviceNameProperty);
            set => this.SetValue(MainWindow.DeviceNameProperty, (object)value);
        }


        // ObservableCollection<APKFile> Files
        public ObservableCollection<APKFile> Files
        {
            get => (ObservableCollection<APKFile>)this.GetValue(MainWindow.FilesProperty);
            set => this.SetValue(MainWindow.FilesProperty, (object)value);
        }


        // MainWindow
        public MainWindow()
        {
            this.InitializeComponent();

            this.DataContext = (object)this;
            this.Loaded += new RoutedEventHandler(this.MainWindow_Loaded);

            Files = new ObservableCollection<APKFile>();
        }//MainWindow end


        // RunCommand      
        private async Task<string> RunCommand(string app, string args)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

            Thread thread = new Thread((ThreadStart)(() =>
            tcs.SetResult(Process.Start(new ProcessStartInfo(app, args)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,

                //TMP (test mode)
                WindowStyle = ProcessWindowStyle.Normal//.Hidden
            }).StandardOutput.ReadToEnd())));

            thread.Start();
        
            return await tcs.Task;

        }//RunCommand end


        // MainWindow_Loaded
        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string outputStr = await this.RunCommand("wconnect.exe", "devices");

            Regex regex = new Regex("(?<deviceName>.+) on (?<method>.+)");
            
            MatchCollection matches = regex.Matches(outputStr);

            if (matches.Count > 1)
            {
                int num = (int)MessageBox.Show(
                    "There are more than 1 devices connected. They will be discoonected");

                foreach (Match match in matches)
                {
                    string str = await this.RunCommand("wconnect.exe",
                        string.Format("disconnect {0}",
                        (object)match.Groups["deviceName"].ToString()));
                }
            }
            else if (outputStr.StartsWith("The following Windows devices(s) are connected:"))
            {
                this.DeviceName = matches[0].ToString().Trim();
                this.IsPaired = true;
            }
            else
            {
                // TMP (Test mode)
                this.DeviceName = outputStr; //!

                this.IsPaired = false;
            }

        }//MainWindow_Loaded end


        // btnUnPair_Click
        private async void btnUnPair_Click(object sender, RoutedEventArgs e)
        {
            string output = await this.RunCommand
            (
                "wconnect.exe",
                    string.Format
                    (
                        "disconnect {0}", 
                        (object)this.DeviceName.Split
                        (
                            new string[1]
                            {
                                " on "
                            }, 
                        StringSplitOptions.None
                        )[0]
                    )
            );

            this.IsPaired = false;
            
            this.DeviceName = "";

        }//btnUnPair_Click end


        // Button_Click_1
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbKey.Text))
            {
                int num1 = (int)MessageBox.Show("Enter key to pair");
            }
            else
            {
                //string outputStr = await this.RunCommand(
                //    "wconnect.exe",
                //    string.Format("usb {0}",
                //    (object)this.tbKey.Text)
                //    );
                string outputStr = await this.RunCommand(
                   "wconnect.exe",
                   string.Format("usb {0}",
                   tbKey.Text)
                   );


                Regex regex = new Regex("(?<deviceName>.+) on USB connected");

                Match match = regex.Match(outputStr);
                
                if (match.Success)
                {
                    this.DeviceName = string.Format("{0} on USB",
                        (object)match.Groups["deviceName"].ToString());
                    this.IsPaired = true;
                }
                else
                {
                    int num2 = (int)MessageBox.Show(outputStr);
                    this.IsPaired = false;
                }
            }//else

        }//Button_Click_1 end


        // ListView_Drop
        private void ListView_Drop(object sender, DragEventArgs e)
        {
            // drop files cycle
            foreach (string fileName in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                FileInfo fileInfo = new FileInfo(fileName);
                this.Files.Add(new APKFile()
                {
                    Path = fileInfo.FullName,
                    Size = string.Format("{0:N2} MB",
                  (object)(float)((double)fileInfo.Length / 1024.0 / 1024.0)),
                    State = ""
                });
            }

        }//ListView_Drop end


        // Button_Click_2
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (!this.IsPaired)
            {
                int num = (int)MessageBox.Show("Device is not paired");
            }

            APKFile apk = ((FrameworkElement)sender).DataContext as APKFile;
            
            await this.Deploy(apk);

        }//Button_Click_2 end


        // Deploy
        private async Task Deploy(APKFile apk)
        {
            if (!this.IsPaired)
                return;

            apk.IsDeploying = true;
            this.Cursor = Cursors.Wait;
            DateTime start = DateTime.Now;
            string output = await this.RunCommand
            (
                "adb.exe", 
                string.Format
                (
                    "install \"{0}\"",
                    (object)apk.Path.Replace("\\", "\\\\")
                )
            );

            apk.Duration = string.Format("{0:N2}s", (object)(DateTime.Now - start).TotalSeconds);
            apk.IsDeploying = false;
            apk.IsDeployed = true;
            this.Cursor = Cursors.Arrow;
            apk.StateForeground = !output.Contains("Success")
                      ? (Brush)new BrushConverter().ConvertFrom((object)"Red")
                      : (Brush)new BrushConverter().ConvertFrom((object)"Green");

            apk.DeployState = output.Replace("\r\r", "\r").Trim();
        }//Delpoy end


        // Button_Click_3
        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            List<APKFile> files = this.Files.Where<APKFile>(
                (Func<APKFile, bool>)(t => !t.IsDeployed)).ToList<APKFile>();

            foreach (APKFile file in files)
            {
                await this.Deploy(file);
            }
        }//Button_Click_3 end


        // Button_Click_4
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (!this.IsPaired)
            {
                int num = (int)MessageBox.Show("Device is not paired");
            }
            APKFile dataContext = ((FrameworkElement)sender).DataContext as APKFile;
            Button button = sender as Button;
            button.IsEnabled = false;
            this.Files.Remove(dataContext);
            button.IsEnabled = true;
        }//Button_Click_4

        /*
        // Connect
        public void Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.tbKey = (TextBox)target;
                    break;
                case 2:
                    ((ButtonBase)target).Click += new RoutedEventHandler(this.Button_Click_1);
                    break;
                case 3:
                    ((ButtonBase)target).Click += new RoutedEventHandler(this.btnUnPair_Click);
                    break;
                case 4:
                    ((UIElement)target).Drop += new DragEventHandler(this.ListView_Drop);
                    break;
                case 7:
                    ((ButtonBase)target).Click += new RoutedEventHandler(this.Button_Click_3);
                    break;
                default:
                    this._contentLoaded = true;
                    break;
            }
        }//Connect
        */

        
    }//class end

}//namespace end
