using System;
using System.Diagnostics;

namespace Matrix.Tests
{
    class MatrixMultiplicationTimeTest
    {
        public MatrixMultiplicationTimeTest(int m1Rows, int m1Cols, int m2Rows, int m2Cols)
        {
            Multiply(m1Rows, m1Cols, m2Rows, m2Cols);
        }

        // shorthand for constructor with square matricies
        public MatrixMultiplicationTimeTest(int squareMatrixDimentions)
        {
            Multiply(squareMatrixDimentions, squareMatrixDimentions, squareMatrixDimentions, squareMatrixDimentions);
        }

        private static void Multiply(int m1Rows, int m1Cols, int m2Rows, int m2Cols)
        {
            // prep data
            var m1 = new Matrix(m1Rows, m1Cols, initialiseWithRandomValues: true);
            var m2 = new Matrix(m2Rows, m2Cols, initialiseWithRandomValues: true);

            // time the multiplication
            var watch = Stopwatch.StartNew();
            var m3 = m1*m2;
            var ms = watch.ElapsedMilliseconds;
            watch.Stop();

            // report results
            Debug.WriteLine(Environment.NewLine + "*** Multiplication took " + ms + " ms ***");
            var msg = "Result of dot product (dimentions " + m1Rows + "x" + m1Cols + " and " + m2Rows + "x" + m2Cols +
                      ")" + " set to:";
            Debug.WriteLine(msg + Environment.NewLine + new string('-', msg.Length-1) + Environment.NewLine + m3);
            Debug.WriteLine("The average value is: " + m3.Average());
        }
    }
}
