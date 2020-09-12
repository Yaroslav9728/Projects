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

namespace Quadric_Equatation
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private QuadricEquatation equatation;
        private Tuple<double,double,double> results;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Input_BButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double a = double.Parse(ABox.Text);
                double b = double.Parse(BBox.Text);
                double c = double.Parse(CBox.Text);

                equatation = new QuadricEquatation(a, b, c);

            }
            catch (FormatException exception)
            {
                MessageBox.Show("Uncorrect input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Calculate_Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                results = equatation.CalculateResults();



                if (double.IsNegativeInfinity(results.Item2) && double.IsNegativeInfinity(results.Item3))
                {
                    MessageBox.Show("Disriminant = " + results.Item1 + "\n" + "Not solutions", "Message");
                }
                else
                {
                    double TOLERANCE = 0.0000001;
                    if (Math.Abs(results.Item3) < TOLERANCE)
                    {
                        MessageBox.Show("Disriminant = " + results.Item1 + "\n" + "Solution = " + results.Item2, "Message");
                    }
                    else
                    {
                        MessageBox.Show(
                            "Disriminant = " + results.Item1 + "\n" + "Solution 1 = " + results.Item2 + "\n" + "Solution 2 = " +
                            results.Item3, "Message");
                    }
                }
            }
            catch (NullReferenceException exception)
            {
                MessageBox.Show("Not setted values", "Message");
            }
        }

        
    }
}