
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

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : UserControl
    {
        public ObservableCollection<CustomerData> listViewCustomerData { get; set; }

        public ICommand DeleteCommand { get; private set; }
        public Window1()
        {
            InitializeComponent();
            //this.Loaded += Popup_Loaded;
            //Application.Current.Deactivated += Popup_Deactivated;

            // Test list view
            listViewCustomerData = new ObservableCollection<CustomerData>
            {
            new CustomerData { ID = "ID1", Name = "Customer 1", Phone = "(555) 123-4501", Email = "customer1@example.com", birthDay = new DateTime(1991, 1, 1) },
            new CustomerData { ID = "ID2", Name = "Customer 2", Phone = "(555) 123-4502", Email = "customer2@example.com", birthDay = new DateTime(1992, 1, 1) },
            new CustomerData { ID = "ID3", Name = "Customer 3", Phone = "(555) 123-4503", Email = "customer3@example.com", birthDay = new DateTime(1993, 1, 1) },
            new CustomerData { ID = "ID4", Name = "Customer 4", Phone = "(555) 123-4504", Email = "customer4@example.com", birthDay = new DateTime(1994, 1, 1) },
            new CustomerData { ID = "ID5", Name = "Customer 5", Phone = "(555) 123-4505", Email = "customer5@example.com", birthDay = new DateTime(1995, 1, 1) },
            new CustomerData { ID = "ID6", Name = "Customer 6", Phone = "(555) 123-4506", Email = "customer6@example.com", birthDay = new DateTime(1996, 1, 1) },
            new CustomerData { ID = "ID7", Name = "Customer 7", Phone = "(555) 123-4507", Email = "customer7@example.com", birthDay = new DateTime(1997, 1, 1) },
            new CustomerData { ID = "ID8", Name = "Customer 8", Phone = "(555) 123-4508", Email = "customer8@example.com", birthDay = new DateTime(1998, 1, 1) },
            new CustomerData { ID = "ID9", Name = "Customer 9", Phone = "(555) 123-4509", Email = "customer9@example.com", birthDay = new DateTime(1999, 1, 1) },
            new CustomerData { ID = "ID10", Name = "Customer 10", Phone = "(555) 123-4510", Email = "customer10@example.com", birthDay = new DateTime(2000, 1, 1) }
            };
            MyListView.ItemsSource = listViewCustomerData;

            DeleteCommand = new RelayCommand<object>(DeleteItem);
            DataContext = this;

        }
        private void DeleteItem(object parameter)
        {
            var itemToRemove = parameter as CustomerData;
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
