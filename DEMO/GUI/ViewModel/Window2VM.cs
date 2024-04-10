using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI.Model;

namespace GUI.ViewModel
{
    class Window2VM : Ultilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public string Window2
        {
            get { return _pageModel.Window2; }
            set { _pageModel.Window2 = value; OnPropertyChanged(); }
        }

        public Window2VM()
        {
            _pageModel = new PageModel();
        }
    }
}
