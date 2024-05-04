using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DTO;
using GUI.Model;

namespace GUI.ViewModel
{
    class Window1VM : Ultilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public string Window1
        {
            get { return _pageModel.Window1; }
            set { _pageModel.Window1 = value; OnPropertyChanged(); }
        }

        public Window1VM()
        {
            _pageModel = new PageModel();
        }
    }
}

namespace GUI.ViewModel.StaffWin1
{
    public class CustomerData
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public DateTime birthDay { get; set; }
    }
}

public class RelayCommand<T> : ICommand
{
    private Action<T> _execute;
    private Func<T, bool> _canExecute;

    public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
        return _canExecute == null || _canExecute((T)parameter);
    }

    public void Execute(object parameter)
    {
        _execute((T)parameter);
    }

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }
}
