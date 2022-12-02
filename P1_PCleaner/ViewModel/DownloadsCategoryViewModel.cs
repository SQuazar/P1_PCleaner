using System;
using System.ComponentModel;
using System.Windows.Input;
using P1_PCleaner.Command;
using P1_PCleaner.Model;
using P1_PCleaner.Util;

namespace P1_PCleaner.ViewModel;

public class DownloadsCategoryViewModel : ObservableObject
{
    private Category _downloadsCategory;

    public Category DownloadsCategory => _downloadsCategory;

    public DownloadsCategoryViewModel(Category downloadsCategory)
    {
        _downloadsCategory = downloadsCategory;
    }

    public ICommand SelectCategory => new RelayCommand<Category>(category =>
    {
        category.IsSelected = !category.IsSelected;
        CategorySelected?.Invoke(category);
    });
    
    public event Action<Category>? CategorySelected;

    public DownloadsCategoryViewModel()
    {
        _downloadsCategory = new Category();
    }
}