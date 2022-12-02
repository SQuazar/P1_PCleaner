using System;
using P1_PCleaner.Util;

namespace P1_PCleaner.ViewModel;

public class LoadingViewModel : ViewModelBase
{
    private bool _isLoading;
    private NavbarViewModel _navbar = null!;
    private string? _text;
    private double _percent;

    private ViewType _nextViewType = ViewType.Scanner;

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            if (!_isLoading)
                _navbar.ChangeViewModel(_nextViewType);
        }
    }

    public string? Text
    {
        get => _text;
        set
        {
            _text = value;
            OnPropertyChanged();
        }
    }

    public double Percent
    {
        get => _percent;
        set
        {
            _percent = value;
            OnPropertyChanged();
        }
    }

    public LoadingViewModel(NavbarViewModel navbar, ViewType nextViewType)
    {
        _navbar = navbar ?? throw new ArgumentNullException(nameof(navbar));
        _nextViewType = nextViewType;
    }

    public LoadingViewModel()
    {
    }
}