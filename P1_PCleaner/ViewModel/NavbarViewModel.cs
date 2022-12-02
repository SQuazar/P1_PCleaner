using System;
using System.Diagnostics;
using System.Linq;
using P1_PCleaner.Command;
using P1_PCleaner.Util;

namespace P1_PCleaner.ViewModel;

public class NavbarViewModel : ViewModelBase
{
    private ViewModelBase _currentViewModel = new();
    private bool _isLocked;

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            ViewModelChanged?.Invoke();
        }
    }

    public bool IsLocked
    {
        get => _isLocked;
        set
        {
            _isLocked = value;
            OnPropertyChanged();
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
        if (IsLocked) return;
        if (App.ScannedRepository.Categories().Values.Sum(category => category.Files.Count) == 0)
        {
            CurrentViewModel = new ScanViewModel(this);
            return;
        }

        CurrentViewModel = type switch
        {
            ViewType.Scanner => new ScanViewModel(this),
            ViewType.RecycleBin => new RecycleBinViewModel(),
            ViewType.JunkFiles => new JunkFilesViewModel(this),
            _ => new NotFoundViewModel()
        };
    }
}