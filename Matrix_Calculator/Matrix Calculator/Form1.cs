using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matrix_Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private MatrixCalculator matrixCalculator1;
        private MatrixCalculator matrixCalculator2;
        

        private int rows1, cols1;
        private int rows2, cols2;

        private void button3_Click(object sender, EventArgs e)
        {
           
            try
            {
               string selected_operation = comboBox1.Items[comboBox1.SelectedIndex].ToString();
               

                if (selected_operation.Equals("+"))
                {
                    if (matrixCalculator1.Rows == matrixCalculator2.Rows &&
                        matrixCalculator1.Columns == matrixCalculator2.Columns)
                    {
                        MatrixCalculator matrixCalculator3 = matrixCalculator1 + matrixCalculator2;

                        dataGridView3.RowCount = matrixCalculator3.Rows;
                        dataGridView3.ColumnCount = matrixCalculator3.Columns;

                        for (int i = 0; i < dataGridView3.RowCount; i++)
                        {
                            for (int j = 0; j < dataGridView3.ColumnCount; j++)
                            {
                                dataGridView3.Rows[i].Cells[j].Value = matrixCalculator3[i, j];
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Not equal amount of rows/cols of two matrices","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

               else if (selected_operation.Equals("-"))
                {
                    if (matrixCalculator1.Rows == matrixCalculator2.Rows &&
                        matrixCalculator1.Columns == matrixCalculator2.Columns)
                    {
                        MatrixCalculator matrixCalculator3 = matrixCalculator1 - matrixCalculator2;

                        dataGridView3.RowCount = matrixCalculator3.Rows;
                        dataGridView3.ColumnCount = matrixCalculator3.Columns;

                        for (int i = 0; i < dataGridView3.RowCount; i++)
                        {
                            for (int j = 0; j < dataGridView3.ColumnCount; j++)
                            {
                                dataGridView3.Rows[i].Cells[j].Value = matrixCalculator3[i, j];
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Not equal amount of rows/cols of two matrices", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (matrixCalculator1.Columns == matrixCalculator2.Rows)
                    {
                        MatrixCalculator matrixCalculator3 = matrixCalculator1 * matrixCalculator2;

                        dataGridView3.RowCount = matrixCalculator3.Rows;
                        dataGridView3.ColumnCount = matrixCalculator3.Columns;

                        for (int i = 0; i < dataGridView3.RowCount; i++)
                        {
                            for (int j = 0; j < dataGridView3.ColumnCount; j++)
                            {
                                dataGridView3.Rows[i].Cells[j].Value = matrixCalculator3[i, j];
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Not equal amount of rows and cols of two matrices", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (NullReferenceException exception)
            {
                MessageBox.Show("Matrices did not initialized", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                MessageBox.Show("The operation did not choose", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            try
            {
                rows2 = int.Parse(RowsSecond.Text);
                cols2 = int.Parse(ColumnSecond.Text);

                matrixCalculator2 = new MatrixCalculator(rows2, cols2);

                dataGridView2.RowCount = matrixCalculator2.Rows;
                dataGridView2.ColumnCount = matrixCalculator2.Columns;

                matrixCalculator2.FullMatrixWithValues();

                for (int i = 0; i < dataGridView2.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView2.ColumnCount; j++)
                    {
                        dataGridView2.Rows[i].Cells[j].Value = matrixCalculator2[i, j];
                    }
                }
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Incorrect input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            try
            {
                rows1 = int.Parse(Rows1.Text);
                cols1 = int.Parse(Columns1.Text);

                matrixCalculator1 = new MatrixCalculator(rows1, cols1);

                dataGridView1.RowCount = matrixCalculator1.Rows;
                dataGridView1.ColumnCount = matrixCalculator1.Columns;

                matrixCalculator1.FullMatrixWithValues();

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = matrixCalculator1[i, j];
                    }
                }
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Incorrect input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
