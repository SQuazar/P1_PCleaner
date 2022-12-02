using System.Windows;
using P1_PCleaner.Repository;

namespace P1_PCleaner;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static readonly ScannedFilesRepository ScannedRepository = new();
}