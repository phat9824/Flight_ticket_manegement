using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
