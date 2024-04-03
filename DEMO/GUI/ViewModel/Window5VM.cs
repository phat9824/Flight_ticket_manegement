using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI.Model;

namespace GUI.ViewModel
{
    class Window5VM : Ultilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public string Window5
        {
            get { return _pageModel.Window5; }
            set { _pageModel.Window5 = value; OnPropertyChanged(); }
        }

        public Window5VM()
        {
            _pageModel = new PageModel();
        }
    }
}
