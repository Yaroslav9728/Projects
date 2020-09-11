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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        SqlConnection connection = new SqlConnection(@"Data Source=YARKO-PC\SQLSERVER; Initial Catalog = BusStationDatabase; Integrated Security = True");
        private SqlDataReader reader;
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        public  DataTable SelectAllBuses()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            SqlCommand selectCommand = new SqlCommand("Select m.ModelName, b.AmountSeats, b.RegisrtyNumber From Bus b Join Model m on b.ModelID = m.ModelID", connection);

            reader = selectCommand.ExecuteReader();

            DataTable table = new DataTable();

            table.Load(reader);

            reader.Close();
            connection.Close();

            return table;
        }

        public DataTable SelectBusesWithSeats(int seats)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            SqlCommand selectCommand = new SqlCommand("Select m.ModelName, b.RegisrtyNumber From Bus b Join Model m on b.ModelID = m.ModelID where b.AmountSeats >= @seats", connection);
            selectCommand.Parameters.AddWithValue("@seats", SqlDbType.Int).Value = seats;
            selectCommand.ExecuteNonQuery();

            reader = selectCommand.ExecuteReader();

            DataTable table = new DataTable();

            table.Load(reader);

            reader.Close();
            connection.Close();

            return table;
        }

        public DataTable SelectBusesWithRN(string register)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            SqlCommand selectCommand = new SqlCommand("Select m.ModelName, b.AmountSeats, b.RegisrtyNumber From Bus b Join Model m on b.ModelID = m.ModelID where b.RegisrtyNumber like @register", connection);
            selectCommand.Parameters.AddWithValue("@register", SqlDbType.Text).Value = register + '%';
            selectCommand.ExecuteNonQuery();

            reader = selectCommand.ExecuteReader();

            DataTable table = new DataTable();

            table.Load(reader);

            reader.Close();
            connection.Close();

            return table;
        }

        public void InsertBus(int id, int seats, string register, string model)
        {
            if(connection.State == ConnectionState.Closed) 
                connection.Open();

            SqlCommand insertCommand = new SqlCommand("Insert into Model(ModelName) values (@model)",connection);
            insertCommand.Parameters.AddWithValue("@model", SqlDbType.NVarChar).Value = model;
            insertCommand.ExecuteNonQuery();

            SqlCommand insert = new SqlCommand("Insert into Bus(ModelID,RegisrtyNumber, AmountSeats) Values(@id, @register,@seats)",connection);
            insert.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
            insert.Parameters.AddWithValue("@register", SqlDbType.NVarChar).Value = register;
            insert.Parameters.AddWithValue("@seats", SqlDbType.Int).Value = seats;

            insert.ExecuteNonQuery();

            connection.Close();

        }

        public DataTable SelectAllTrips()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            SqlCommand selectCommand = new SqlCommand("Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, t.Distance, t.TicketPrice, m.ModelName, b.AmountSeats, b.RegisrtyNumber from Trip t Join CityDeparture cd on cd.CityDepartureID = t.CityDeparture " +
                                                      "Join CityArrival ca on ca.CityArrivalID = t.CityArrival Join Bus b on t.BusID = b.BusID Join Model m on b.BusID = m.ModelID", connection);
            

            reader = selectCommand.ExecuteReader();

            DataTable table = new DataTable();

            table.Load(reader);

            reader.Close();
            connection.Close();

            return table;
        }
        public DataTable SelectAllTripsByCityDeparture(string cityDep)
        {
                      
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            SqlCommand selectCommand = new SqlCommand("Select  ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, t.Distance, t.TicketPrice, m.ModelName, b.AmountSeats, b.RegisrtyNumber from Trip t Join CityDeparture cd on cd.CityDepartureID = t.CityDeparture " +
                                                      "Join CityArrival ca on ca.CityArrivalID = t.CityArrival Join Bus b on t.BusID = b.BusID Join Model m on b.BusID = m.ModelID where cd.CityDepartureName = @cityDep", connection);
            selectCommand.Parameters.AddWithValue("@cityDep", SqlDbType.NVarChar).Value = cityDep;
            selectCommand.ExecuteNonQuery();

            reader = selectCommand.ExecuteReader();

            DataTable table = new DataTable();

            table.Load(reader);

            reader.Close();
            connection.Close();

            return table;
        }
        private void MenuItem1_OnClick(object sender, RoutedEventArgs e)
        {
                DataTable table = SelectAllBuses();

                Table.ItemsSource = table.DefaultView;
            
           
        }

        private void MenuItem2_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                int seats = int.Parse(Interaction.InputBox("Input amount of seats", "Input data"));

                DataTable table = SelectBusesWithSeats(seats);

                Table.ItemsSource = table.DefaultView;
            }
            catch (FormatException exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuItem_OnClick3(object sender, RoutedEventArgs e)
        {
            string register = Interaction.InputBox("Input a begin of register number", "Input data");

            if (String.IsNullOrEmpty(register))
            {
                MessageBox.Show("Empty field", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                DataTable table = SelectBusesWithRN(register);

                Table.ItemsSource = table.DefaultView;
            }
        }

        private void Insert1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InputBusForm busForm = new InputBusForm();
                DialogResult result = busForm.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.Cancel)
                    return;

                int Modelid = int.Parse(busForm.IDBox.Text);
                string ModelName = busForm.NameBox.Text;
                int seats = int.Parse(busForm.SeatsBox.Text);
                string register = busForm.RegistryBox.Text;

                InsertBus(Modelid,seats,register,ModelName);

                DataTable table = SelectAllBuses();

                Table.ItemsSource = table.DefaultView;

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Trips_All_OnClick(object sender, RoutedEventArgs e)
        {
            DataTable table = SelectAllTrips();

            Table.ItemsSource = table.DefaultView;
        }

        public void InsertNewTrip(int citydepID, int cityarrID, int BusID,
            TimeSpan TimeDeparture, TimeSpan TimeArrival, int distance, float price)
        {
            if(connection.State == ConnectionState.Closed)
                connection.Open();
            

            SqlCommand insertCommand3 = new SqlCommand("Insert into Trip(CityDeparture,CityArrival,BusID,Distance,TicketPrice,TimeDeparture, TimeArrival) " +
                                                       "values (@dep,@arr,@busInvetar,@distance,@price,@timedep,@timearr)",connection);
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
        private void Insert2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InputTripForm tripForm = new InputTripForm();
                DialogResult result = tripForm.ShowDialog();

                if(result == System.Windows.Forms.DialogResult.Cancel)
                    return;

                int busInventory = int.Parse(tripForm.InventoryNumberBox.Text);
                int departureCode = int.Parse(tripForm.CityDepartureCodeBox.Text);
                int arriveCode = int.Parse(tripForm.CityArriveCodeBox.Text);
                int distance = int.Parse(tripForm.DistanceBox.Text);

                float price = float.Parse(tripForm.PriceBox.Text);


                TimeSpan timeDeparture = TimeSpan.Parse(tripForm.TimeDepartureBox.Text);
                TimeSpan timeArrival = TimeSpan.Parse(tripForm.TimeArrivalBox.Text);

                InsertNewTrip(departureCode,arriveCode,busInventory,timeDeparture,timeArrival,distance,price);

                DataTable table = SelectAllTrips();

                Table.ItemsSource = table.DefaultView;

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Insert5_Click(object sender, RoutedEventArgs e)
        {
            string insert = Interaction.InputBox("Input a city of departure", "Input data");

            if (String.IsNullOrEmpty(insert))
            {
                MessageBox.Show("Empty field", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if(connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand insertCommand  = new SqlCommand("Insert into CityDeparture([CityDepartureName]) values(@departure)", connection);
                insertCommand.Parameters.AddWithValue("@departure", SqlDbType.NVarChar).Value = insert;

                insertCommand.ExecuteNonQuery();

                connection.Close();
            }
        }

        private void Insert6_Click(object sender, RoutedEventArgs e)
        {

            string insert = Interaction.InputBox("Input a city of arrival", "Input data");

            if (String.IsNullOrEmpty(insert))
            {
                MessageBox.Show("Empty field", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand insertCommand = new SqlCommand("Insert into CityArrival([CityArrivalName]) values(@arrival)", connection);
                insertCommand.Parameters.AddWithValue("@arrival", SqlDbType.NVarChar).Value = insert;

                insertCommand.ExecuteNonQuery();

                connection.Close();
            }
        }

        private void Cities_OnClick(object sender, RoutedEventArgs e)
        {
            if(connection.State == ConnectionState.Closed)
                connection.Open();

            SqlCommand selectCommand = new SqlCommand("Select CityDepartureName from CityDeparture Union Select CityArrivalName from CityArrival", connection);

            reader = selectCommand.ExecuteReader();

            DataTable table = new DataTable();
            table.Load(reader);

            reader.Close();
            connection.Close();

            Table.ItemsSource = table.DefaultView;
        }

        private void Update_Click1(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateTimeDepartureForm form = new UpdateTimeDepartureForm();
                DialogResult result = form.ShowDialog();

                if(result == System.Windows.Forms.DialogResult.Cancel)
                    return;

                TimeSpan oldtimeDeparture = TimeSpan.Parse(form.OldTimeBox.Text);
                TimeSpan newtimeDeparture = TimeSpan.Parse(form.NewTimBox.Text);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand updateCommand = new SqlCommand("Update Trip Set TimeDeparture = @newTimeDeparture where TimeDeparture = @oldTimeDeparture",connection);

                updateCommand.Parameters.AddWithValue("@oldTimeDeparture", SqlDbType.Time).Value = oldtimeDeparture;
                updateCommand.Parameters.AddWithValue("@newTimeDeparture", SqlDbType.Time).Value = newtimeDeparture;
               

                updateCommand.ExecuteNonQuery();

                connection.Close();

                DataTable table = SelectAllTrips();
                Table.ItemsSource = table.DefaultView;
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
               

                TimeSpan oldtimeArrive = TimeSpan.Parse(Interaction.InputBox("Input old time of arrive","Input data"));
                TimeSpan newtimeArrive = TimeSpan.Parse(Interaction.InputBox("Input new time of arrive", "Input data"));

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand updateCommand = new SqlCommand("Update Trip Set TimeArrival = @newTimeArrival where TimeArrival = @oldTimeArrival", connection);

                updateCommand.Parameters.AddWithValue("@oldTimeArrival", SqlDbType.Time).Value = oldtimeArrive;
                updateCommand.Parameters.AddWithValue("@newTimeArrival", SqlDbType.Time).Value = newtimeArrive;


                updateCommand.ExecuteNonQuery();
                connection.Close();

                DataTable table = SelectAllTrips();
                Table.ItemsSource = table.DefaultView;
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
                SqlCommand updateCommand = new SqlCommand("Update Trip Set TicketPrice = @newPrice where TicketPrice = @oldPrice", connection);

                updateCommand.Parameters.AddWithValue("@oldPrice", SqlDbType.Float).Value = oldPrice;
                updateCommand.Parameters.AddWithValue("@newPrice", SqlDbType.Float).Value = newPrice;


                updateCommand.ExecuteNonQuery();
                connection.Close();

                DataTable table = SelectAllTrips();
                Table.ItemsSource = table.DefaultView;
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
                SqlCommand updateCommand = new SqlCommand("Update Trip Set Distance = @newDistance where Distance = @oldDistance", connection);

                updateCommand.Parameters.AddWithValue("@oldDistance", SqlDbType.Int).Value = oldDistance;
                updateCommand.Parameters.AddWithValue("@newDistance", SqlDbType.Int).Value = newDistance;


                updateCommand.ExecuteNonQuery();
                connection.Close();

                DataTable table = SelectAllTrips();
                Table.ItemsSource = table.DefaultView;
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
                Table.ItemsSource = table.DefaultView;
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
                Table.ItemsSource = table.DefaultView;
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
            SqlCommand DELETEALL = new SqlCommand("Delete from Trip; DBCC CHECKIDENT(Trip, RESEED, 0)",connection);
            DELETEALL.ExecuteNonQuery();
            connection.Close();

            DataTable table = SelectAllTrips();
            Table.ItemsSource = table.DefaultView;
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
                Table.ItemsSource = table.DefaultView;
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
                Table.ItemsSource = table.DefaultView;
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }
        }

        private void ScheduleAllSelect(object sender, RoutedEventArgs e)
        {
            if(connection.State == ConnectionState.Closed)
                connection.Open();

            SqlCommand command = new SqlCommand("Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, sch.DepartureDate, sch.SuccessTrip from Schedule sch Join Trip t  on sch.TripID = t.TripID Join CityDeparture cd on cd.CityDepartureID = t.CityDeparture Join CityArrival ca on ca.CityArrivalID = t.CityArrival",connection);

            reader = command.ExecuteReader();

            
            DataTable table = new DataTable();
            table.Load(reader);
            reader.Close();

            connection.Close();

            Table.ItemsSource = table.DefaultView;

        }

        private void Insert_Schedule(object sender, RoutedEventArgs e)
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

                if(connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand insertCommand = new SqlCommand("Insert into Schedule([TripID],[DepartureDate],[SuccessTrip]) values (@id, @dt, @st)",connection);

                insertCommand.Parameters.AddWithValue("@id", SqlDbType.Int).Value = TripID;
                insertCommand.Parameters.AddWithValue("@dt", SqlDbType.Date).Value = date;
                insertCommand.Parameters.AddWithValue("@st", SqlDbType.Bit).Value = success;

                insertCommand.ExecuteNonQuery();

                SqlCommand command = new SqlCommand("Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, sch.DepartureDate, sch.SuccessTrip from Schedule sch Join Trip t  on sch.TripID = t.TripID Join CityDeparture cd on cd.CityDepartureID = t.CityDeparture Join CityArrival ca on ca.CityArrivalID = t.CityArrival", connection);

                reader = command.ExecuteReader();


                DataTable table = new DataTable();
                table.Load(reader);
                reader.Close();

                connection.Close();

                Table.ItemsSource = table.DefaultView;


            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Delete_All(object sender, RoutedEventArgs e)
        {
            if(connection.State == ConnectionState.Closed)
                connection.Open();

            SqlCommand AllDelete = new SqlCommand(
                "delete from Sale; delete from Schedule; delete From Trip; delete From Bus; delete From Model; DBCC CHECKIDENT(Sale, RESEED, 0);DBCC CHECKIDENT(Schedule, RESEED, 0);DBCC CHECKIDENT(Trip, RESEED, 0);DBCC CHECKIDENT(Bus, RESEED, 0);DBCC CHECKIDENT(Model, Reseed, 0);",
                connection);

            AllDelete.ExecuteNonQuery();

            connection.Close();
        }

        private void TripsByCityDeparture(object sender, RoutedEventArgs e)
        {

            string cityDeparture = Interaction.InputBox("Input city of departure", "Input city");
            if (String.IsNullOrEmpty(cityDeparture))
            {
                MessageBox.Show("Empty field", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                DataTable table = SelectAllTripsByCityDeparture(cityDeparture);
                Table.ItemsSource = table.DefaultView;
            }
        }

        private void TripsByCityArrival(object sender, RoutedEventArgs e)
        {
            string cityArrival = Interaction.InputBox("Input city of arrival", "Input city");
            if (String.IsNullOrEmpty(cityArrival))
            {
                MessageBox.Show("Empty field", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                DataTable table = SelectAllTripsByCityArrival(cityArrival);
                Table.ItemsSource = table.DefaultView;
            }
        }

        public DataTable SelectAllTripsByCityArrival(string cityArrival)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            SqlCommand selectCommand = new SqlCommand("Select  cd.CityDepartureName, t.TimeDeparture, t.TimeArrival, t.Distance, t.TicketPrice, m.ModelName, b.AmountSeats, b.RegisrtyNumber from Trip t Join CityDeparture cd on cd.CityDepartureID = t.CityDeparture " +
                                                      "Join CityArrival ca on ca.CityArrivalID = t.CityArrival Join Bus b on t.BusID = b.BusID Join Model m on b.BusID = m.ModelID where ca.CityArrivalName = @cityarr", connection);
            selectCommand.Parameters.AddWithValue("@cityarr", SqlDbType.NVarChar).Value = cityArrival;
            selectCommand.ExecuteNonQuery();

            reader = selectCommand.ExecuteReader();

            DataTable table = new DataTable();

            table.Load(reader);

            reader.Close();
            connection.Close();

            return table;
        }

        private void TripsByTimeDeparture(object sender, RoutedEventArgs e)
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

                Table.ItemsSource = table.DefaultView;

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TripsByTimeArrival(object sender, RoutedEventArgs e)
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

                Table.ItemsSource = table.DefaultView;

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TripByDistance(object sender, RoutedEventArgs e)
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

                Table.ItemsSource = table.DefaultView;
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TripByPrice(object sender, RoutedEventArgs e)
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

                Table.ItemsSource = table.DefaultView;
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ScheduleWithDepartureDate(object sender, RoutedEventArgs e)
        {
            try
            {
                DepartureDate departureDate = new DepartureDate();
                DialogResult result = departureDate.ShowDialog();
                if(result == System.Windows.Forms.DialogResult.Cancel)
                    return;
                
                DateTime date = DateTime.Parse(departureDate.dateTimePicker1.Text);

                if(connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand command = new SqlCommand("Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, sch.SuccessTrip from Schedule sch Join Trip t  on sch.TripID = t.TripID Join CityDeparture cd on cd.CityDepartureID = t.CityDeparture Join CityArrival ca on ca.CityArrivalID = t.CityArrival where sch.DepartureDate = @depDate", connection);
                command.Parameters.AddWithValue("@depDate", SqlDbType.Date).Value = date;
                command.ExecuteNonQuery();

                reader = command.ExecuteReader();


                DataTable table = new DataTable();
                table.Load(reader);
                reader.Close();

                connection.Close();

                Table.ItemsSource = table.DefaultView;

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ScheduleWithSuccessfullyTrips(object sender, RoutedEventArgs e)
        {
            try
            {
                bool success = bool.Parse(Interaction.InputBox("Input successfully of trip", "Input data"));

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand command = new SqlCommand("Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, sch.DepartureDate from Schedule sch Join Trip t  on sch.TripID = t.TripID Join CityDeparture cd on cd.CityDepartureID = t.CityDeparture Join CityArrival ca on ca.CityArrivalID = t.CityArrival where sch.SuccessTrip = @success", connection);
                command.Parameters.AddWithValue("@success", SqlDbType.Bit).Value = success;
                command.ExecuteNonQuery();

                reader = command.ExecuteReader();


                DataTable table = new DataTable();
                table.Load(reader);
                reader.Close();

                connection.Close();

                Table.ItemsSource = table.DefaultView;

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteAllBuses(object sender, RoutedEventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            SqlCommand AllDelete = new SqlCommand(
                " delete From Bus; DBCC CHECKIDENT(Bus, RESEED, 0)",
                connection);

            AllDelete.ExecuteNonQuery();

            DataTable table = SelectAllBuses();

            Table.ItemsSource = table.DefaultView;
            connection.Close();
        }

        private void DeleteBusesWithSeats(object sender, RoutedEventArgs e)
        {
            try
            {
                int seats = int.Parse(Interaction.InputBox("Input a amount seats of the bus", "Input data"));

                if(connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand command = new SqlCommand("delete from Bus where AmountSeats >= @seats",connection);
                command.Parameters.AddWithValue("@seats", SqlDbType.Int).Value = seats;
                command.ExecuteNonQuery();

                DataTable table = SelectAllBuses();
                Table.ItemsSource = table.DefaultView;

                connection.Close();

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteBusesWithRegisterNumber(object sender, RoutedEventArgs e)
        {
            try
            {
                string reg = Interaction.InputBox("Input a amount seats of the bus", "Input data");

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand command = new SqlCommand("delete from Bus where RegisrtyNumber like @reg", connection);
                command.Parameters.AddWithValue("@reg", SqlDbType.NVarChar).Value = reg + "%";
                command.ExecuteNonQuery();

                DataTable table = SelectAllBuses();
                Table.ItemsSource = table.DefaultView;

                connection.Close();

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteAllSchedule(object sender, RoutedEventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            SqlCommand deleteCommand = new SqlCommand("delete from Schedule; DBCC CHECKIDENT(Schedule, RESEED,0); ",connection);
            deleteCommand.ExecuteNonQuery();

            SqlCommand command = new SqlCommand("Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, sch.DepartureDate, sch.SuccessTrip from Schedule sch Join Trip t  on sch.TripID = t.TripID Join CityDeparture cd on cd.CityDepartureID = t.CityDeparture Join CityArrival ca on ca.CityArrivalID = t.CityArrival", connection);

            reader = command.ExecuteReader();


            DataTable table = new DataTable();
            table.Load(reader);
            reader.Close();

            connection.Close();

            Table.ItemsSource = table.DefaultView;
        }

        private void DeleteSchduleByDate(object sender, RoutedEventArgs e)
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

                SqlCommand command = new SqlCommand("delete from Schedule  where DepartureDate = @depDate", connection);
                command.Parameters.AddWithValue("@depDate", SqlDbType.Date).Value = date;
                command.ExecuteNonQuery();

                reader = command.ExecuteReader();


                DataTable table = new DataTable();
                table.Load(reader);
                reader.Close();

                connection.Close();

                Table.ItemsSource = table.DefaultView;

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteScheduleBySuccess(object sender, RoutedEventArgs e)
        {
            try
            {
                bool success = bool.Parse(Interaction.InputBox("Input successfully of trip", "Input data"));

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand command = new SqlCommand("delete from Schedule 0 where SuccessTrip = @success", connection);
                command.Parameters.AddWithValue("@success", SqlDbType.Bit).Value = success;
                command.ExecuteNonQuery();

                reader = command.ExecuteReader();


                DataTable table = new DataTable();
                table.Load(reader);
                reader.Close();

                connection.Close();

                Table.ItemsSource = table.DefaultView;

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateDepartureDate(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateDepartureDateForm form = new UpdateDepartureDateForm();

                DialogResult result = form.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.Cancel)
                    return;

                DateTime oldDate = DateTime.Parse(form.dateTimePicker1.Text);
                DateTime newDate = DateTime.Parse(form.dateTimePicker2.Text);

                SqlCommand updateCommand = new SqlCommand("update Schedule set DepartureDate = @new where DepartureDate = @old",connection);
                updateCommand.Parameters.AddWithValue("@new", SqlDbType.Date).Value = newDate;
                updateCommand.Parameters.AddWithValue("@old", SqlDbType.Date).Value = oldDate;

                SqlCommand command = new SqlCommand("Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, sch.DepartureDate, sch.SuccessTrip from Schedule sch Join Trip t  on sch.TripID = t.TripID Join CityDeparture cd on cd.CityDepartureID = t.CityDeparture Join CityArrival ca on ca.CityArrivalID = t.CityArrival", connection);

                reader = command.ExecuteReader();


                DataTable table = new DataTable();
                table.Load(reader);
                reader.Close();

                connection.Close();

                Table.ItemsSource = table.DefaultView;



            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateSuccess(object sender, RoutedEventArgs e)
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

                Table.ItemsSource = table.DefaultView;
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SelectAllSale(object sender, RoutedEventArgs e)
        {
           if(connection.State == ConnectionState.Closed)
            connection.Open();

            SqlCommand selectCommand = new SqlCommand("Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, Schedule.DepartureDate, s.DateOfSaling, s.AmountTickets, s.PriceSum from Sale s join  Schedule on s.ScheduleID = Schedule.ScheduleID join Trip t on Schedule.TripID = t.TripID join CityDeparture cd on cd.CityDepartureID = t.CityDeparture Join CityArrival ca on ca.CityArrivalID = t.CityArrival ", connection);

            reader = selectCommand.ExecuteReader();

            DataTable table = new DataTable();
            table.Load(reader);

            reader.Close();
            connection.Close();

            Table.ItemsSource = table.DefaultView;
        }

        private void SelectSaleByAmount(object sender, RoutedEventArgs e)
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

                DataTable table = new DataTable();
                table.Load(reader);

                reader.Close();
                connection.Close();

                Table.ItemsSource = table.DefaultView;

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SelectSaleBySum(object sender, RoutedEventArgs e)
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

                DataTable table = new DataTable();
                table.Load(reader);

                reader.Close();
                connection.Close();

                Table.ItemsSource = table.DefaultView;

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SelectSaleByDate(object sender, RoutedEventArgs e)
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

                DataTable table = new DataTable();
                table.Load(reader);

                reader.Close();
                connection.Close();

                Table.ItemsSource = table.DefaultView;

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InsertSale(object sender, RoutedEventArgs e)
        {
            try
            {
                 SaleInput sale = new SaleInput();
                DialogResult result = sale.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.Cancel)
                    return;

                int ScheduleID = int.Parse(sale.textBox1.Text);
                DateTime date = DateTime.Parse(sale.dateTimePicker1.Text);
                int amountTickets = int.Parse(sale.textBox2.Text);
                float sum = float.Parse(sale.textBox3.Text);

               if(connection.State == ConnectionState.Closed)
                    connection.Open();

               SqlCommand insert = new SqlCommand("Insert into Sale(ScheduleID, DateOfSaling,AmountTickets,PriceSum) values (@sch,@date,@amount,@price",connection);
                insert.Parameters.AddWithValue("@sch", SqlDbType.Int).Value = ScheduleID;
                insert.Parameters.AddWithValue("@date", SqlDbType.Date).Value = date;
                insert.Parameters.AddWithValue("@amount", SqlDbType.Int).Value = amountTickets;
                insert.Parameters.AddWithValue("@price", SqlDbType.Float).Value = sum;

                insert.ExecuteNonQuery();

                SqlCommand selectCommand = new SqlCommand("Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, Schedule.DepartureDate, s.DateOfSaling, s.AmountTickets, s.PriceSum from Sale s join  Schedule on s.ScheduleID = Schedule.ScheduleID join Trip t on Schedule.TripID = t.TripID join CityDeparture cd on cd.CityDepartureID = t.CityDeparture Join CityArrival ca on ca.CityArrivalID = t.CityArrival ", connection);

                reader = selectCommand.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(reader);

                reader.Close();
                connection.Close();

                Table.ItemsSource = table.DefaultView;


            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateDate(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateDateSaling form = new UpdateDateSaling();

                DialogResult result = form.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.Cancel)
                    return;

                DateTime oldDate = DateTime.Parse(form.dateTimePicker1.Text);
                DateTime newDate = DateTime.Parse(form.dateTimePicker2.Text);

                SqlCommand updateCommand = new SqlCommand("update Sale set DateOfSalin = @new where DepartureDate = @old", connection);
                updateCommand.Parameters.AddWithValue("@new", SqlDbType.Date).Value = newDate;
                updateCommand.Parameters.AddWithValue("@old", SqlDbType.Date).Value = oldDate;

                SqlCommand command = new SqlCommand("Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, Schedule.DepartureDate, s.DateOfSaling, s.AmountTickets, s.PriceSum from Sale s join  Schedule on s.ScheduleID = Schedule.ScheduleID join Trip t on Schedule.TripID = t.TripID join CityDeparture cd on cd.CityDepartureID = t.CityDeparture Join CityArrival ca on ca.CityArrivalID = t.CityArrival ", connection);

                reader = command.ExecuteReader();


                DataTable table = new DataTable();
                table.Load(reader);
                reader.Close();

                connection.Close();

                Table.ItemsSource = table.DefaultView;



            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateAmount(object sender, RoutedEventArgs e)
        {
            try
            {
                int oldAmount = int.Parse(Interaction.InputBox("Input old amount of ticket", "input data"));
                int newAmount = int.Parse(Interaction.InputBox("Input new amount of ticket", "input data"));

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand updateCommand = new SqlCommand("Update Sale Set AmountTickets = @new where AmountTickets = @old", connection);

                updateCommand.Parameters.AddWithValue("@old", SqlDbType.Int).Value = oldAmount;
                updateCommand.Parameters.AddWithValue("@new", SqlDbType.Int).Value = newAmount;


                updateCommand.ExecuteNonQuery();

                SqlCommand selectCommand = new SqlCommand("Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, Schedule.DepartureDate, s.DateOfSaling, s.AmountTickets, s.PriceSum from Sale s join  Schedule on s.ScheduleID = Schedule.ScheduleID join Trip t on Schedule.TripID = t.TripID join CityDeparture cd on cd.CityDepartureID = t.CityDeparture Join CityArrival ca on ca.CityArrivalID = t.CityArrival ", connection);

                reader = selectCommand.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(reader);

                reader.Close();
                connection.Close();

                Table.ItemsSource = table.DefaultView;

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateSum(object sender, RoutedEventArgs e)
        {
             try
            {
                float oldsum = int.Parse(Interaction.InputBox("Input old sum of price", "input data"));
                float newsum = int.Parse(Interaction.InputBox("Input new sum of price", "input data"));

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand updateCommand = new SqlCommand("Update Sale Set PriceSum = @new where PriceSum = @old", connection);

                updateCommand.Parameters.AddWithValue("@old", SqlDbType.Float).Value = oldsum;
                updateCommand.Parameters.AddWithValue("@new", SqlDbType.Float).Value = newsum;


                updateCommand.ExecuteNonQuery();

                SqlCommand selectCommand = new SqlCommand("Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, Schedule.DepartureDate, s.DateOfSaling, s.AmountTickets, s.PriceSum from Sale s join  Schedule on s.ScheduleID = Schedule.ScheduleID join Trip t on Schedule.TripID = t.TripID join CityDeparture cd on cd.CityDepartureID = t.CityDeparture Join CityArrival ca on ca.CityArrivalID = t.CityArrival ", connection);

                reader = selectCommand.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(reader);

                reader.Close();
                connection.Close();

                Table.ItemsSource = table.DefaultView;

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteAll(object sender, RoutedEventArgs e)
        {

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            SqlCommand deleteCommand = new SqlCommand("delete from Sale; DBCC CHECKIDENT(Sale, RESEED,0); ", connection);
            deleteCommand.ExecuteNonQuery();

            SqlCommand selectCommand = new SqlCommand("Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, Schedule.DepartureDate, s.DateOfSaling, s.AmountTickets, s.PriceSum from Sale s join  Schedule on s.ScheduleID = Schedule.ScheduleID join Trip t on Schedule.TripID = t.TripID join CityDeparture cd on cd.CityDepartureID = t.CityDeparture Join CityArrival ca on ca.CityArrivalID = t.CityArrival ", connection);

            reader = selectCommand.ExecuteReader();


            DataTable table = new DataTable();
            table.Load(reader);
            reader.Close();

            connection.Close();

            Table.ItemsSource = table.DefaultView;
        }

        private void DeleteAmount(object sender, RoutedEventArgs e)
        {
            try
            {
                int amount = int.Parse(Interaction.InputBox("Input amount of tickets", "Input data"));

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand deleteCommand = new SqlCommand("delete from Sale where AmountTickets >= @amount ", connection);
                deleteCommand.Parameters.AddWithValue("@amount", SqlDbType.Int).Value = amount;
                deleteCommand.ExecuteNonQuery();

                SqlCommand selectCommand = new SqlCommand("Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, Schedule.DepartureDate, s.DateOfSaling, s.AmountTickets, s.PriceSum from Sale s join  Schedule on s.ScheduleID = Schedule.ScheduleID join Trip t on Schedule.TripID = t.TripID join CityDeparture cd on cd.CityDepartureID = t.CityDeparture Join CityArrival ca on ca.CityArrivalID = t.CityArrival ", connection);

                reader = selectCommand.ExecuteReader();


                DataTable table = new DataTable();
                table.Load(reader);
                reader.Close();

                connection.Close();

                Table.ItemsSource = table.DefaultView;

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteSum(object sender, RoutedEventArgs e)
        {
            try
            {
                float suma = float.Parse(Interaction.InputBox("Input sum of prices", "Input data"));

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand deleteCommand = new SqlCommand("delete from Sale where PriceSum >= @price ", connection);
                deleteCommand.Parameters.AddWithValue("@price", SqlDbType.Float).Value = suma;
                deleteCommand.ExecuteNonQuery();

                SqlCommand selectCommand = new SqlCommand("Select cd.CityDepartureName, ca.CityArrivalName, t.TimeDeparture, t.TimeArrival, Schedule.DepartureDate, s.DateOfSaling, s.AmountTickets, s.PriceSum from Sale s join  Schedule on s.ScheduleID = Schedule.ScheduleID join Trip t on Schedule.TripID = t.TripID join CityDeparture cd on cd.CityDepartureID = t.CityDeparture Join CityArrival ca on ca.CityArrivalID = t.CityArrival ", connection);

                reader = selectCommand.ExecuteReader();


                DataTable table = new DataTable();
                table.Load(reader);
                reader.Close();

                connection.Close();

                Table.ItemsSource = table.DefaultView;

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteByDate(object sender, RoutedEventArgs e)
        {
            try
            {
                SaleDate saleDate = new SaleDate();
                DialogResult result = saleDate.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.Cancel)
                    return;

                DateTime date = DateTime.Parse(saleDate.dateTimePicker1.Text);

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand command = new SqlCommand("delete from Schedule  where sch.DepartureDate = @depDate", connection);
                command.Parameters.AddWithValue("@depDate", SqlDbType.Date).Value = date;
                command.ExecuteNonQuery();

                reader = command.ExecuteReader();


                DataTable table = new DataTable();
                table.Load(reader);
                reader.Close();

                connection.Close();

                Table.ItemsSource = table.DefaultView;

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RegUpdate(object sender, RoutedEventArgs e)
        {
            
               string oldReg = Interaction.InputBox("Input old registry number", "Input data");
            string newReg = Interaction.InputBox("Input new registry number", "Input data");

            if (String.IsNullOrEmpty(oldReg) || String.IsNullOrEmpty(newReg))
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand updateCommand = new SqlCommand("Update Bus Set RegisrtyNumber = @new where RegisrtyNumber = @old", connection);

                updateCommand.Parameters.AddWithValue("@old", SqlDbType.NVarChar).Value = oldReg;
                updateCommand.Parameters.AddWithValue("@new", SqlDbType.NVarChar).Value = newReg;


                updateCommand.ExecuteNonQuery();
                connection.Close();

                DataTable table = SelectAllBuses();
                Table.ItemsSource = table.DefaultView;
            }

            

        }
    }
}
