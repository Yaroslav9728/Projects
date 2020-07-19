using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Graphs_of_basic_functios
{
    public partial class Form1 : Form
    {
        private double x1, x2;
        private int step;
        private string function;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Axis x = new Axis();
            Axis y = new Axis();
            chart1.Series[0].Points.Clear();

            chart1.Refresh();

            chart1.Dock = DockStyle.Right;
           
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;

            try
            {
                x1 = double.Parse(textBox1.Text);
                x2 = double.Parse(textBox2.Text);
                step = int.Parse(numericUpDown1.Text);

                function = comboBox1.Items[comboBox1.SelectedIndex].ToString();

                if (function.Equals("y = x"))
                {
                    x.Title = "x";
                    y.Title = "y = x";
                    chart1.ChartAreas[0].AxisX = x;
                    chart1.ChartAreas[0].AxisY = y;
                    for (double i = x1; i <= x2; i++)
                    {
                        chart1.Series[0].Points.AddXY(i, i + step);
                    }
                }
                else if (function.Equals("y = x²"))
                {
                    x.Title = "x";
                    y.Title = "y = x²";
                    chart1.ChartAreas[0].AxisX = x;
                    chart1.ChartAreas[0].AxisY = y;
                    for (double i = x1; i <= x2; i++)
                    {
                        chart1.Series[0].Points.AddXY(i, Math.Pow(i, 2) + step);
                    }
                }
                else if (function.Equals("y = x³"))
                {
                    x.Title = "x";
                    y.Title = "y = x³";
                    chart1.ChartAreas[0].AxisX = x;
                    chart1.ChartAreas[0].AxisY = y;
                    for (double i = x1; i <= x2; i++)
                    {
                        chart1.Series[0].Points.AddXY(i, Math.Pow(i, 3) + step);
                    }
                }
                else if (function.Equals("y = |x|"))
                {
                    x.Title = "x";
                    y.Title = "y = |x|";
                    chart1.ChartAreas[0].AxisX = x;
                    chart1.ChartAreas[0].AxisY = y;
                    for (double i = x1; i <= x2; i++)
                    {
                        chart1.Series[0].Points.AddXY(i, Math.Abs(i) + step);
                    }
                }
                else if (function.Equals("y = √x"))
                {
                    x.Title = "x";
                    y.Title = "y = √x";
                    chart1.ChartAreas[0].AxisX = x;
                    chart1.ChartAreas[0].AxisY = y;
                    for (double i = x1; i <= x2; i++)
                    {
                        chart1.Series[0].Points.AddXY(i, Math.Sqrt(i) + step);
                    }
                }


            }
            catch (FormatException exception)
            {
                MessageBox.Show("Incorrect input");
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Please choose a function");
            }

            
        }
    }
}
