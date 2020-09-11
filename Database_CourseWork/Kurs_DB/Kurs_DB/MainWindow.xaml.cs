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

namespace Kurs_DB
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AdminBox_OnChecked(object sender, RoutedEventArgs e)
        {
            if (AdminBox.IsChecked == true)
            {
                DyspetcherBox.IsEnabled = false;
                CashBox.IsEnabled = false;
            }
        }

        private void AdminBox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            if (AdminBox.IsChecked == false)
            {
                DyspetcherBox.IsEnabled = true;
                CashBox.IsEnabled = true;
            }
        }

        private void DyspetcherBox_OnChecked(object sender, RoutedEventArgs e)
        {
            if (DyspetcherBox.IsChecked == true)
            {
                AdminBox.IsEnabled = false;
                CashBox.IsEnabled = false;
            }
        }

        private void DyspetcherBox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            if (DyspetcherBox.IsChecked == false)
            {
                AdminBox.IsEnabled = true;
                CashBox.IsEnabled = true;
            }
        }

        private void CashBox_OnChecked(object sender, RoutedEventArgs e)
        {
            if (CashBox.IsChecked == true)
            {
                AdminBox.IsEnabled = false;
                DyspetcherBox.IsEnabled = false;
            }
        }

        private void CashBox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            if (CashBox.IsChecked == false)
            {
                AdminBox.IsEnabled = true;
                DyspetcherBox.IsEnabled = true;
            }
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            if (AdminBox.IsChecked == true)
            {
                string login = "admin";
                string password = "admin";

                string inLogin = textBox.Text;
                string inPassword = passwordBox.Password;

                if (String.IsNullOrEmpty(inLogin) || String.IsNullOrEmpty(inPassword))
                {
                    MessageBox.Show("Empty Fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else 
                {
                    if (String.Equals(inLogin, login) && String.Equals(inPassword, password))
                    {
                        AdminWindow window = new AdminWindow();
                        window.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Wrong login/password", "Error", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                }
            }
            if (DyspetcherBox.IsChecked == true)
            {
                string login = "control";
                string password = "control";

                string inLogin = textBox.Text;
                string inPassword = passwordBox.Password;

                if (String.IsNullOrEmpty(inLogin) || String.IsNullOrEmpty(inPassword))
                {
                    MessageBox.Show("Empty Fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (String.Equals(inLogin, login) && String.Equals(inPassword, password))
                    {
                        DyspetcherWindow dyspetcher = new DyspetcherWindow();
                        dyspetcher.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Wrong login/password", "Error", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                }
            }
            if (CashBox.IsChecked == true)
            {
                string login = "profit";
                string password = "profit";

                string inLogin = textBox.Text;
                string inPassword = passwordBox.Password;

                if (String.IsNullOrEmpty(inLogin) || String.IsNullOrEmpty(inPassword))
                {
                    MessageBox.Show("Empty Fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (String.Equals(inLogin, login) && String.Equals(inPassword, password))
                    {
                       CashyrWindow window = new CashyrWindow();
                        window.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Wrong login/password", "Error", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                }
            }
        }
    }
}
