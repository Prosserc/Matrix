using Matrix.Tests;
using Matrix.Tests.Utils;
using System;
using System.Diagnostics;

namespace Matrix.Tests.Timing
{
    static class MatrixMultiplicationTimeTest
    {
        // shorthand for constructor with square Matrices
        public static void Multiply(int squareMatrixDimentions, TablePrinter tab = null)
        {
            Multiply(squareMatrixDimentions, squareMatrixDimentions, squareMatrixDimentions, squareMatrixDimentions, tab);
        }

        public static void Multiply(int m1Rows, int m1Cols, int m2Rows, int m2Cols, TablePrinter tab = null)
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
            if (tab != null)
                tab.AddRow(new string[] { "Multiplication", StringUtils.FormatDimentions(m1Rows, m1Cols), 
                                          StringUtils.FormatDimentions(m2Rows, m2Cols), ms.ToString("#,###") });
            
            var msg = string.Format("Multiplication took {0} ms on {1} x {2} and {3} x {4} Matrices. Result:", ms, m1Rows, m1Cols, m2Rows, m2Cols);
            Debug.WriteLine("{0}{1}{2}{3}{4}", msg, Environment.NewLine, new string('-', msg.Length-1), Environment.NewLine, m3);
        }
    }
}
