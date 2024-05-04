
using MaterialDesignThemes.Wpf;
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
using GUI.ViewModel;
using GUI.ViewModel.StaffWin1;
using System.Windows.Controls.Primitives;
using BLL;
using DTO;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : UserControl
    {
        public ObservableCollection<CustomerDTO> listViewCustomerData { get; set; }


        public List<AirportDTO> airports { get; set; }
        public ICommand DeleteCommand { get; private set; }
        public Window1()
        {
            InitializeComponent();
            //this.Loaded += Popup_Loaded;
            //Application.Current.Deactivated += Popup_Deactivated;

            // Test list view
            listViewCustomerData = new ObservableCollection<CustomerDTO>
            {
            new CustomerDTO { ID = "ID1", CustomerName = "Customer 1", Phone = "5551234501", Email = "customer1@example.com", Birth = new DateTime(1991, 1, 1) },
            new CustomerDTO { ID = "ID2", CustomerName = "Customer 2", Phone = "5551234502", Email = "customer2@example.com", Birth = new DateTime(1992, 1, 1) },
            new CustomerDTO { ID = "ID3", CustomerName = "Customer 3", Phone = "5551234503", Email = "customer3@example.com", Birth = new DateTime(1993, 1, 1) },
            new CustomerDTO { ID = "ID4", CustomerName = "Customer 4", Phone = "5551234504", Email = "customer4@example.com", Birth = new DateTime(1994, 1, 1) },
            new CustomerDTO { ID = "ID5", CustomerName = "Customer 5", Phone = "5551234505", Email = "customer5@example.com", Birth = new DateTime(1995, 1, 1) },
            new CustomerDTO { ID = "ID6", CustomerName = "Customer 6", Phone = "5551234506", Email = "customer6@example.com", Birth = new DateTime(1996, 1, 1) },
            new CustomerDTO { ID = "ID7", CustomerName = "Customer 7", Phone = "5551234507", Email = "customer7@example.com", Birth = new DateTime(1997, 1, 1) },
            new CustomerDTO { ID = "ID8", CustomerName = "Customer 8", Phone = "5551234508", Email = "customer8@example.com", Birth = new DateTime(1998, 1, 1) },
            new CustomerDTO { ID = "ID9", CustomerName = "Customer 9", Phone = "5551234509", Email = "customer9@example.com", Birth = new DateTime(1999, 1, 1) },
            new CustomerDTO { ID = "ID10", CustomerName = "Customer 10", Phone = "5551234510", Email = "customer10@example.com", Birth = new DateTime(2000, 1, 1) }
            };
            MyListView.ItemsSource = listViewCustomerData;

            Airport_BLL airport_bll = new Airport_BLL();
            airports = airport_bll.L_airport();
            


            DeleteCommand = new RelayCommand<object>(DeleteItem);
            DataContext = this;

        }
        private void DeleteItem(object parameter)
        {
            var itemToRemove = parameter as CustomerDTO;
            if (itemToRemove != null)
            {
                listViewCustomerData.Remove(itemToRemove);
            }
        }

        private void SelectButton_Click_1(object sender, RoutedEventArgs e)
        {
            Button selectButton = sender as Button;
        }

        private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void textMaChuyenBay_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void txtMaChuyenBay_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textGiave_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void txtGiaVe_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
        }

        private void FindFlight_Click(object sender, RoutedEventArgs e)
        {
            SearchFlight_Popup.IsOpen = true;
        }

        private void Popup_Loaded(object sender, RoutedEventArgs e)
        {
            SearchFlight_Popup.IsOpen = true;
        }

        private void Popup_Deactivated(object sender, EventArgs e)
        {
            SearchFlight_Popup.IsOpen = false;
        }

        private void ClosePopup_Click(object sender, RoutedEventArgs e)
        {
            SearchFlight_Popup.IsOpen = false;
        }
    }
}
