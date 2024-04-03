using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI.Model;

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
}
