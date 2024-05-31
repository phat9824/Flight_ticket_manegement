﻿using BLL;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for Window8.xaml
    /// </summary>
    /// 


    /* Mô tả:
     Tìm kiếm vé theo CustomerID, FlightID, TicketID. Có thể bổ sung nhiều thuộc tính hơn nếu có thời gian
     Mỗi thuộc tính tìm kiếm đều có thể NULL
     
     -Cần viết phương thức để trả về List<BookingTicketDTO> thỏa mãn thuộc tính tìm kiếm và parameter - phương thức này sẽ được gọi tại nút Search
     -Cần viết phương thức xóa vé được chọn (vé được chọn phải thỏa mãn parameter) - phương thức này sẽ được gọi tại nút Delete (không xóa thực sự)
     
     Lưu ý: Các Processor cần chứa thuộc tính state là trạng thái xử lí của Process, nếu state là chuỗi rỗng thì xem như thành công
     
     Khác: Khi vé được chèn vào db, sẽ có Status mặc định là 1 - Sold
                 Khi chuyến bay cất cánh, Status của vé chuyển sang 0 - Flown
                 Khi hủy vé, isDeleted = 1;
     
     */
    public partial class Window8 : UserControl
    {
        private ObservableCollection<BookingTicketDTO> listTicket;
        public Window8()
        {
            InitializeComponent();
            listTicket = new ObservableCollection<BookingTicketDTO>()
            {
                new BookingTicketDTO() {TicketID = "..", FlightID = "..", ID = "..", TicketClassID = "001", TicketStatus = 1},
                new BookingTicketDTO() {TicketID = "..", FlightID = "..", ID = "..", TicketClassID = "..", TicketStatus = 1},
                new BookingTicketDTO() {TicketID = "..", FlightID = "..", ID = "..", TicketClassID = "..", TicketStatus = 1},
                new BookingTicketDTO() {TicketID = "..", FlightID = "..", ID = "..", TicketClassID = "..", TicketStatus = 1}
            };

            dataGrid.ItemsSource = listTicket; 
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var item = button.DataContext as BookingTicketDTO;
                if (item != null)
                {
                    listTicket.Remove(item);
                }
            }
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var item = button.DataContext;
            }
        }
    }

    public class IdToNameConverterTK : IValueConverter
    {
        private Dictionary<string, string> idToNameMap = new Ticket_Class_BLL().L_TicketClass().ToDictionary(ticketclass => ticketclass.TicketClassID, ticketclass => ticketclass.TicketClassName);

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return "";

            string id = value.ToString();
            if (idToNameMap.TryGetValue(id, out string name))
            {
                return name;
            }
            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack is not supported.");
        }
    }

    public class IdToNameConverterST : IValueConverter
    {
        private Dictionary<string, string> idToNameMap = new Dictionary<string, string>
        {
            {"1", "Sold"},
            {"0", "Flown"}
        };

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return "";

            string id = value.ToString();
            if (idToNameMap.TryGetValue(id, out string name))
            {
                return name;
            }
            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack is not supported.");
        }
    }

    public class ST
    {
        public ST() { }
        public ST(string name) { }
        public string Name { get; set; }
        public string ID { get; set; }
    }
}
