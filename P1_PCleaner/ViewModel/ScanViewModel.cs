using System.Linq;
using System.Threading;
using System.Windows.Input;
using P1_PCleaner.Command;
using P1_PCleaner.Model;
using P1_PCleaner.Repository;
using P1_PCleaner.Util;

namespace P1_PCleaner.ViewModel;

public class ScanViewModel : ViewModelBase
{
    private readonly NavbarViewModel _navbar = null!;
    private readonly LoadingViewModel? _loadingViewModel;

    public bool IsLoaded => App.ScannedRepository.Categories().Values.Sum(category => category.Files.Count) != 0;

    public ICommand StartScan => new AsyncRelayCommand(_ =>
    {
        _navbar.CurrentViewModel = _loadingViewModel;

        App.ScannedRepository.CategoryStartLoading += CategoryStartLoading;
        App.ScannedRepository.CategoryLoaded += CategoryLoaded;

        if (_loadingViewModel != null)
        {
            _navbar.IsLocked = true;
            _loadingViewModel.IsLoading = true;
            App.ScannedRepository.Clear();
            App.ScannedRepository.Load();
            _navbar.IsLocked = false;
            _loadingViewModel.IsLoading = false;
            OnPropertyChanged(nameof(IsLoaded));
        }

        App.ScannedRepository.CategoryStartLoading -= CategoryStartLoading;
        App.ScannedRepository.CategoryLoaded -= CategoryLoaded;
    });

    public ScanViewModel(NavbarViewModel navbar)
    {
        _navbar = navbar;
        _loadingViewModel = new LoadingViewModel(_navbar, ViewType.Scanner);
    }

    public ScanViewModel()
    {
    }

    private void CategoryStartLoading(IFilesRepository.ScanCategory type)
    {
        if (_loadingViewModel != null)
            _loadingViewModel.Text =
                $"Получаем информацию о категории {typeof(IFilesRepository.ScanCategory).GetEnumName(type)}";
    }

    private void CategoryLoaded(IFilesRepository.ScanCategory type, Category category)
    {
        var c = 0;
        foreach (var file in category.Files)
        {
            c++;
            if (_loadingViewModel != null)
            {
                _loadingViewModel.Text = file.Name;
                _loadingViewModel.Percent = (double)c / category.Files.Count * 100;
                Thread.Sleep(1);
            }
            else break;
        }
    }
}