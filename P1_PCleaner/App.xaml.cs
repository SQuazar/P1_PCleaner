using System;
using System.IO;
using System.Linq;
using System.Windows;
using P1_PCleaner.IO;
using P1_PCleaner.Repository;

namespace P1_PCleaner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static readonly ScannedFilesRepository ScannedRepository = new();
        // Environment.ExpandEnvironmentVariables
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ScannedRepository.Load();
        }
    }
}