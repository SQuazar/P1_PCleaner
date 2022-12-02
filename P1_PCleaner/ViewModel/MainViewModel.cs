using P1_PCleaner.Util;

namespace P1_PCleaner.ViewModel;

public class MainViewModel : ObservableObject
{
    public MainViewModel()
    {
        NavbarViewModel.CurrentViewModel = new ScanViewModel();
        NavbarViewModel.ViewModelChanged += () => OnPropertyChanged(nameof(CurrentViewModel));
    }

    public NavbarViewModel NavbarViewModel { get; } = new();

    public ObservableObject? CurrentViewModel => NavbarViewModel.CurrentViewModel;
}