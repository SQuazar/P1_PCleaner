using System;
using System.Windows.Input;

namespace P1_PCleaner.Command;

public class RelayCommand<T> : ICommand
{
    private readonly Action<T> _action;
    private readonly Func<T?, bool>? _canExecute;

    public RelayCommand(Action<T> action, Func<T?, bool>? canExecute = null)
    {
        _action = action;
        _canExecute = canExecute;
    }


    public bool CanExecute(object? parameter)
    {
        if (parameter is not T src) return false;
        return _canExecute == null || _canExecute(src);
    }

    public void Execute(object? parameter)
    {
        if (parameter is not T src) return;
        _action(src);
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}

public class RelayCommand : ICommand
{
    private readonly Action<object> _action;
    private readonly Func<object?, bool>? _canExecute;

    public RelayCommand(Action<object> action, Func<object?, bool>? canExecute = null)
    {
        _action = action;
        _canExecute = canExecute;
    }


    public bool CanExecute(object? parameter)
    {
        return _canExecute == null || _canExecute(parameter);
    }

    public void Execute(object? parameter)
    {
        _action(parameter);
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}