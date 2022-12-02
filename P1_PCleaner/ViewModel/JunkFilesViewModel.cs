using System;
using System.IO;
using System.Linq;
using System.Threading;
using P1_PCleaner.Command;
using P1_PCleaner.Factory;
using P1_PCleaner.Model;
using P1_PCleaner.Repository;
using P1_PCleaner.Util;

namespace P1_PCleaner.ViewModel;

public class JunkFilesViewModel : ViewModelBase
{
    private readonly NavbarViewModel _navbar;
    private readonly BrowserCacheCategoryViewModel _browserCacheCategoryViewModel;
    private readonly DownloadsCategoryViewModel _downloadsCategoryViewModel;

    private readonly WindowsSystemCategoryViewModel _windowsCategoryViewModel;
    private ViewModelBase? _currentJunkCategory;

    public SizeInfo WindowsSystemSize => new FileSizeFactory().CreateInfo
    (
        App.ScannedRepository.GetCategory(IFilesRepository.ScanCategory.SystemLogFiles).SizeInfo.Length +
        App.ScannedRepository.GetCategory(IFilesRepository.ScanCategory.SystemCache).SizeInfo.Length +
        App.ScannedRepository.GetCategory(IFilesRepository.ScanCategory.TempFiles).SizeInfo.Length
    );

    public SizeInfo BrowserCacheSize =>
        App.ScannedRepository.GetCategory(IFilesRepository.ScanCategory.GoogleChrome).SizeInfo +
        App.ScannedRepository.GetCategory(IFilesRepository.ScanCategory.MozillaFirefox).SizeInfo +
        App.ScannedRepository.GetCategory(IFilesRepository.ScanCategory.MicrosoftEdge).SizeInfo;

    public SizeInfo DownloadsSize =>
        App.ScannedRepository.GetCategory(IFilesRepository.ScanCategory.Downloads).SizeInfo;

    public SizeInfo SelectedSize
    {
        get
        {
            var selected = App.ScannedRepository.Categories().Values.Where(category => category.IsSelected)
                .Select(category => category.SizeInfo);
            var length = selected.Sum(sizeInfo => sizeInfo.Length);
            return new FileSizeFactory().CreateInfo(length);
        }
    }

    public SizeInfo TotalSize =>
        new FileSizeFactory().CreateInfo(App.ScannedRepository.Categories().Values
            .Sum(category => category.SizeInfo.Length));

    public ViewModelBase? CurrentJunkCategory
    {
        get => _currentJunkCategory;
        set
        {
            _currentJunkCategory = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand<JunkCategory> ChangeJunkCategory { get; }

    public AsyncRelayCommand ClearSelectedFiles => new(_ =>
    {
        var loadingModel = new LoadingViewModel(_navbar, ViewType.JunkFiles);
        _navbar.CurrentViewModel = loadingModel;
        
        var selectedCategories = App.ScannedRepository.Categories().Where(pair => pair.Value.IsSelected)
            .Select(pair => pair.Key);
        var selectedFiles = App.ScannedRepository.Categories().Values.Where(category => category.IsSelected)
            .SelectMany(category => category.Files)
            ;
        foreach (var selectedCategory in selectedCategories)
            App.ScannedRepository.Categories().Remove(selectedCategory);
        
        var deletedCount = 0;
        var fileInfs = selectedFiles as FileInf[] ?? selectedFiles.ToArray();
        _navbar.IsLocked = true;
        loadingModel.IsLoading = true;
        foreach (var inf in fileInfs)
        {
            deletedCount++;
            loadingModel.Text = $"Удаление {inf.Name}";
            loadingModel.Percent = (double)deletedCount / fileInfs.Length * 100;
            File.Delete(inf.Path);
            Thread.Sleep(1);
        }

        _navbar.IsLocked = false;
        loadingModel.IsLoading = false;
    });

    public RelayCommand Scan => new(_ =>
    {
        App.ScannedRepository.Clear();
        _navbar.ChangeViewModel(ViewType.Scanner);
    });

    public JunkFilesViewModel(NavbarViewModel navbar)
    {
        _navbar = navbar;
        _windowsCategoryViewModel = new WindowsSystemCategoryViewModel(
            App.ScannedRepository.GetCategory(IFilesRepository.ScanCategory.SystemLogFiles),
            App.ScannedRepository.GetCategory(IFilesRepository.ScanCategory.SystemCache),
            App.ScannedRepository.GetCategory(IFilesRepository.ScanCategory.TempFiles));
        _windowsCategoryViewModel.CategorySelected += _ => OnPropertyChanged(nameof(SelectedSize));

        _browserCacheCategoryViewModel = new BrowserCacheCategoryViewModel(
            App.ScannedRepository.GetCategory(IFilesRepository.ScanCategory.GoogleChrome),
            App.ScannedRepository.GetCategory(IFilesRepository.ScanCategory.MozillaFirefox),
            App.ScannedRepository.GetCategory(IFilesRepository.ScanCategory.MicrosoftEdge)
        );
        _browserCacheCategoryViewModel.CategorySelected += _ => OnPropertyChanged(nameof(SelectedSize));

        _downloadsCategoryViewModel =
            new DownloadsCategoryViewModel(App.ScannedRepository.GetCategory(IFilesRepository.ScanCategory.Downloads));
        _downloadsCategoryViewModel.CategorySelected += _ => OnPropertyChanged(nameof(SelectedSize));

        CurrentJunkCategory = _windowsCategoryViewModel;

        ChangeJunkCategory = new RelayCommand<JunkCategory>(ChangeCategory);
    }

    private void ChangeCategory(JunkCategory category)
    {
        CurrentJunkCategory = category switch
        {
            JunkCategory.WindowsSystem => _windowsCategoryViewModel,
            JunkCategory.BrowserCache => _browserCacheCategoryViewModel,
            JunkCategory.Downloads => _downloadsCategoryViewModel,
            _ => throw new ArgumentOutOfRangeException(nameof(category), category, null)
        };
    }
}

public enum JunkCategory
{
    WindowsSystem,
    BrowserCache,
    Downloads
}