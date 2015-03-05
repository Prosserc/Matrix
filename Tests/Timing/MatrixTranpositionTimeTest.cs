using Matrix.RefData;
using Matrix.Utils;
using System;
using System.Diagnostics;

namespace Matrix.Tests.Timing
{
    static class MatrixTranpositionTimeTest
    {
        /// <summary>Timed test of transpose method.</summary>
        /// <param name="x">matrix - takes maxtrix to be transposed</param>
        public static void Transpose(Matrix x, TablePrinter tab = null)
        {
            // time the multiplication
            var watch = Stopwatch.StartNew();
            x.T();
            var ms = watch.ElapsedMilliseconds;
            watch.Stop();

            // report results
            if (tab != null)
                tab.AddRow(new string[] { "Transposition", string.Format("{0} x {1}", x.Rows, x.Cols), ms.ToString() }, 
                           new Alignment[] { Alignment.Left, Alignment.Left, Alignment.Right });
        }

        /// <summary>Alternative constructor which generates the matrix to be run through the timed test and initalises it with random numbers</summary>
        /// <param name="rows">int - number of rows in new matrix</param>
        /// <param name="cols">int - number of columns in new matrix</param>
        public static void Transpose(int rows, int cols, TablePrinter tab = null)
        {
            Transpose(new Matrix(rows, cols, initialiseWithRandomValues: true), tab);
        }
    }
}
