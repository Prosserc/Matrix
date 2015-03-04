using System;
using System.Diagnostics;

namespace Matrix.Tests.Timing
{
    static class MatrixSubtractionTimeTest
    {
        // shorthand for constructor with square Matrices
        public static void Subtract(int squareMatrixDimentions)
        {
            Subtract(squareMatrixDimentions, squareMatrixDimentions);
        }

        public static void Subtract(int rows, int cols)
        {
            // prep data
            var m1 = new Matrix(rows, cols, initialiseWithRandomValues: true);
            var m2 = new Matrix(rows, cols, initialiseWithRandomValues: true);

            // time the multiplication
            var watch = Stopwatch.StartNew();
            var m3 = m1 - m2;
            var ms = watch.ElapsedMilliseconds;
            watch.Stop();

            // report results
            Debug.WriteLine(string.Format("*** Subtraction took {0} ms on {1} x {2} Matrices ***", ms, rows, cols));
        }
    }
}
