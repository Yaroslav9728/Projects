using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.VisualBasic;
using MessageBox = System.Windows.MessageBox;

namespace Kurs_DB
{
    /// <summary>
    /// Логика взаимодействия для CashyrWindow.xaml
    /// </summary>
    public partial class CashyrWindow : Window
    {
        SqlConnection connection = new SqlConnection(@"Data Source=YARKO-PC\SQLSERVER; Initial Catalog = BusStationDatabase; Integrated Security = True");
        private SqlDataReader reader;
        public CashyrWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void AllSales(object sender, RoutedEventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            SqlCommand selectCommand = new SqlCommand("Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, Schedule.DepartureDate, s.DateOfSaling, s.AmountTickets, s.PriceSum from Sale s join  Schedule on s.ScheduleID = Schedule.ScheduleID join Trip t on Schedule.TripID = t.TripID join CityDeparture cd on cd.CityDepartureID = t.CityDeparture Join CityArrival ca on ca.CityArrivalID = t.CityArrival ", connection);

            reader = selectCommand.ExecuteReader();

            DataTable Table = new DataTable();
            Table.Load(reader);

            reader.Close();
            connection.Close();

            this.table.ItemsSource = Table.DefaultView;
        }

        private void SalesByDate(object sender, RoutedEventArgs e)
        {
            try
            {
                DateSaling saling = new DateSaling();

                DialogResult result = saling.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.Cancel)
                    return;

                DateTime date = DateTime.Parse(saling.dateTimePicker1.Text);
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand selectCommand = new SqlCommand("Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, Schedule.DepartureDate, s.DateOfSaling, s.AmountTickets, s.PriceSum from Sale s join  Schedule on s.ScheduleID = Schedule.ScheduleID join Trip t on Schedule.TripID = t.TripID join CityDeparture cd on cd.CityDepartureID = t.CityDeparture Join CityArrival ca on ca.CityArrivalID = t.CityArrival where s.DateOfSaling = @date; ", connection);
                selectCommand.Parameters.AddWithValue("@date", SqlDbType.Date).Value = date;
                selectCommand.ExecuteNonQuery();

                reader = selectCommand.ExecuteReader();

                DataTable Table = new DataTable();
                Table.Load(reader);

                reader.Close();
                connection.Close();

                table.ItemsSource = Table.DefaultView;

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaleByAmount(object sender, RoutedEventArgs e)
        {
            try
            {
                int amount = int.Parse(Interaction.InputBox("Input amount of tickets", "Input data"));
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand selectCommand = new SqlCommand("Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, Schedule.DepartureDate, s.DateOfSaling, s.AmountTickets, s.PriceSum from Sale s join  Schedule on s.ScheduleID = Schedule.ScheduleID join Trip t on Schedule.TripID = t.TripID join CityDeparture cd on cd.CityDepartureID = t.CityDeparture Join CityArrival ca on ca.CityArrivalID = t.CityArrival where s.AmountTickets >= @amount; ", connection);
                selectCommand.Parameters.AddWithValue("@amount", SqlDbType.Int).Value = amount;
                selectCommand.ExecuteNonQuery();

                reader = selectCommand.ExecuteReader();

                DataTable Table = new DataTable();
                Table.Load(reader);

                reader.Close();
                connection.Close();

                table.ItemsSource = Table.DefaultView;

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaleByPrice(object sender, RoutedEventArgs e)
        {
            try
            {
                float suma = float.Parse(Interaction.InputBox("Input suma of tickets", "Input data"));
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand selectCommand = new SqlCommand("Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, Schedule.DepartureDate, s.DateOfSaling, s.AmountTickets, s.PriceSum from Sale s join  Schedule on s.ScheduleID = Schedule.ScheduleID join Trip t on Schedule.TripID = t.TripID join CityDeparture cd on cd.CityDepartureID = t.CityDeparture Join CityArrival ca on ca.CityArrivalID = t.CityArrival where s.PriceSum >= @sum; ", connection);
                selectCommand.Parameters.AddWithValue("@sum", SqlDbType.Float).Value = suma;
                selectCommand.ExecuteNonQuery();

                reader = selectCommand.ExecuteReader();

                DataTable Table = new DataTable();
                Table.Load(reader);

                reader.Close();
                connection.Close();

                table.ItemsSource = Table.DefaultView;

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
