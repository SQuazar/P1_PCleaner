using System;
using System.Linq;
using P1_PCleaner.Command;
using P1_PCleaner.Factory;
using P1_PCleaner.Model;
using P1_PCleaner.Repository;
using P1_PCleaner.Util;

namespace P1_PCleaner.ViewModel;

public class JunkFilesViewModel : ObservableObject
{
    private readonly BrowserCacheCategoryViewModel _browserCacheCategoryViewModel;
    private readonly DownloadsCategoryViewModel _downloadsCategoryViewModel;

    private readonly WindowsSystemCategoryViewModel _windowsCategoryViewModel;
    private ObservableObject? _currentJunkCategory;

    public JunkFilesViewModel()
    {
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

    public ObservableObject? CurrentJunkCategory
    {
        get => _currentJunkCategory;
        set
        {
            _currentJunkCategory = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand<JunkCategory> ChangeJunkCategory { get; }

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