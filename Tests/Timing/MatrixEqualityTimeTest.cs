using System;
using System.Diagnostics;

namespace Matrix.Tests.Timing
{
    static class MatrixEqualityTimeTest
    {
        /// <summary>CheckEquality represents worst case time scenario as the values are equal (the method short circuits as soon as an inequality is found).</summary>
        /// <param name="squareMatrixDimentions">int - number of rows / columns in each square matrix</param>
        public static void CheckEquality(int squareMatrixDimentions)
        {
            CheckEquality(squareMatrixDimentions, squareMatrixDimentions);
        }

        /// <summary>CheckEquality represents worst case time scenario as the values are equal (the method short circuits as soon as an inequality is found).</summary>
        /// <param name="rows">int - number of rows in each matrix</param>
        /// <param name="cols">int - number of columns in each matrix</param>
        public static void CheckEquality(int rows, int cols)
        {
            // prep data
            var m1 = new Matrix(rows, cols, initialiseWithRandomValues: true);
            var m2 = m1.Copy();

            // time the multiplication
            var watch = Stopwatch.StartNew();
            var match = (m1 == m2);
            var ms = watch.ElapsedMilliseconds;
            watch.Stop();

            // report results
            Debug.WriteLine(string.Format("*** Equality check took {0} ms on {1} x {2} Matrices ***", ms, rows, cols));
        }
    }
}
