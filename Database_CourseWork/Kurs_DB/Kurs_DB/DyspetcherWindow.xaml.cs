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
    /// Логика взаимодействия для DyspetcherWindow.xaml
    /// </summary>
    public partial class DyspetcherWindow : Window
    {
        SqlConnection connection = new SqlConnection(
            @"Data Source=YARKO-PC\SQLSERVER; Initial Catalog = BusStationDatabase; Integrated Security = True");

        private SqlDataReader reader;

        public DyspetcherWindow()
        {
            InitializeComponent();
        }

        public DataTable SelectAllTrips()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            SqlCommand selectCommand = new SqlCommand(
                "Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, t.Distance, t.TicketPrice, m.ModelName, b.AmountSeats, b.RegisrtyNumber from Trip t Join CityDeparture cd on cd.CityDepartureID = t.CityDeparture " +
                "Join CityArrival ca on ca.CityArrivalID = t.CityArrival Join Bus b on t.BusID = b.BusID Join Model m on b.BusID = m.ModelID",
                connection);


            reader = selectCommand.ExecuteReader();

            DataTable table = new DataTable();

            table.Load(reader);

            reader.Close();
            connection.Close();

            return table;
        }

        public void InsertNewTrip(int citydepID, int cityarrID, int BusID,
            TimeSpan TimeDeparture, TimeSpan TimeArrival, int distance, float price)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();


            SqlCommand insertCommand3 = new SqlCommand(
                "Insert into Trip(CityDeparture,CityArrival,BusID,Distance,TicketPrice,TimeDeparture, TimeArrival) " +
                "values (@dep,@arr,@busInvetar,@distance,@price,@timedep,@timearr)", connection);
            insertCommand3.Parameters.AddWithValue("@dep", SqlDbType.Int).Value = citydepID;
            insertCommand3.Parameters.AddWithValue("@arr", SqlDbType.Int).Value = cityarrID;
            insertCommand3.Parameters.AddWithValue("@busInvetar", SqlDbType.Int).Value = BusID;
            insertCommand3.Parameters.AddWithValue("@distance", SqlDbType.Int).Value = distance;
            insertCommand3.Parameters.AddWithValue("@price", SqlDbType.Int).Value = price;
            insertCommand3.Parameters.AddWithValue("@timedep", SqlDbType.Time).Value = TimeDeparture;
            insertCommand3.Parameters.AddWithValue("@timearr", SqlDbType.Time).Value = TimeArrival;

            insertCommand3.ExecuteNonQuery();

            connection.Close();
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void MenuItem2_OnClick(object sender, RoutedEventArgs e)
        {
            DataTable table = SelectAllTrips();
            dataGrid.ItemsSource = table.DefaultView;
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InputTripForm tripForm = new InputTripForm();
                DialogResult result = tripForm.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.Cancel)
                    return;

                int busInventory = int.Parse(tripForm.InventoryNumberBox.Text);
                int departureCode = int.Parse(tripForm.CityDepartureCodeBox.Text);
                int arriveCode = int.Parse(tripForm.CityArriveCodeBox.Text);
                int distance = int.Parse(tripForm.DistanceBox.Text);

                float price = float.Parse(tripForm.PriceBox.Text);


                TimeSpan timeDeparture = TimeSpan.Parse(tripForm.TimeDepartureBox.Text);
                TimeSpan timeArrival = TimeSpan.Parse(tripForm.TimeArrivalBox.Text);

                InsertNewTrip(departureCode, arriveCode, busInventory, timeDeparture, timeArrival, distance, price);

                DataTable table = SelectAllTrips();

                dataGrid.ItemsSource = table.DefaultView;
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SelectAllSchedule(object sender, RoutedEventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            SqlCommand command = new SqlCommand(
                "Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, sch.DepartureDate, sch.SuccessTrip from Schedule sch Join Trip t  on sch.TripID = t.TripID Join CityDeparture cd on cd.CityDepartureID = t.CityDeparture Join CityArrival ca on ca.CityArrivalID = t.CityArrival",
                connection);

            reader = command.ExecuteReader();


            DataTable table = new DataTable();
            table.Load(reader);
            reader.Close();

            connection.Close();

            dataGrid.ItemsSource = table.DefaultView;
        }

        private void Insert2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ScheduleInput input = new ScheduleInput();
                DialogResult result = input.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.Cancel)
                    return;

                int TripID = int.Parse(input.textBox1.Text);

                DateTime date = DateTime.Parse(input.dateTimePicker1.Text);

                bool success = bool.Parse(input.textBox2.Text);

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand insertCommand = new SqlCommand(
                    "Insert into Schedule([TripID],[DepartureDate],[SuccessTrip]) values (@id, @dt, @st)", connection);

                insertCommand.Parameters.AddWithValue("@id", SqlDbType.Int).Value = TripID;
                insertCommand.Parameters.AddWithValue("@dt", SqlDbType.Date).Value = date;
                insertCommand.Parameters.AddWithValue("@st", SqlDbType.Bit).Value = success;

                insertCommand.ExecuteNonQuery();

                SqlCommand command = new SqlCommand(
                    "Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, sch.DepartureDate, sch.SuccessTrip from Schedule sch Join Trip t  on sch.TripID = t.TripID Join CityDeparture cd on cd.CityDepartureID = t.CityDeparture Join CityArrival ca on ca.CityArrivalID = t.CityArrival",
                    connection);

                reader = command.ExecuteReader();


                DataTable table = new DataTable();
                table.Load(reader);
                reader.Close();

                connection.Close();

                dataGrid.ItemsSource = table.DefaultView;


            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuItem3_OnClick(object sender, RoutedEventArgs e)
        {

            string cityArrival = Interaction.InputBox("Input city of arrival", "Input city");
            if (String.IsNullOrEmpty(cityArrival))
            {
                MessageBox.Show("Empty field", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                AdminWindow window = new AdminWindow();
                DataTable table = window.SelectAllTripsByCityArrival(cityArrival);
                dataGrid.ItemsSource = table.DefaultView;
            }
        }

        private void MenuItem4_OnClick(object sender, RoutedEventArgs e)
        {
            string cityDeparture = Interaction.InputBox("Input city of departure", "Input city");
            if (String.IsNullOrEmpty(cityDeparture))
            {
                MessageBox.Show("Empty field", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                AdminWindow window = new AdminWindow();
                DataTable table = window.SelectAllTripsByCityDeparture(cityDeparture);
                dataGrid.ItemsSource = table.DefaultView;
            }
        }

        private void MenuItem5_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                TimeSpan timeFirst = TimeSpan.Parse(Interaction.InputBox("Input a first arrival", "Input data"));
                TimeSpan timeLast = TimeSpan.Parse(Interaction.InputBox("Input a last arrival", "Input data"));

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand selectCommand = new SqlCommand(
                    "Select  cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.Distance, t.TicketPrice, m.ModelName, b.AmountSeats, b.RegisrtyNumber from Trip t Join CityDeparture cd on cd.CityDepartureID = t.CityDeparture " +
                    "Join CityArrival ca on ca.CityArrivalID = t.CityArrival Join Bus b on t.BusID = b.BusID Join Model m on b.BusID = m.ModelID where t.TimeArrival >= @TimeFirst and t.TimeArrival <= @TimeLast",
                    connection);
                selectCommand.Parameters.AddWithValue("@TimeFirst", SqlDbType.Time).Value = timeFirst;
                selectCommand.Parameters.AddWithValue("@TimeLast", SqlDbType.Time).Value = timeLast;

                selectCommand.ExecuteNonQuery();

                reader = selectCommand.ExecuteReader();

                DataTable table = new DataTable();

                table.Load(reader);

                reader.Close();
                connection.Close();

                dataGrid.ItemsSource = table.DefaultView;

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuItem6_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                TimeSpan timeFirst = TimeSpan.Parse(Interaction.InputBox("Input a first departure", "Input data"));
                TimeSpan timeLast = TimeSpan.Parse(Interaction.InputBox("Input a last departure", "Input data"));

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand selectCommand = new SqlCommand(
                    "Select  cd.CityDepartureName, ca.CityArrivalName, t.TimeArrival, t.Distance, t.TicketPrice, m.ModelName, b.AmountSeats, b.RegisrtyNumber from Trip t Join CityDeparture cd on cd.CityDepartureID = t.CityDeparture " +
                    "Join CityArrival ca on ca.CityArrivalID = t.CityArrival Join Bus b on t.BusID = b.BusID Join Model m on b.BusID = m.ModelID where t.TimeDeparture >= @TimeFirst and t.TimeDeparture <= @TimeLast",
                    connection);
                selectCommand.Parameters.AddWithValue("@TimeFirst", SqlDbType.Time).Value = timeFirst;
                selectCommand.Parameters.AddWithValue("@TimeLast", SqlDbType.Time).Value = timeLast;

                selectCommand.ExecuteNonQuery();

                reader = selectCommand.ExecuteReader();

                DataTable table = new DataTable();

                table.Load(reader);

                reader.Close();
                connection.Close();

                dataGrid.ItemsSource = table.DefaultView;

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuItem7_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                int distance = int.Parse(Interaction.InputBox("Input a distance of the route", "Input data"));

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand selectCommand = new SqlCommand(
                    "Select  cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, t.TicketPrice, m.ModelName, b.AmountSeats, b.RegisrtyNumber from Trip t Join CityDeparture cd on cd.CityDepartureID = t.CityDeparture " +
                    "Join CityArrival ca on ca.CityArrivalID = t.CityArrival Join Bus b on t.BusID = b.BusID Join Model m on b.BusID = m.ModelID where t.Distance >= @dist",
                    connection);
                selectCommand.Parameters.AddWithValue("@dist", SqlDbType.Int).Value = distance;


                selectCommand.ExecuteNonQuery();

                reader = selectCommand.ExecuteReader();

                DataTable table = new DataTable();

                table.Load(reader);

                reader.Close();
                connection.Close();

                dataGrid.ItemsSource = table.DefaultView;
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuItem8_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                float price = float.Parse(Interaction.InputBox("Input a price of the route", "Input data"));

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand selectCommand = new SqlCommand(
                    "Select  cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, m.ModelName, b.AmountSeats, b.RegisrtyNumber from Trip t Join CityDeparture cd on cd.CityDepartureID = t.CityDeparture " +
                    "Join CityArrival ca on ca.CityArrivalID = t.CityArrival Join Bus b on t.BusID = b.BusID Join Model m on b.BusID = m.ModelID where t.TicketPrice >= @price",
                    connection);
                selectCommand.Parameters.AddWithValue("@price", SqlDbType.Float).Value = price;


                selectCommand.ExecuteNonQuery();

                reader = selectCommand.ExecuteReader();

                DataTable table = new DataTable();

                table.Load(reader);

                reader.Close();
                connection.Close();

                dataGrid.ItemsSource = table.DefaultView;
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Update_Click1(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateTimeDepartureForm form = new UpdateTimeDepartureForm();
                DialogResult result = form.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.Cancel)
                    return;

                TimeSpan oldtimeDeparture = TimeSpan.Parse(form.OldTimeBox.Text);
                TimeSpan newtimeDeparture = TimeSpan.Parse(form.NewTimBox.Text);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand updateCommand = new SqlCommand(
                    "Update Trip Set TimeDeparture = @newTimeDeparture where TimeDeparture = @oldTimeDeparture",
                    connection);

                updateCommand.Parameters.AddWithValue("@oldTimeDeparture", SqlDbType.Time).Value = oldtimeDeparture;
                updateCommand.Parameters.AddWithValue("@newTimeDeparture", SqlDbType.Time).Value = newtimeDeparture;


                updateCommand.ExecuteNonQuery();

                connection.Close();

                DataTable table = SelectAllTrips();
                dataGrid.ItemsSource = table.DefaultView;
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Update_Click2(object sender, RoutedEventArgs e)
        {
            try
            {


                TimeSpan oldtimeArrive = TimeSpan.Parse(Interaction.InputBox("Input old time of arrive", "Input data"));
                TimeSpan newtimeArrive = TimeSpan.Parse(Interaction.InputBox("Input new time of arrive", "Input data"));

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand updateCommand = new SqlCommand(
                    "Update Trip Set TimeArrival = @newTimeArrival where TimeArrival = @oldTimeArrival", connection);

                updateCommand.Parameters.AddWithValue("@oldTimeArrival", SqlDbType.Time).Value = oldtimeArrive;
                updateCommand.Parameters.AddWithValue("@newTimeArrival", SqlDbType.Time).Value = newtimeArrive;


                updateCommand.ExecuteNonQuery();
                connection.Close();

                DataTable table = SelectAllTrips();
                dataGrid.ItemsSource = table.DefaultView;
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Update_Click3(object sender, RoutedEventArgs e)
        {
            try
            {
                float oldPrice = float.Parse(Interaction.InputBox("Input old price of ticket", "input data"));
                float newPrice = float.Parse(Interaction.InputBox("Input new price of ticket", "input data"));

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand updateCommand = new SqlCommand(
                    "Update Trip Set TicketPrice = @newPrice where TicketPrice = @oldPrice", connection);

                updateCommand.Parameters.AddWithValue("@oldPrice", SqlDbType.Float).Value = oldPrice;
                updateCommand.Parameters.AddWithValue("@newPrice", SqlDbType.Float).Value = newPrice;


                updateCommand.ExecuteNonQuery();
                connection.Close();

                DataTable table = SelectAllTrips();
                dataGrid.ItemsSource = table.DefaultView;
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Update_Click4(object sender, RoutedEventArgs e)
        {
            try
            {
                int oldDistance = int.Parse(Interaction.InputBox("Input old price of ticket", "input data"));
                int newDistance = int.Parse(Interaction.InputBox("Input new price of ticket", "input data"));

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand updateCommand = new SqlCommand(
                    "Update Trip Set Distance = @newDistance where Distance = @oldDistance", connection);

                updateCommand.Parameters.AddWithValue("@oldDistance", SqlDbType.Int).Value = oldDistance;
                updateCommand.Parameters.AddWithValue("@newDistance", SqlDbType.Int).Value = newDistance;


                updateCommand.ExecuteNonQuery();
                connection.Close();

                DataTable table = SelectAllTrips();
                dataGrid.ItemsSource = table.DefaultView;
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DepartureDate(object sender, RoutedEventArgs e)
        {
            try
            {
                DepartureDate departureDate = new DepartureDate();
                DialogResult result = departureDate.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.Cancel)
                    return;

                DateTime date = DateTime.Parse(departureDate.dateTimePicker1.Text);

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand command = new SqlCommand(
                    "Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, sch.SuccessTrip from Schedule sch Join Trip t  on sch.TripID = t.TripID Join CityDeparture cd on cd.CityDepartureID = t.CityDeparture Join CityArrival ca on ca.CityArrivalID = t.CityArrival where sch.DepartureDate = @depDate",
                    connection);
                command.Parameters.AddWithValue("@depDate", SqlDbType.Date).Value = date;
                command.ExecuteNonQuery();

                reader = command.ExecuteReader();


                DataTable table = new DataTable();
                table.Load(reader);
                reader.Close();

                connection.Close();

                dataGrid.ItemsSource = table.DefaultView;

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SuccessTrip(object sender, RoutedEventArgs e)
        {
            try
            {
                bool success = bool.Parse(Interaction.InputBox("Input successfully of trip", "Input data"));

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand command = new SqlCommand(
                    "Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, sch.DepartureDate from Schedule sch Join Trip t  on sch.TripID = t.TripID Join CityDeparture cd on cd.CityDepartureID = t.CityDeparture Join CityArrival ca on ca.CityArrivalID = t.CityArrival where sch.SuccessTrip = @success",
                    connection);
                command.Parameters.AddWithValue("@success", SqlDbType.Bit).Value = success;
                command.ExecuteNonQuery();

                reader = command.ExecuteReader();


                DataTable table = new DataTable();
                table.Load(reader);
                reader.Close();

                connection.Close();

                dataGrid.ItemsSource = table.DefaultView;

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteAllTrips(object sender, RoutedEventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            SqlCommand DELETEALL = new SqlCommand("Delete from Trip; DBCC CHECKIDENT(Trip, RESEED, 0)", connection);
            DELETEALL.ExecuteNonQuery();
            connection.Close();

            DataTable table = SelectAllTrips();
            dataGrid.ItemsSource = table.DefaultView;
        }

        private void DeleteTrips(object sender, RoutedEventArgs e)
        {
            try
            {
                TimeSpan timeDeparture = TimeSpan.Parse(Interaction.InputBox("Input  time of departure", "Input data"));

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand deleteCommand = new SqlCommand("delete from Trip where TimeDeparture = @delete", connection);

                deleteCommand.Parameters.AddWithValue("@delete", SqlDbType.Time).Value = timeDeparture;

                deleteCommand.ExecuteNonQuery();
                connection.Close();

                DataTable table = SelectAllTrips();
                dataGrid.ItemsSource = table.DefaultView;
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void DeleteTrips2(object sender, RoutedEventArgs e)
        {
            try
            {
                TimeSpan timeArrival = TimeSpan.Parse(Interaction.InputBox("Input  time of arrival", "Input data"));

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand deleteCommand = new SqlCommand("delete from Trip where TimeArrival = @delete;", connection);

                deleteCommand.Parameters.AddWithValue("@delete", SqlDbType.Time).Value = timeArrival;

                deleteCommand.ExecuteNonQuery();
                connection.Close();

                DataTable table = SelectAllTrips();
                dataGrid.ItemsSource = table.DefaultView;
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void DeleteTrips3(object sender, RoutedEventArgs e)
        {
            try
            {
                int distance = int.Parse(Interaction.InputBox("Input distance", "Input data"));

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand deleteCommand = new SqlCommand("delete from Trip where Distance >= @delete", connection);

                deleteCommand.Parameters.AddWithValue("@delete", SqlDbType.Int).Value = distance;

                deleteCommand.ExecuteNonQuery();
                connection.Close();

                DataTable table = SelectAllTrips();
                dataGrid.ItemsSource = table.DefaultView;
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void DeleteTrips4(object sender, RoutedEventArgs e)
        {
            try
            {
                float price = float.Parse(Interaction.InputBox("Input price of ticket", "Input data"));

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand deleteCommand = new SqlCommand("delete from Trip where TicketPrice >= @delete", connection);

                deleteCommand.Parameters.AddWithValue("@delete", SqlDbType.Int).Value = price;

                deleteCommand.ExecuteNonQuery();
                connection.Close();

                DataTable table = SelectAllTrips();
                dataGrid.ItemsSource = table.DefaultView;
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void UpdateSchedule1(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateDepartureDateForm form = new UpdateDepartureDateForm();

                DialogResult result = form.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.Cancel)
                    return;

                DateTime oldDate = DateTime.Parse(form.dateTimePicker1.Text);
                DateTime newDate = DateTime.Parse(form.dateTimePicker2.Text);

                SqlCommand updateCommand = new SqlCommand("update Schedule set DepartureDate = @new where DepartureDate = @old", connection);
                updateCommand.Parameters.AddWithValue("@new", SqlDbType.Date).Value = newDate;
                updateCommand.Parameters.AddWithValue("@old", SqlDbType.Date).Value = oldDate;

                SqlCommand command = new SqlCommand("Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, sch.DepartureDate, sch.SuccessTrip from Schedule sch Join Trip t  on sch.TripID = t.TripID Join CityDeparture cd on cd.CityDepartureID = t.CityDeparture Join CityArrival ca on ca.CityArrivalID = t.CityArrival", connection);

                reader = command.ExecuteReader();


                DataTable table = new DataTable();
                table.Load(reader);
                reader.Close();

                connection.Close();

                dataGrid.ItemsSource = table.DefaultView;



            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateSchedule2(object sender, RoutedEventArgs e)
        {
            try
            {
                bool oldSuccess = bool.Parse(Interaction.InputBox("Input previous success of trip", "Input data"));
                bool newSuccess = bool.Parse(Interaction.InputBox("input a new success of trip", "Input data"));

                SqlCommand updateCommand = new SqlCommand("update Schedule set SuccessTrip = @new where DepartureDate = @old", connection);
                updateCommand.Parameters.AddWithValue("@new", SqlDbType.Bit).Value = newSuccess;
                updateCommand.Parameters.AddWithValue("@old", SqlDbType.Bit).Value = oldSuccess;

                SqlCommand command = new SqlCommand("Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, sch.DepartureDate, sch.SuccessTrip from Schedule sch Join Trip t  on sch.TripID = t.TripID Join CityDeparture cd on cd.CityDepartureID = t.CityDeparture Join CityArrival ca on ca.CityArrivalID = t.CityArrival", connection);

                reader = command.ExecuteReader();


                DataTable table = new DataTable();
                table.Load(reader);
                reader.Close();

                connection.Close();

                dataGrid.ItemsSource = table.DefaultView;
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}