using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GUI.Ultilities; 

namespace GUI.ViewModel
{
    class NavigationVM : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand Window1Command { get; set; }
        public ICommand Window2Command { get; set; }
        public ICommand Window3Command { get; set; }
        public ICommand FlightScheduleWindowCommand { get; set; }
        public ICommand Window5Command { get; set; }

        private void Window1(object obj) => CurrentView = new Window1VM();
        private void Window2(object obj) => CurrentView = new Window2VM();
        private void Window3(object obj) => CurrentView = new Window3VM();
        private void FlightScheduleWindow(object obj) => CurrentView = new FlightScheduleWindowVM();
        private void Window5(object obj) => CurrentView = new Window5VM();

        public NavigationVM()
        {
            Window1Command = new RelayCommand(Window1);
            Window2Command = new RelayCommand(Window2);
            Window3Command = new RelayCommand(Window3);
            FlightScheduleWindowCommand = new RelayCommand(FlightScheduleWindow);
            Window5Command = new RelayCommand(Window5);

            CurrentView = new Window1VM();
        }
    }
}
