using System.Windows;
using System.Windows.Controls;

namespace P1_PCleaner.Control;

public class CategoryRadioButton : RadioButton
{
    public static readonly DependencyProperty CategorySizeProperty = DependencyProperty.Register(nameof(CategorySize),
        typeof(double), typeof(CategoryRadioButton),
        new PropertyMetadata(0.0, OnCategorySizePropertyChanged));

    public static readonly DependencyProperty CategoryIdentificatorProperty = DependencyProperty.Register(
        nameof(CategoryIdentificator),
        typeof(string), typeof(CategoryRadioButton),
        new PropertyMetadata("B", OnCategoryIdentificatorPropertyChanged));

    static CategoryRadioButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CategoryRadioButton), new
            FrameworkPropertyMetadata(typeof(CategoryRadioButton)));
    }

    public double CategorySize
    {
        get => (double)GetValue(CategorySizeProperty);
        set => SetValue(CategorySizeProperty, value);
    }

    public string CategoryIdentificator
    {
        get => (string)GetValue(CategoryIdentificatorProperty);
        set => SetValue(CategoryIdentificatorProperty, value);
    }

    private static void OnCategorySizePropertyChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
        d.SetValue(CategorySizeProperty, e.NewValue);
    }

    private static void OnCategoryIdentificatorPropertyChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
        d.SetValue(CategoryIdentificatorProperty, e.NewValue);
    }
}