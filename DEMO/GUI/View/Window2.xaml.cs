using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using DTO;
using BLL;


namespace GUI.View
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : UserControl
    {
        private List<string> suggestions = new List<string> { "Gợi ý 1", "Gợi ý 2", "Gợi ý 3" };
        public Window2()
        {
            InitializeComponent();
            var converter = new BrushConverter();
            ObservableCollection<Flight> flights = new ObservableCollection<Flight>();

            //Create DataGrid Items

            flights.Add(new Flight { STT = "1", SanBayDi = "", SanBayDen = "", KhoiHanh = "", ThoiGian = "", SoGheDat = "", SoGheTrong = "" });
            flights.Add(new Flight { STT = "2", SanBayDi = "", SanBayDen = "", KhoiHanh = "", ThoiGian = "", SoGheDat = "", SoGheTrong = "" });
            flights.Add(new Flight { STT = "3", SanBayDi = "", SanBayDen = "", KhoiHanh = "", ThoiGian = "", SoGheDat = "", SoGheTrong = "" });
            flights.Add(new Flight { STT = "4", SanBayDi = "", SanBayDen = "", KhoiHanh = "", ThoiGian = "", SoGheDat = "", SoGheTrong = "" });
            flights.Add(new Flight { STT = "5", SanBayDi = "", SanBayDen = "", KhoiHanh = "", ThoiGian = "", SoGheDat = "", SoGheTrong = "" });
            FlightsDataGrid.ItemsSource = flights;
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    textInput.Visibility = Visibility.Visible;
        //    textInput.Focus();
        //}

        //private void textInput_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    string input = textInput.Text.ToLower();
        //    suggestionListBox.Items.Clear();
        //    foreach (string suggestion in suggestions)
        //    {
        //        if (suggestion.ToLower().Contains(input))
        //        {
        //            suggestionListBox.Items.Add(suggestion);
        //        }
        //    }
        //    suggestionListBox.Visibility = Visibility.Visible;
        //}

        public class Flight
        {
            public string STT { get; set; }
            public string SanBayDi { get; set; }
            public string SanBayDen { get; set; }
            public string KhoiHanh { get; set; }
            public string ThoiGian { get; set; }
            public string SoGheTrong { get; set; }
            public string SoGheDat { get; set; }


        }

        private void Flight_from_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<AirportDTO> airports = AirportBLL.GetAirports();

            // Xóa các mục cũ trước khi thêm mới
            Flight_from_comboBox.Items.Clear();

            // Lấy 5 phần tử đầu tiên của danh sách airports
            var top5Airports = airports.Take(5);

            // Tạo chuỗi để hiển thị trong MessageBox
            StringBuilder message = new StringBuilder();
            message.AppendLine("Top 5 sân bay:");

            // Duyệt qua danh sách và thêm vào chuỗi
            int count = 1;
            foreach (AirportDTO airport in top5Airports)
            {
                message.AppendLine($"{count}. {airport.AirportName}");
                count++;
            }

            // Hiển thị MessageBox
            MessageBox.Show(message.ToString(), "Top 5 sân bay", MessageBoxButton.OK, MessageBoxImage.Information);

            // Duyệt qua danh sách và thêm từng mục vào ComboBox
            foreach (AirportDTO airport in airports)
            {
                Flight_from_comboBox.Items.Add(airport.AirportName);
            }

            MessageBox.Show("Chọn sân bay đi thành công!");
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
