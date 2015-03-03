using System;
using System.Diagnostics;

namespace Matrix.Tests
{
    static class MatrixIndexerTest
    {
        public static void GetViaIndex(Matrix a, int rowIndex=0, int colIndex=0, double valShouldBe=0)
        {
            var val = a[rowIndex][colIndex];
            Debug.WriteLine(string.Format("get a[0][1]: {0:0.######}", val));
            Debug.Assert(val.Equals(valShouldBe));
        }

        public static void SetViaIndex(Matrix a, int rowIndex = 0, int colIndex = 0)
        {
            const double val = 3.145;
            Debug.WriteLine(string.Format("set a[0][1] to {0:0.######}", val));
            a[rowIndex][colIndex] = val;
            Debug.WriteLine(string.Format("get a[0][1]: {0:0.######}", a[rowIndex][colIndex]));
            Debug.Assert(a[rowIndex][colIndex].Equals(val));
        }
    }
}
