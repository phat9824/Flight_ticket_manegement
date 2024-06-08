using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DTO;
using GUI.ViewModel;

namespace GUI.View
{
    public partial class Window10 : UserControl
    {   
        private ParameterDTO parameter = null;
        private ParameterDTO newParameter = null;
        private ObservableCollection<AirportDTO> airports = new ObservableCollection<AirportDTO>();
        private ObservableCollection<TicketClassDTO> ticketClassDTOs = new ObservableCollection<TicketClassDTO>();
        public Window10()
        {
            InitializeComponent();
            parameter = new BLL.SearchProcessor().GetParameterDTO();
            LoadData();
        }

        private void LoadData()
        {
            LoadParameter();
            airports = new ObservableCollection<AirportDTO>(new BLL.Airport_BLL().L_airport());
            ticketClassDTOs = new ObservableCollection<TicketClassDTO>(new BLL.Ticket_Class_BLL().L_TicketClass());
            ListAirport.ItemsSource = airports;
            ListTicketClass.ItemsSource = ticketClassDTOs;
        }

        private void LoadParameter()
        {
            if (parameter != null)
            {
                int numIAirports = parameter.AirportCount;
                TimeSpan minFlightTime = parameter.MinFlighTime;
                TimeSpan minDownTime = parameter.MinStopTime;
                TimeSpan maxDownTime = parameter.MaxStopTime;
                TimeSpan lastBookTicket = parameter.SlowestBookingTime;
                TimeSpan lastCancel = parameter.CancelTime;

                MinFlightTime.SelectedTime = DateTime.Today.Add(minFlightTime);
                MinDownTime.SelectedTime = DateTime.Today.Add(minDownTime);
                MaxDownTime.SelectedTime = DateTime.Today.Add(maxDownTime);
                LastBookTicket.SelectedTime = DateTime.Today.Add(lastBookTicket);
                LastCancelTicket.SelectedTime = DateTime.Today.Add(lastCancel);
                maxInterAirportTextBox.Text = numIAirports.ToString();
            }
        }

