using Matrix.Tests.Utils;
using System.Diagnostics;

namespace Matrix.Tests.Timing
{
    static class MatrixEqualityTimeTest
    {
        /// <summary>CheckEquality represents worst case time scenario as the values are equal (the method short circuits as soon as an inequality is found).</summary>
        /// <param name="squareMatrixDimentions">int - number of rows / columns in each square matrix</param>
        /// <param name="tab">optional tablePrinter - row will be added to tablePrinter if passed in</param>
        public static void CheckEquality(int squareMatrixDimentions, TablePrinter tab = null)
        {
            CheckEquality(squareMatrixDimentions, squareMatrixDimentions, tab);
        }

        /// <summary>CheckEquality represents worst case time scenario as the values are equal (the method short circuits as soon as an inequality is found).</summary>
        /// <param name="rows">int - number of rows in each matrix</param>
        /// <param name="cols">int - number of columns in each matrix</param>
        /// <param name="tab">optional tablePrinter - row will be added to tablePrinter if passed in</param>
        /// <remarks>Speed of equality check for unequal matricies would depend on how early the first difference occurs</remarks>
        public static void CheckEquality(int rows, int cols, TablePrinter tab = null)
        {
            // prep data
            var m1 = new Matrix(rows, cols, initialiseWithRandomValues: true);
            var m2 = m1.Copy();

            // time the multiplication
            var watch = Stopwatch.StartNew();
            var equal = (m1 == m2);
            var ms = watch.ElapsedMilliseconds;
            watch.Stop();

            // report results
            if (tab != null)
                tab.AddRow(new[] { "Equality check", StringUtils.FormatDimentions(rows, cols), null, ms.ToString("#,###") });
        }
    }
}
