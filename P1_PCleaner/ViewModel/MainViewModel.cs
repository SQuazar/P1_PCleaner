using P1_PCleaner.Util;

namespace P1_PCleaner.ViewModel;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        
        NavbarViewModel = new NavbarViewModel();
        NavbarViewModel.CurrentViewModel = new ScanViewModel(NavbarViewModel);
        NavbarViewModel.ViewModelChanged += () => OnPropertyChanged(nameof(CurrentViewModel));
    }

    public NavbarViewModel NavbarViewModel { get; }

    public ViewModelBase? CurrentViewModel => NavbarViewModel.CurrentViewModel;
}