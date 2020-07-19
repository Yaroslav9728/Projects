using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private double operand1;
        private double operand2;
        private string operation;


        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" && textBox1.Text != null)
            {
                textBox1.Text = "5";
            }
            else
            {
                textBox1.Text = textBox1.Text + "5";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" && textBox1.Text != null)
            {
                textBox1.Text = "1";
            }
            else
            {
                textBox1.Text = textBox1.Text + "1";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" && textBox1.Text != null)
            {
                textBox1.Text = "2";
            }
            else
            {
                textBox1.Text = textBox1.Text + "2";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" && textBox1.Text != null)
            {
                textBox1.Text = "3";
            }
            else
            {
                textBox1.Text = textBox1.Text + "3";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" && textBox1.Text != null)
            {
                textBox1.Text = "4";
            }
            else
            {
                textBox1.Text = textBox1.Text + "4";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" && textBox1.Text != null)
            {
                textBox1.Text = "6";
            }
            else
            {
                textBox1.Text = textBox1.Text + "6";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" && textBox1.Text != null)
            {
                textBox1.Text = "7";
            }
            else
            {
                textBox1.Text = textBox1.Text + "7";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" && textBox1.Text != null)
            {
                textBox1.Text = "8";
            }
            else
            {
                textBox1.Text = textBox1.Text + "8";
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" && textBox1.Text != null)
            {
                textBox1.Text = "9";
            }
            else
            {
                textBox1.Text = textBox1.Text + "9";
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "0";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + ".";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                operand1 = Convert.ToDouble(textBox1.Text);
                textBox1.Text = "0";

                operation = "+";
            }
            catch (FormatException exception)
            {
                textBox1.Text = "Not number!";
            }


        }

        private void button14_Click(object sender, EventArgs e)
        {

            try
            {
                operand1 = Convert.ToDouble(textBox1.Text);
                textBox1.Text = "0";

                operation = "-";
            }
            catch (FormatException exception)
            {
                textBox1.Text = "Not number!";
            }


        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                operand1 = Convert.ToDouble(textBox1.Text);
                textBox1.Text = "0";

                operation = "*";
            }
            catch (FormatException exception)
            {
                textBox1.Text = "Not number!";
            }


        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                operand1 = Convert.ToDouble(textBox1.Text);
                textBox1.Text = "0";

                operation = "/";
            }
            catch (FormatException exception)
            {
                textBox1.Text = "Not number!";
            }


        }

        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                operand2 = Convert.ToDouble(textBox1.Text);

                switch (operation)
                {
                    case "+":
                        var res = operand1 + operand2;
                        textBox1.Text = res.ToString();
                        break;

                    case "-":
                        var res2 = operand1 - operand2;
                        textBox1.Text = res2.ToString();
                        break;

                    case "*":
                        var res3 = operand1 * operand2;
                        textBox1.Text = res3.ToString();
                        break;

                    case "/":

                        if (Math.Abs(operand2) < 0.001)
                        {
                            textBox1.Text = "Can not divide on zero!";
                        }
                        else
                        {
                            var res4 = operand1 / operand2;
                            textBox1.Text = res4.ToString();
                        }

                        break;

                    case "x^y":
                        var res5 = Math.Pow(operand1, operand2);
                        textBox1.Text = res5.ToString();
                        break;

                }
            }
            catch (FormatException)
            {
                textBox1.Text = "Please input only numbers!";
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                operand1 = Convert.ToDouble(textBox1.Text);

                if (operand1 >= 0)
                {
                    textBox1.Text = Math.Sqrt(operand1).ToString();
                }
                else
                {
                    textBox1.Text = "Number should be equal/greater zero!";
                }
            }
            catch (FormatException)
            {
                textBox1.Text = "Not numbers!";
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            try
            {
                operand1 = Convert.ToDouble(textBox1.Text);

                if (operand1 >= 0)
                {
                    textBox1.Text = Math.Log10(operand1).ToString();
                }
                else
                {
                    textBox1.Text = "Operand should be greater/equal zero!";
                }
            }
            catch (FormatException)
            {
                textBox1.Text = "Not number!";
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                operand1 = Convert.ToDouble(textBox1.Text);
                textBox1.Text = "0";

                operation = "x^y";
            }
            catch (FormatException exception)
            {
                textBox1.Text = "Not number!";
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                operand1 = Convert.ToDouble(textBox1.Text);

                textBox1.Text = Math.Exp(operand1).ToString();
            }
            catch (FormatException)
            {
                textBox1.Text = "Not number!";
            }
        }
    }
}
