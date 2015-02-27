using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    // ***********************************************************************************************
    // allows indexing and operations directly on matrix object
    // initially just work on getting basic functionality working, not yet optimised for performance
    // ***********************************************************************************************

    class Matrix
    {
        private double[,] _val;
        public int Rows { get; set; }
        public int Cols { get; set; }

        public Matrix(double[,] values)
        {
            PopulateMatrix(values);
        }

        // alternative contructor specifying just rows, cols and options for intialisation
        public Matrix(int rows, int cols, bool initialiseWithRandomValues = false, int initialiseTo = 0)
        {
            if (initialiseWithRandomValues)
            {
                PopulateMatrix(rows, cols, initialiseWithRandomValues: initialiseWithRandomValues);
            }
            else if (initialiseTo != 0)
            {
                PopulateMatrix(rows, cols, initialiseTo: initialiseTo);
            }
            else
            {
                PopulateMatrix(new double[rows, cols]);   
            }
        }

        // allow creation of empty Matrix
        public Matrix()
        {}

        public void PopulateMatrix(int rows, int cols, bool initialiseWithRandomValues = false, double initialiseTo = 0)
        {
            _val = new double[rows, cols];
            Rows = rows;
            Cols = cols;

            if (initialiseWithRandomValues)
            {
                InitialiseMatrix(randInit: initialiseWithRandomValues);
            }
            else
            {
                InitialiseMatrix(defaultVal:initialiseTo);
            }
            
            SetRowAndColSize();
        }

        public void PopulateMatrix(double[,] values)
        {
            if (values == null || values.Length == 0)
            {
                InitialiseMatrix();
            }
            else
            {
                _val = values;
            }

            SetRowAndColSize();
        }

        private void SetRowAndColSize()
        {
            Rows  = _val.GetLength(0); 
            Cols = _val.Length / Rows;
        }

        private void InitialiseMatrix(double defaultVal = 0, bool randInit = true)
        {
            var rnd = new Random();
            for (var n = 0; n < Rows; n++)
            {
                for (var m = 0; m < Cols; m++)
                {
                    this[n, m] = randInit ? rnd.NextDouble() : defaultVal;
                }
            }
        }

        // Transpose method
        public void T()
        {
            throw new NotImplementedException();
        }

        // gets a Transposed copy of the matrix without changing the original
        public Matrix GetT()
        {
            var x = new Matrix(_val);
            x.T();
            return x;
        }

        // gets row(s) from the Matrix (index is zero based)
        public double[] GetRows(int indexFrom, int rows = 1)
        {
            throw new NotImplementedException();
        }

        // gets column(s) from the Matix (index is zero based)
        public double[] GetCols(int indexFrom, int cols = 1)
        {
            throw new NotImplementedException();
        }

        // use with GetRows / GetCols rather than implementint RowAverage / ColAverage
        public double Average()
        {
            double sum = 0;
            for (var n = 0; n < Rows; n++)
            {
                for (var m = 0; m < Cols; m++)
                {
                    sum += _val[n, m];
                }
            }
            return sum / _val.Length;
        }

        // use with GetRows / GetCols rather than implementint RowMax / ColMax
        public double Max()
        {
            var max = -1 * (Math.Pow(2, 63) +1); // min unigned 64bit int
            Debug.WriteLine("max initialised to: " + max);
            for (var n = 0; n < Rows; n++)
            {
                for (var m = 0; m < Cols; m++)
                {
                    if (_val[n, m] > max)
                    {
                        max = _val[n, m];
                    }
                }
            }
            return max;
        }

        // use with GetRows / GetCols rather than implementint RowMax / ColMax
        public double Min()
        {
            var min = (Math.Pow(2, 63) - 1); // max unsigned 64 bit int
            Debug.WriteLine("min initialised to: " + min);
            for (var n = 0; n < Rows; n++)
            {
                for (var m = 0; m < Cols; m++)
                {
                    if (_val[n, m] < min)
                    {
                        min = _val[n, m];
                    }
                }
            }
            return min;
        }


        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        // * * * * * * * * * * * * * * * * *   O P E R A T O R   O V E R L O A D S   e t c   * * * * * * * * * * * * * * * * * 
        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 


        // allow for easy calculation of dot product of matricies e.g. A*B
        public static Matrix operator *(Matrix a, Matrix b)
        {
            // check dimentions
            if (a.Cols != b.Rows)
            {
                throw new IncompatibleMatrixDimentionsException(IncompatibleMatrixDimentionsException.MatrixOperators.Multiply);
            }

            // initialise result matrix
            var res = new Matrix(new double[a.Rows, b.Cols]);

            // loop over rows in first matrix
            for (var n = 0; n < a.Rows; n++)
            {
                // loop over cols in second matrix
                for (var m = 0; m < b.Cols; m++)
                {
                    // got cell of new matix, need to myltiply each element of row in first but each element of col in second matrix
                    for (var k = 0; k < a.Cols; k++)
                    {
                        // performance is significantly reduced on large matricies if indexing matricies directly rather than via _val
                        res._val[n, m] += (a._val[n, k] * b._val[k, m]);
                    }
                }
            }
            return res;
        }

        // allow for indexing directly on Matrix rather than just Matrix.val
        public double this[int row, int col]
        {
            get { return _val[row, col]; }
            set { _val[row, col] = value; }
        }

        // allow for comparisons directly on matrix so you don't have to do matrix.Val
        public static bool operator ==(Matrix a, Matrix b)
        {
            try
            {
                return a._val == b._val;
            }
            catch (NullReferenceException e)
            {
                return a._val == null && b._val == null;
            }
        }

        public static bool operator !=(Matrix a, Matrix b)
        {
            try
            {
                return a._val != b._val;
            }
            catch (NullReferenceException e)
            {
                return a._val == null ^ b._val == null;
            }
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            throw new NotImplementedException();
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            const int maxRows = 11;
            const int maxCols = 11;

            var str = "";
            var len = 9;
            var max = this.Max();
            string formatStr;

            if (max < 100)
            {
                formatStr = "0.000000";
            }
            else if (max < 1000)
            {
                formatStr = "0.00000";
            }
            else if (max < 10000)
            {
                formatStr = "0.0000";
            }
            else if (max < 100000)
            {
                formatStr = "0.000";
            }
            else
            {
                formatStr = "0.00";
                len = Convert.ToInt64(max).ToString(CultureInfo.InvariantCulture).Length + 3;
            }

            for (var n = 0; n < Rows; n++)
            {
                if (n > maxRows && n != Rows-1 )
                {
                    continue;
                }
                else if (n == maxRows)
                {
                    for (var m = 0; m < maxCols+2; m++) str += "...".PadLeft(len + 2, ' ');
                }
                else
                {
                    for (var m = 0; m < Cols; m++)
                    {
                        if (m > maxCols && m != Cols - 1)
                        {
                            continue;
                        }
                        else if (m == maxCols)
                        {
                            str += "...".PadLeft(len + 2, ' ');
                        }
                        else
                        {
                            str += this[n, m].ToString(formatStr).PadLeft(len + 2, ' ');
                        }
                    }
                }

                str += Environment.NewLine;
            }
            return str;
        }

        // ******************************************************
        // system generated to g owith equality operators...
        // ******************************************************

        protected bool Equals(Matrix other)
        {
            return Equals(_val, other._val) && Rows == other.Rows && Cols == other.Cols;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Matrix)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (_val != null ? _val.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Rows;
                hashCode = (hashCode * 397) ^ Cols;
                return hashCode;
            }
        }
    }
}
