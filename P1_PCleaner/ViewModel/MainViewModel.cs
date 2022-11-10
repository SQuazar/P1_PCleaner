using System;
using P1_PCleaner.Command;
using P1_PCleaner.Util;

namespace P1_PCleaner.ViewModel;

public class MainViewModel : ObservableObject
{
    private object? _currentViewModel;

    public object? CurrentViewModel 
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnPropertyChanged();
        }
    }

    public MainViewModel()
    {
        _currentViewModel = new ScanViewModel();
    }

    public RelayCommand<ViewType> ChangeView => new(ChangeViewModel);

    public void ChangeViewModel(ViewType type)
    {
        CurrentViewModel = type switch
        {
            ViewType.Scanner => new ScanViewModel(),
            ViewType.RecycleBin => new RecycleBinViewModel(),
            _ => new NotFoundViewModel()
        };
    }
}