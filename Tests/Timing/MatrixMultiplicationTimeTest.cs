using System;
using System.Diagnostics;

namespace Matrix.Tests.Timing
{
    static class MatrixMultiplicationTimeTest
    {
        // shorthand for constructor with square Matrices
        public static void Multiply(int squareMatrixDimentions)
        {
            Multiply(squareMatrixDimentions, squareMatrixDimentions, squareMatrixDimentions, squareMatrixDimentions);
        }

        public static void Multiply(int m1Rows, int m1Cols, int m2Rows, int m2Cols)
        {
            Debug.Write(Environment.NewLine);

            // prep data
            var m1 = new Matrix(m1Rows, m1Cols, initialiseWithRandomValues: true);
            var m2 = new Matrix(m2Rows, m2Cols, initialiseWithRandomValues: true);

            // time the multiplication
            var watch = Stopwatch.StartNew();
            var m3 = m1*m2;
            var ms = watch.ElapsedMilliseconds;
            watch.Stop();

            // report results
            var msg = string.Format("Multiplication took {0} ms on {1} x {2} and {3} x {4} Matrices. Result:", ms, m1Rows, m1Cols, m2Rows, m2Cols);
            Debug.WriteLine("{0}{1}{2}{3}{4}", msg, Environment.NewLine, new string('-', msg.Length-1), Environment.NewLine, m3);
        }
    }
}
