using System;
using System.Windows.Input;
using P1_PCleaner.Command;
using P1_PCleaner.Model;
using P1_PCleaner.Util;

namespace P1_PCleaner.ViewModel;

public class WindowsSystemCategoryViewModel : ViewModelBase
{
    private Category _cacheCategory;
    private Category _logFilesCategory;
    private Category _tempFilesCategory;

    public WindowsSystemCategoryViewModel(Category logFilesCategory, Category cacheCategory, Category tempFilesCategory)
    {
        _logFilesCategory = logFilesCategory;
        _cacheCategory = cacheCategory;
        _tempFilesCategory = tempFilesCategory;
    }

    public WindowsSystemCategoryViewModel()
    {
        _logFilesCategory = new Category();
        _cacheCategory = new Category();
        _tempFilesCategory = new Category();
    }

    public Category LogFilesCategory
    {
        get => _logFilesCategory;
        set
        {
            _logFilesCategory = value;
            OnPropertyChanged();
        }
    }

    public Category CacheCategory
    {
        get => _cacheCategory;
        set
        {
            _cacheCategory = value;
            OnPropertyChanged();
        }
    }

    public Category TempFilesCategory
    {
        get => _tempFilesCategory;
        set
        {
            _tempFilesCategory = value;
            OnPropertyChanged();
        }
    }

    public ICommand SelectCategory => new RelayCommand<Category>(category =>
    {
        category.IsSelected = !category.IsSelected;
        CategorySelected?.Invoke(category);
    });

    public event Action<Category>? CategorySelected;
}