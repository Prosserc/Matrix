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
            var a = new Matrix( new double[,] { { 4,  5, -5}, 
                                                {-1, -4, -2}, 
                                                {-3,  1,  5}, 
                                                { 2,  1,  4} } );

            var b = new Matrix( new double[,] { { -4,  4, -1,  3, -4 }, 
                                                { -1, -5, -5,  4, -5 }, 
                                                { -1, -1,  0, -1,  1 } }) ;

            Debug.WriteLine("Matrix a set to: " + Environment.NewLine + a);
            Debug.WriteLine("Matrix b set to: " + Environment.NewLine + b);

            // * opperator has override to get dot product
            var c = a*b;
            Debug.WriteLine("Result of dot product (Matrix c) set to: " + Environment.NewLine + c);

            // allows indexing to get cell e.g.
            Debug.WriteLine("a[0,0] set to: " + a[0,0]);

            // test equality operator
            var x = new Matrix();
            var y = new Matrix();
            Debug.WriteLine("Test equality with empty matricies, both empty, equality should show as true: " + (x == y));
            y.PopulateMatrix(10, 10, initialiseWithRandomValues: true);
            Debug.WriteLine("y initialised to : " + Environment.NewLine + y);
            Debug.WriteLine("Test equality with one matrix, equality should show as false: " + (x == y));

            // test larger multiplication
            var testMatrixMultiplicationWith100RowsAndCols = new MatrixMultiplicationTest(100);
            var testMatrixMultiplicationWith250RowsAndCols = new MatrixMultiplicationTest(250);
            //var testMatrixMultiplicationWith2000RowsAndCols = new MatrixMultiplicationTest(2000);
            //var testMatrixMultiplicationWith10000RowsAnd200ColsBy200RowsAnd10000Cols =
            //    new MatrixMultiplicationTest(10000, 200, 200, 10000);
        }
    }
}
