using System;
using System.Windows.Input;
using P1_PCleaner.Command;
using P1_PCleaner.Model;
using P1_PCleaner.Util;

namespace P1_PCleaner.ViewModel;

public class BrowserCacheCategoryViewModel : ObservableObject
{
    private Category _googleCategory;
    private Category _mozillaCategory;
    private Category _edgeCategory;

    public Category GoogleCategory => _googleCategory;

    public Category MozillaCategory => _mozillaCategory;

    public Category EdgeCategory => _edgeCategory;

    public ICommand SelectCategory => new RelayCommand<Category>(category =>
    {
        category.IsSelected = !category.IsSelected;
        CategorySelected?.Invoke(category);
    });

    public event Action<Category>? CategorySelected;

    public BrowserCacheCategoryViewModel(Category googleCategory, Category mozillaCategory, Category edgeCategory)
    {
        _googleCategory = googleCategory;
        _mozillaCategory = mozillaCategory;
        _edgeCategory = edgeCategory;
    }

    public BrowserCacheCategoryViewModel()
    {
        _googleCategory = new Category();
        _mozillaCategory = new Category();
        _edgeCategory = new Category();
    }
}