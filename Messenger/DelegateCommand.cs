using System;
using System.Windows.Input;

namespace TinyTimer.Messenger;

// Implementation of Command pattern for binding UI actions to ViewModel code
public class DelegateCommand : ICommand
{
    private readonly Action _execute;           // Action to execute when command is invoked
    private readonly Func<bool>? _canExecute;   // Condition determining if command can be executed

    // The constructor gets an action and an optional condition
    public DelegateCommand(Action execute, Func<bool>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }
    
    // Checks if command can be executed
    public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;
    
    // Executes the command's action
    public void Execute(object? parameter) => _execute();

    // Event notifying about changes in command's execution capability state
    public event EventHandler CanExecuteChanged;
    
    // Method to force notification about execution state change
    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}