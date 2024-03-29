﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FontAwesome.WPF;

namespace P1_PCleaner.Control;

public class NavRadioButton : RadioButton
{
    private static readonly FontFamily FontAwesomeFontFamily =
        new(new Uri("pack://application:,,,/FontAwesome.WPF;component/"), "./#FontAwesome");

    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon),
        typeof(FontAwesomeIcon), typeof(NavRadioButton),
        new PropertyMetadata(FontAwesomeIcon.None,
            OnIconPropertyChanged));

    static NavRadioButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(NavRadioButton), new
            FrameworkPropertyMetadata(typeof(NavRadioButton)));
    }

    public FontAwesomeIcon Icon
    {
        get => (FontAwesomeIcon)GetValue(IconProperty);
        set => SetValue(ContentProperty, value);
    }

    private static void OnIconPropertyChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
        d.SetValue(TextOptions.TextRenderingModeProperty, TextRenderingMode.ClearType);
        // d.SetValue(TextBlock.FontFamilyProperty, FontAwesomeFontFamily);
        // d.SetValue(TextBlock.TextAlignmentProperty, Center);
        d.SetValue(TextBlock.TextProperty, char.ConvertFromUtf32((int)e.NewValue));
    }
}