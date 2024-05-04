using System;
using System.Collections.Generic;
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
    public class Item
    {
        public string xyz { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : UserControl
    {
        public Window1()
        {
            InitializeComponent();
            this.Loaded += Popup_Loaded;
            //Application.Current.Deactivated += Popup_Deactivated;
            

            // Test ListView
            List<Item> items = new List<Item>
            {
            new Item { xyz = "1", Name = "Item 1", Description = "Description 1" },
            new Item { xyz = "2", Name = "Item 2", Description = "Description 2" },
            new Item { xyz = "3", Name = "Item 1", Description = "Description 1" },
            new Item { xyz = "4", Name = "Item 2", Description = "Description 2" },
            new Item { xyz = "5", Name = "Item 1", Description = "Description 1" },
            new Item { xyz = "1", Name = "Item 1", Description = "Description 1" },
            new Item { xyz = "2", Name = "Item 2", Description = "Description 2" },
            new Item { xyz = "3", Name = "Item 1", Description = "Description 1" },
            new Item { xyz = "4", Name = "Item 2", Description = "Description 2" },
            new Item { xyz = "5", Name = "Item 1", Description = "Description 1" },
            new Item { xyz = "1", Name = "Item 1", Description = "Description 1" },
            new Item { xyz = "2", Name = "Item 2", Description = "Description 2" },
            new Item { xyz = "3", Name = "Item 1", Description = "Description 1" },
            new Item { xyz = "4", Name = "Item 2", Description = "Description 2" },
            new Item { xyz = "5", Name = "Item 1", Description = "Description 1" },
            };

            MyListView.ItemsSource = items;
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
