using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix_Calculator
{
    public class MatrixCalculator
    {
        private int[,] matrix;
        private int rows, columns;

        public MatrixCalculator(int n, int m)
        {
            this.rows = n;
            this.columns = m;

            matrix = new int[rows, columns];
        }

        public int Rows
        {
            get { return rows; }
        }

        public int Columns
        {
            get { return columns; }
        }

        public void FullMatrixWithValues()
        {
            Random random = new Random();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = random.Next(1, 1001);
                }
            }
        }

        public int this[int i, int j]
        {
            get { return matrix[i, j]; }
            set { matrix[i, j] = value; }
        }

        public static MatrixCalculator operator +(MatrixCalculator a, MatrixCalculator b)
        {
           MatrixCalculator c = new MatrixCalculator(a.rows, a.columns);

            for (int i = 0; i < c.Rows; i++)
            {
                for (int j = 0; j < c.Columns; j++)
                {
                    c[i, j] = a[i, j] + b[i, j];
                }
            }

            return c;

        }
        public static MatrixCalculator operator -(MatrixCalculator a, MatrixCalculator b)
        {
            MatrixCalculator c = new MatrixCalculator(a.rows, a.columns);

            for (int i = 0; i < c.Rows; i++)
            {
                for (int j = 0; j < c.Columns; j++)
                {
                    c[i, j] = a[i, j] - b[i, j];
                }
            }

            return c;

        }

        public static MatrixCalculator operator *(MatrixCalculator a, MatrixCalculator b)
        {
            MatrixCalculator c = new MatrixCalculator(a.rows, b.columns);
            int l = a.rows;

            for (int i = 0; i < c.Rows; i++)
            {
                for (int j = 0; j < c.Columns; j++)
                {
                    int res = 0;
                    for (int x = 0; x < l; x++)
                    {
                        res += a[i, x] * b[x, j];
                    }

                    c[i, j] = res;
                   
                }
            }

            return c;
        }
    }
}
