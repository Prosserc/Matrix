using System;
using System.Diagnostics;

namespace Matrix.Tests
{
    static class MatrixGetColsTest
    {
        public static void GetSingleCol(Matrix a, int colIndex = 0)
        {
            Debug.Write(Environment.NewLine);
            Debug.WriteLine(string.Format("Col {0} of Matrix a{1}{2}", colIndex, Environment.NewLine, a.GetCols(colIndex, 1)));
        }

        public static void GetMultipleCols(Matrix a, int colIndex = 0, int numCols = 2)
        {
            Debug.Write(Environment.NewLine);
            Debug.WriteLine(string.Format("Colss {0}:{1} of Matrix a{2}{3}", colIndex, colIndex + (numCols - 1), Environment.NewLine, a.GetCols(colIndex, numCols)));
        }
    }
}
