using System;
using System.Windows.Input;
using P1_PCleaner.Command;
using P1_PCleaner.Model;
using P1_PCleaner.Util;

namespace P1_PCleaner.ViewModel;

public class DownloadsCategoryViewModel : ObservableObject
{
    public DownloadsCategoryViewModel(Category downloadsCategory)
    {
        DownloadsCategory = downloadsCategory;
    }

    public DownloadsCategoryViewModel()
    {
        DownloadsCategory = new Category();
    }

    public Category DownloadsCategory { get; }

    public ICommand SelectCategory => new RelayCommand<Category>(category =>
    {
        category.IsSelected = !category.IsSelected;
        CategorySelected?.Invoke(category);
    });

    public event Action<Category>? CategorySelected;
}