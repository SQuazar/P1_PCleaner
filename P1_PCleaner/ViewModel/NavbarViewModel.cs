using System;
using System.Diagnostics;
using P1_PCleaner.Command;
using P1_PCleaner.Util;

namespace P1_PCleaner.ViewModel;

public class NavbarViewModel : ObservableObject
{
    private ObservableObject? _currentViewModel;

    public ObservableObject? CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            ViewModelChanged?.Invoke();
        }
    }

    public RelayCommand<string> OpenUrl => new(uri =>
    {
        Process.Start(new ProcessStartInfo(uri) { UseShellExecute = true });
    });

    public RelayCommand<ViewType> ChangeView => new(ChangeViewModel);

    public event Action? ViewModelChanged;

    public void ChangeViewModel(ViewType type)
    {
        CurrentViewModel = type switch
        {
            ViewType.Scanner => new ScanViewModel(),
            ViewType.RecycleBin => new RecycleBinViewModel(),
            ViewType.JunkFiles => new JunkFilesViewModel(),
            _ => new NotFoundViewModel()
        };
    }
}