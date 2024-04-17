using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Windows.Input;
using GUI.Model;
using GUI.Ultilities;

namespace GUI.ViewModel
{
    class Window4VM : Ultilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public string Window4
        {
            get { return _pageModel.Window4; }
            set { _pageModel.Window4 = value; OnPropertyChanged(); }
        }

        public Window4VM()
        {
            _pageModel = new PageModel();
        }
    }

    public class TicketClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _id;
        private string _name;
        private int _quantity;
        private string _buttonContent = "Edit";

        public string ID { get => _id; set { _id = value; OnPropertyChanged(); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public int Quantity { get => _quantity; set { _quantity = value; OnPropertyChanged(); } }
        public string ButtonContent
        {
            get => _buttonContent;
            set { _buttonContent = value; OnPropertyChanged(); }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class IntermediateAirport : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _id;
        private string _name;
        private TimeSpan _layoverTime;
        private string _note;
        private string _buttonContent = "Edit";

        public string ID { get => _id; set { if (_id != value) { _id = value; OnPropertyChanged(); } } }

        public string Name { get => _name; set { if (_name != value) { _name = value; OnPropertyChanged(); } } }

        public TimeSpan LayoverTime { get => _layoverTime; set { if (_layoverTime != value) { _layoverTime = value; OnPropertyChanged(); } } }

        public string Note { get => _note; set { if (_note != value) { _note = value; OnPropertyChanged(); } } }

        public string ButtonContent
        {
            get => _buttonContent;
            set { _buttonContent = value; OnPropertyChanged(); }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
