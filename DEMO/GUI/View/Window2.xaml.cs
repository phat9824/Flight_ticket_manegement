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
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : UserControl
    {
        private List<string> suggestions = new List<string> { "Gợi ý 1", "Gợi ý 2", "Gợi ý 3" };
        public Window2()
        {
            InitializeComponent();
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
    }
}
