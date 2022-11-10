using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Shapes;
using P1_PCleaner.Util;
using Path = System.IO.Path;

namespace P1_PCleaner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Environment.ExpandEnvironmentVariables
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //Console.WriteLine(Environment.ExpandEnvironmentVariables("%SystemDrive%"));
        }
    }
}