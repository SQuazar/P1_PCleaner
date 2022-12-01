using System.Windows;
using System.Windows.Controls;

namespace P1_PCleaner.Control;

public class CategoryCheckBox : CheckBox
{
    public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(nameof(Size),
        typeof(double), typeof(CategoryCheckBox),
        new PropertyMetadata(0.0, OnSizePropertyChanged));

    public static readonly DependencyProperty SizeSuffixProperty = DependencyProperty.Register(nameof(SizeSuffix),
        typeof(string), typeof(CategoryCheckBox),
        new PropertyMetadata("B", OnSizeSuffixPropertyChanged));

    public double Size
    {
        get => (double)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    public string SizeSuffix
    {
        get => (string)GetValue(SizeSuffixProperty);
        set => SetValue(SizeSuffixProperty, value);
    }

    static CategoryCheckBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CategoryCheckBox),
            new FrameworkPropertyMetadata(typeof(CategoryCheckBox)));
    }

    private static void OnSizePropertyChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
        d.SetValue(SizeProperty, e.NewValue);
    }

    private static void OnSizeSuffixPropertyChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
        d.SetValue(SizeSuffixProperty, e.NewValue);
    }
}