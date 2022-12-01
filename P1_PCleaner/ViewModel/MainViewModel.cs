using System;
using P1_PCleaner.Command;
using P1_PCleaner.Util;

namespace P1_PCleaner.ViewModel;

public class MainViewModel : ObservableObject
{
    private readonly NavbarViewModel _navbar = new();

    public NavbarViewModel NavbarViewModel => _navbar;

    public ObservableObject? CurrentViewModel => _navbar.CurrentViewModel;

    public MainViewModel()
    {
        _navbar.CurrentViewModel = new ScanViewModel();
        _navbar.ViewModelChanged += () => OnPropertyChanged(nameof(CurrentViewModel));
    }
}