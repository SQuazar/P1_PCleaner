using System;
using System.IO;
using System.Windows.Input;
using P1_PCleaner.Command;
using P1_PCleaner.Model;
using P1_PCleaner.Util;

namespace P1_PCleaner.ViewModel;

public class JunkFilesViewModel : ObservableObject
{
    private double _selectedSize;
    private double _totalSize;

    private ObservableObject? _currentJunkCategory = new WindowsSystemCategoryViewModel();

    public double SelectedSize
    {
        get => _selectedSize;
        set
        {
            _selectedSize = value;
            OnPropertyChanged();
        }
    }

    public double TotalSize
    {
        get => _totalSize;
        set
        {
            _totalSize = value;
            OnPropertyChanged();
        }
    }

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
    public JunkFilesViewModel()
    {
        ChangeJunkCategory = new RelayCommand<JunkCategory>(ChangeCategory);
    }

    private void ChangeCategory(JunkCategory category)
    {
        CurrentJunkCategory = category switch
        {
            JunkCategory.WindowsSystem => new WindowsSystemCategoryViewModel(),
            JunkCategory.BrowserCache => new BrowserCacheCategoryViewModel(),
            _ => null
        };
    }
}

public enum JunkCategory
{
    WindowsSystem,
    BrowserCache,
    Downloads
}