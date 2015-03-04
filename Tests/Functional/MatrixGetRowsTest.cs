using System;
using System.Diagnostics;

namespace Matrix.Tests.Functional
{
    static class MatrixGetRowsTest
    {
        public static void GetSingleRow(Matrix a, int rowIndex=0)
        {
            Debug.Write(Environment.NewLine);
            Debug.WriteLine(string.Format("Row {0} of Matrix a{1}{2}", rowIndex, Environment.NewLine, a.GetRows(rowIndex,1)));   
        }

        public static void GetMultipleRows(Matrix a, int rowIndex = 0, int numRows = 2)
        {
            Debug.Write(Environment.NewLine);
            Debug.WriteLine(string.Format("Rows {0}:{1} of Matrix a{2}{3}", rowIndex, rowIndex+(numRows-1), Environment.NewLine, a.GetRows(rowIndex, numRows)));   
        }

        public static void GetAndManipulateRowsToCheckThatChangesPersistInOriginalMatrix(Matrix a, int rowIndex = 0, int numRows = 2)
        {
            Debug.Write(Environment.NewLine);
            var subMatrix = a.GetRows(rowIndex, numRows);

            // change values in subMatrix
            for (var i = 0; i < numRows; i++)
            {
                for (var colIndex = 0; colIndex < a.Cols; colIndex++)
                {
                    subMatrix[i][colIndex] = -1;
                }
            }

            Debug.WriteLine(string.Format("tmpMatrix now set to:{0}{1}", Environment.NewLine, subMatrix));
            Debug.WriteLine(string.Format("original matrix now set to:{0}{1}", Environment.NewLine, a));

            // check that indexed row in original matrix has been updated to reflect changes in subMatrix
            for (var i = 0; i < numRows; i++)
            {
                Debug.Assert(a[rowIndex = i].Equals(subMatrix[i]));
            }
        }
    }
}
