using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProbeAI
{
    public class Matrix
    {
        public int rows, cols; //cols - линия
        public double[,] matrix;
        Random r = new Random();
        public Matrix(int r, int c)
        {
            rows = r;
            cols = c;
            matrix = new double[rows, cols];
        }

        public void randomize()
        {
            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < cols; j++)
                {
                    matrix[i,j] = Convert.ToSingle(r.NextDouble() * 2 - 1);
                }
            }
        }
        public Matrix singleColumnMatrixFromArray(double[] arr)
        {
            Matrix n = new Matrix(arr.Length, 1);
            for (int i = 0; i < arr.Length; i++)
            {
                n.matrix[i,0] = arr[i];
            }
            return n;
        }
        public Matrix addBias()
        {
            Matrix n = new Matrix(rows + 1, 1);
            for (int i = 0; i < rows; i++)
            {
                n.matrix[i,0] = matrix[i,0];
            }
            n.matrix[rows,0] = 1;
            return n;
        }
        public Matrix activate()
        {
            Matrix n = new Matrix(rows, cols);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    n.matrix[i,j] = relu(matrix[i,j]);
                }
            }
            return n;
        }

        double relu(double x)
        {
            return Math.Max(0, x);
        }
        public Matrix dot(Matrix n)
        {
            Matrix result = new Matrix(rows, n.cols);

            if (cols == n.rows)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < n.cols; j++)
                    {
                        double sum = 0;
                        for (int k = 0; k < cols; k++)
                        {
                            sum += matrix[i,k] * n.matrix[k,j];
                        }
                        result.matrix[i,j] = sum;
                    }
                }
            }
            return result;
        }
        public double[] toArray()
        {
            double[] arr = new double[rows * cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    arr[j + i * cols] = matrix[i,j];
                }
            }
            return arr;
        }
    }
}
