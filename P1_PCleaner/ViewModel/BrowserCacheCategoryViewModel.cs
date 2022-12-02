using System;
using System.Windows.Input;
using P1_PCleaner.Command;
using P1_PCleaner.Model;
using P1_PCleaner.Util;

namespace P1_PCleaner.ViewModel;

public class BrowserCacheCategoryViewModel : ObservableObject
{
    public BrowserCacheCategoryViewModel(Category googleCategory, Category mozillaCategory, Category edgeCategory)
    {
        GoogleCategory = googleCategory;
        MozillaCategory = mozillaCategory;
        EdgeCategory = edgeCategory;
    }

    public BrowserCacheCategoryViewModel()
    {
        GoogleCategory = new Category();
        MozillaCategory = new Category();
        EdgeCategory = new Category();
    }

    public Category GoogleCategory { get; }

    public Category MozillaCategory { get; }

    public Category EdgeCategory { get; }

    public ICommand SelectCategory => new RelayCommand<Category>(category =>
    {
        category.IsSelected = !category.IsSelected;
        CategorySelected?.Invoke(category);
    });

    public event Action<Category>? CategorySelected;
}