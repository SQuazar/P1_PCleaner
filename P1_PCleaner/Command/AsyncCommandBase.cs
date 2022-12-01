using System;
using System.Threading.Tasks;

namespace P1_PCleaner.Command;

public abstract class AsyncCommandBase : CommandBase
{
    public bool IsExecuting { get; private set; }

    public override bool CanExecute(object? parameter)
    {
        return !IsExecuting && base.CanExecute(parameter);
    }

    public override async void Execute(object? parameter)
    {
        IsExecuting = true;
        try
        {
            await ExecuteAsync(parameter);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
        }
        finally
        {
            IsExecuting = false;
        }
    }

    public abstract Task ExecuteAsync(object? parameter);
}