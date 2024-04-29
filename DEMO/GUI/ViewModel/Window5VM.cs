using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using GUI.Model;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.ObjectModel;
using DTO;

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
    public class ReportByFlightData
    {
        public string flightID { get; set; } 
        public int ticketsSold { get; set; } 
        public decimal revenue { get; set; }
        public decimal ratio { get; set; }

        public static ReportByFlightData Convert(ReportByFlightDTO reportByFlightDTO)
        {
            return new ReportByFlightData
            {
                flightID = reportByFlightDTO.flightID,
                ratio = reportByFlightDTO.ratio,
                revenue = reportByFlightDTO.revenue,
                ticketsSold = reportByFlightDTO.ticketsSold
            };
        }
        public static ObservableCollection<ReportByFlightData> ConvertListToObservableCollection(List<ReportByFlightDTO> list)
        {
            var observableCollection = new ObservableCollection<ReportByFlightData>();
            foreach (ReportByFlightDTO item in list)
            {
                observableCollection.Add(ReportByFlightData.Convert(item));
            }
            return observableCollection;
        }
    }

    public class ReportByMonthData
    {
        public static ObservableCollection<T> ConvertListToObservableCollection<T>(List<T> list)
        {
            var observableCollection = new ObservableCollection<T>();
            foreach (T item in list)
            {
                observableCollection.Add(item);
            }
            return observableCollection;
        }
    }
}
