﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MeetAndGoMobile.Infrastructure.Commands
{
    public class SingleExecutionCommand : ICommand
    {
        private readonly Func<object, Task> _task;
        private bool _canExecute;

        public SingleExecutionCommand(Func<Task> task)
        {
            _task = async obj => await task.Invoke();
            _canExecute = true;
        }

        public SingleExecutionCommand(Func<object, Task> task)
        {
            _task = task;
            _canExecute = true;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public async void Execute(object parameter)
        {
            if (!_canExecute)
            {
                return;
            }

            _canExecute = false;

            await _task(parameter);

            _canExecute = true;
        }

        public event EventHandler CanExecuteChanged;
    }

    public class SingleExecutionCommand<T> : ICommand
    {
        private readonly Func<T, Task> _task;
        private bool _canExecute;
        
        public SingleExecutionCommand(Func<T, Task> task)
        {
            _task = task;
            _canExecute = true;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public async void Execute(object parameter)
        {
            if (!_canExecute)
            {
                return;
            }

            _canExecute = false;

            await _task((T)parameter);

            _canExecute = true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
