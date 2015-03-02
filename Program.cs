using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrix.Tests;

namespace Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new Matrix(4, 3);
            a[0] = new double[] { 4,  5, -5};
            a[1] = new double[] {-1, -4, -2};
            a[2] = new double[] {-3,  1,  5}; 
            a[3] = new double[] { 2,  1,  4};

            var b = new Matrix(3, 5);
            b[0] = new double[] {-4,  4, -1,  3, -4};
            b[1] = new double[] {-1, -5, -5,  4, -5};
            b[2] = new double[] {-1, -1,  0, -1,  1};

            Debug.WriteLine("Matrix a set to: " + Environment.NewLine + a);
            Debug.WriteLine("Matrix b set to: " + Environment.NewLine + b);

            // * opperator has override to get dot product
            var c = a*b;
            Debug.WriteLine("Result of dot product (Matrix c) set to: " + Environment.NewLine + c);

            // allows indexing to get cell e.g.
            Debug.WriteLine("a[0][0] set to: " + a[0][0]);

            // test larger multiplication
            new MatrixMultiplicationTest(100);
            new MatrixMultiplicationTest(500);
            //new MatrixMultiplicationTest(10000, 200, 200, 10000); // test none square array
        }
    }
}
