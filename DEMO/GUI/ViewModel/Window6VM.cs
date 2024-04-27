using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI.Model;

namespace GUI.ViewModel
{
    class Window6VM : Ultilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public string Window6
        {
            get { return _pageModel.Window6; }
            set { _pageModel.Window6 = value; OnPropertyChanged(); }
        }

        public Window6VM()
        {
            _pageModel = new PageModel();
        }
    }
}
