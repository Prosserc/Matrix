using System;
using System.Diagnostics;

namespace Matrix.Tests.Timing
{
    class MatrixTranpositionTimeTest
    {
        /// <summary>Timed test of transpose method.</summary>
        /// <param name="x">matrix - takes maxtrix to be transposed</param>
        public static void Transpose(Matrix x)
        {
            // time the multiplication
            var watch = Stopwatch.StartNew();
            x.T();
            var ms = watch.ElapsedMilliseconds;
            watch.Stop();

            // report results
            Debug.WriteLine(string.Format("{0}*** Transposition took {1} ms on {2} x {3} Matrix ***", Environment.NewLine, ms, x.Rows, x.Cols));
        }

        /// <summary>Alternative constructor which generates the matrix to be run through the timed test and initalises it with random numbers</summary>
        /// <param name="rows">int - number of rows in new matrix</param>
        /// <param name="cols">int - number of columns in new matrix</param>
        public static void Transpose(int rows, int cols)
        {
            Transpose(new Matrix(rows, cols, initialiseWithRandomValues: true));
        }
    }
}