        private void Button_Add_Airport(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Delete_Airport(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Edit_Airport(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Add_TC(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Delete_TC(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Edit_TC(object sender, RoutedEventArgs e)
        {

        }

        private void maxInterAirportTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        
        }

        private void Button_Edit_main(object sender, RoutedEventArgs e)
        {
            EditButton.Visibility = Visibility.Collapsed;
            EditPanel.Visibility = Visibility.Visible;
            MinFlightTime.IsEnabled = true;
            MinDownTime.IsEnabled = true;
            MaxDownTime.IsEnabled = true;
            LastBookTicket.IsEnabled = true;
            LastCancelTicket.IsEnabled = true;
            maxInterAirportTextBox.IsEnabled = true;
        }

        private void Button_Accept_Click(object sender, RoutedEventArgs e)
        {
            EditButton.Visibility = Visibility.Visible;
            EditPanel.Visibility = Visibility.Collapsed;
            MinFlightTime.IsEnabled = false;
            MinDownTime.IsEnabled = false;
            MaxDownTime.IsEnabled = false;
            LastBookTicket.IsEnabled = false;
            LastCancelTicket.IsEnabled = false;
            maxInterAirportTextBox.IsEnabled = false;

            try
            {
                TimeSpan MinFlightTimeSpan = (MinFlightTime.SelectedTime != null) ? MinFlightTime.SelectedTime.Value.TimeOfDay : TimeSpan.Zero;
                TimeSpan MinDownTimeSpan = (MinDownTime.SelectedTime != null) ? MinDownTime.SelectedTime.Value.TimeOfDay : TimeSpan.Zero;
                TimeSpan MaxDownTimeSpan = (MaxDownTime.SelectedTime != null) ? MaxDownTime.SelectedTime.Value.TimeOfDay : TimeSpan.Zero;
                TimeSpan LastBookTicketSpan = (LastBookTicket.SelectedTime != null) ? LastBookTicket.SelectedTime.Value.TimeOfDay : TimeSpan.Zero;
                TimeSpan LastCancelTicketSpan = (LastCancelTicket.SelectedTime != null) ? LastCancelTicket.SelectedTime.Value.TimeOfDay : TimeSpan.Zero;
                int numIAirports = Convert.ToInt32(maxInterAirportTextBox.Text);

                newParameter = new ParameterDTO()
                {
                    AirportCount = numIAirports,
                    CancelTime = LastCancelTicketSpan,
                    SlowestBookingTime = LastBookTicketSpan,
                    MinStopTime = MinDownTimeSpan,
                    MaxStopTime = MaxDownTimeSpan,
                    MinFlighTime = MinFlightTimeSpan,
                };

                BLL.UpdateDataProcessor prc = new BLL.UpdateDataProcessor();

                if (newParameter.AirportCount != parameter.AirportCount)
                {
                    prc.UpdateAirportCount(numIAirports);
                    //MessageBox.Show("A");
                }

                if (newParameter.CancelTime != parameter.CancelTime)
                {   
                    prc.UpdateCancelTime(newParameter.CancelTime);
                    //MessageBox.Show("B");
                }

                if (newParameter.SlowestBookingTime != parameter.SlowestBookingTime)
                {   
                    prc.UpdateSlowestBookingTime(newParameter.SlowestBookingTime);
                    //MessageBox.Show("C");
                }

                if (newParameter.MinStopTime != parameter.MinStopTime)
                {
                    prc.UpdateMinStopTime(newParameter.MinStopTime);
                    //MessageBox.Show("D");
                }

                if (newParameter.MaxStopTime != parameter.MaxStopTime)
                {   
                    prc.UpdateMaxStopTime(newParameter.MaxStopTime);
                    //MessageBox.Show("E");
                }

                if (newParameter.MinFlighTime != parameter.MinFlighTime)
                {   prc.UpdateMinFligthTime(newParameter.MinFlighTime);
                    //MessageBox.Show("F");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error {ex.Message}");
            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            EditButton.Visibility = Visibility.Visible;
            EditPanel.Visibility = Visibility.Collapsed;
            MinFlightTime.IsEnabled = false;
            MinDownTime.IsEnabled = false;
            MaxDownTime.IsEnabled = false;
            LastBookTicket.IsEnabled = false;
            LastCancelTicket.IsEnabled = false;
            maxInterAirportTextBox.IsEnabled = false;

            LoadParameter();
        }

        private void Add_Airport_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NewAirport.Text))
            {
                BLL.Airport_BLL prc = new BLL.Airport_BLL();
                prc.insertAirport(NewAirport.Text.ToString());
                airports = new ObservableCollection<AirportDTO>(new BLL.Airport_BLL().L_airport());
                ListAirport.ItemsSource = airports;
                NewAirport.Text = string.Empty;
            }
        }

        private void Add_Class_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NewClassName.Text) && !string.IsNullOrWhiteSpace(NewMultiplier.Text))
            {
                BLL.Ticket_Class_BLL prc = new BLL.Ticket_Class_BLL();
                prc.InsertTicketClass(new TicketClassDTO() { TicketClassName = NewClassName.Text, BaseMultiplier = Convert.ToDecimal(NewMultiplier.Text.ToString())});
                ticketClassDTOs = new ObservableCollection<TicketClassDTO>(new BLL.Ticket_Class_BLL().L_TicketClass());
                ListTicketClass.ItemsSource = ticketClassDTOs;
                NewClassName.Text = string.Empty;
                NewMultiplier.Text = string.Empty;
            }
        }

        private void Delete_Airport_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is AirportDTO airport)
            {
                airports.Remove(airport);
                BLL.Airport_BLL prc = new BLL.Airport_BLL();
                prc.deleteAirport(airport.AirportID);
            }
        }

        private void Delete_Class_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is TicketClassDTO ticketClass)
            {
                ticketClassDTOs.Remove(ticketClass);
                BLL.Ticket_Class_BLL prc = new BLL.Ticket_Class_BLL();
                prc.DeleteTicketClass(ticketClass.TicketClassID);
            }
        }
    }
}
