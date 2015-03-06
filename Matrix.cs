using System;
using System.Globalization;
using System.Linq;
using Matrix.Tests;

namespace Matrix
{
    /// <summary>
    /// Allows indexing and operations such as * + - and == directly on matrix object
    /// </summary>
    /// <remarks>
    /// Initially tried 2d array, jagged array seems to get better performance on multiplication (~30% saving)
    /// TODO:
    ///     inverse method;
    ///     identity Matrices;
    ///     elementwise operations;
    ///         - consider multiplication with numbers e.g. matrix * 2
    ///     options to load Matrix data from files;
    ///     consider other operations that may be useful for machine learning;
    ///     Performance could be optimised for +,-,==,!=, Max(), Min, Average() and T() e.g. parallelisation - not urgent as already pretty quick;
    ///     ??? allow for matrices containing values other than double e.g. bit, int etc - not sure how difficult this will be yet ???
    /// </remarks>
    public class Matrix
    {
        private double[][] _val;
        public int Rows { get; private set; }
        public int Cols { get; private set; }
        

        /// <summary>Defines margin of error allowed to consider matrix elements equal - allowing for floating point precision errors.</summary>
        /// <remarks>Double allows for 15-16 degits precision, so the value should be within this range.</remarks>
        public const double EPSILON = 10e-12;
        
        /// <summary>
        /// Constructor, creates Matrix object from jagged array.
        /// </summary>
        /// <param name="values">double[][] - jagged array can be empty or populated</param>
        /// <param name="supressPopulation">optional bool - if set to false matrix elements will be initialised to defaut values</param>
        public Matrix(double[][] values, bool supressPopulation = true)
        {
            if (supressPopulation)
            {
                _val = values;
                SetRowAndColSize();
            }
            else
            {
                PopulateMatrix(values);
            }
        }

        /// <summary>
        /// Alternative contructor specifying just rows, cols and options for intialisation.
        /// </summary>
        /// <param name="rows">int - specifying number of rows</param>
        /// <param name="cols">int - specifying number of columns</param>
        /// <param name="initialiseWithRandomValues">optional bool - if set to true each element will be set to a random number between 0 and 1</param>
        /// <param name="initialiseTo">optional int - only applies if initialiseWithRandomVariables is set to false, all elements will be set to this number</param> 
        public Matrix(int rows, int cols, bool initialiseWithRandomValues = false, int initialiseTo = 0)
        {
            if (initialiseWithRandomValues)
            {
                PopulateMatrix(rows, cols, initialiseWithRandomValues: true);
            }
            else if (initialiseTo != 0)
            {
                PopulateMatrix(rows, cols, initialiseTo: initialiseTo);
            }
            else
            {
                _val = new double[rows][];
                CreateColumns(rows, cols);
                PopulateMatrix(_val);   
            }
        }
        
        private void CreateColumns(int rows, int cols)
        {
            // using jagged array need to initialise each column
            for (var i = 0; i < rows; i++)
            {
                _val[i] = new double[cols];
            }
        }

        /// <summary>
        /// Populates a matrix object with set values or random values. ** Warning ** this will overwrite existing values.
        /// </summary>
        /// <param name="rows">int - specifying number of rows</param>
        /// <param name="cols">int - specifying number of columns</param>
        /// <param name="initialiseWithRandomValues">optional bool - if set to true each element will be set to a random number between 0 and 1</param>
        /// <param name="initialiseTo">optional int - only applies if initialiseWithRandomVariables is set to false, all elements will be set to this number</param> 
        public void PopulateMatrix(int rows, int cols, bool initialiseWithRandomValues = false, double initialiseTo = 0)
        {
            _val = new double[rows][];
            CreateColumns(rows, cols);
            PopulateMatrix(_val);  
            Rows = rows;
            Cols = cols;

            if (initialiseWithRandomValues)
            {
                InitialiseMatrix(randInit: true);
            }
            else
            {
                InitialiseMatrix(defaultVal:initialiseTo, randInit:false);
            }
            
            SetRowAndColSize();
        }

        /// <summary>
        /// Overloaded method to populate the matrix with values provided or initialise matrix if an empty array is provided
        /// </summary>
        /// <param name="values">double[][] - jagged array of values to populate matrix with, defaults useed if empty</param>
        public void PopulateMatrix(double[][] values)
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
            Rows  = _val.Length; 
            // even though it is technically implemented as a jagged array, each sub array is the same size so just getting len of the first row is fine
            Cols = _val[0].Length;
        }

        private void InitialiseMatrix(double defaultVal = 0, bool randInit = true)
        {
            var rnd = new Random();
            for (var n = 0; n < Rows; n++)
            {
                for (var m = 0; m < Cols; m++)
                {
                    _val[n][m] = randInit ? rnd.NextDouble() : defaultVal;
                }
            }
        }

        /// <summary>Get deep copy of Matrix</summary>
        public Matrix Copy()
        {
            return new Matrix(_val.Select(s => s.ToArray()).ToArray());
        }

        /// <summary>
        /// Transpose method - transposes the matrix so that the rows become the columns and vice versa. (Shorthand entry point).
        /// </summary>
        public void T()
        {
            Transpose();
        }

        /// <summary>
        /// Tranpose method - transposes the matrix so that the rows become the columns and vice versa.
        /// </summary>
        public void Transpose()
        {
            // initialise new double array with switched rows / cols
            var newVal = new double[Cols][];
            for (var i = 0; i < Cols; i++)
            {
                newVal[i] = new double[Rows];
                
                // populate row of transposed array
                for (var j = 0; j < Rows; j++ )
                {
                    newVal[i][j] = _val[j][i];
                }
            }
            _val = newVal;
            SetRowAndColSize();
        }

        /// <summary>
        /// Gets a Transposed copy of the matrix without changing the original. (Shorthand entry point).
        /// </summary>
        /// <returns>Matrix of double[orig.Cols][orig.Rows]</returns>
        public Matrix GetT()
        {
            return GetTransposedCopy();
        }

        /// <summary>
        /// Gets a Transposed copy of the matrix without changing the original.
        /// </summary>
        /// <returns>Matrix of double[orig.Cols][orig.Rows]</returns>
        public Matrix GetTransposedCopy()
        {
            var x = Copy();
            x.Transpose();
            return x;
        }

        public Matrix GetIdentity()
        {
            var up = new Exception("Ambiguous request for identity matrix, use the static class method Matrix.GetIdentity(int) for non square matrices.");
            if (Rows != Cols) throw up;
            return GetIdentity(Rows);
        }

        /// <summary>
        /// Gets identity matrix of specified dimentions.
        /// </summary>
        /// <param name="rows">int - required dimentions of identity matrix</param>
        /// <returns></returns>
        public static Matrix GetIdentity(int rows)
        {
            var id = new Matrix(rows, rows);
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < rows; j++)
                {
                    id[i][j] = i == j ? 1 : 0;
                }
            }

            return id;
        }

        /// <summary>
        /// Gets Rows from Matrix and returns a subset of the original. Note, any changes made to the subset are also reflected in the original matrix.
        /// </summary>
        /// <param name="indexFrom">int - zero based index, subset of matrix created from this row</param>
        /// <param name="numRows">optional int - number of rows to return in subset</param>
        /// <returns>Matrix of double[numRows][this.Cols]</returns>
        public Matrix GetRows(int indexFrom, int numRows)
        {
            var result = new double[numRows][];
                
            for (var i = 0; i < numRows; i++)
            {
                result[i] = _val[i+indexFrom];
            }
            return new Matrix(result);
        }

        /// <summary>
        /// Gets Columns from Matrix and returns a subset of the original. Note, any changes made to the subset are also reflected in the original matrix.
        /// </summary>
        /// <param name="indexFrom">int - zero based index, subset of matrix created from this row</param>
        /// <param name="numCols">optional int - number of columns to return in subset</param>
        /// <returns>Matrix of double[this.Rows][numCols]</returns>
        public Matrix GetCols(int indexFrom, int numCols)
        {
            var result = new double[Rows][];

            for (var j = 0; j < numCols; j++)
            {
                // TODO - may be a better way to do this using Linq
                //var tmpCol = _val.Select(row => row[indexFrom + j]);
                for (var i = 0; i < Rows; i++)
                {
                    if (j == 0)
                    {
                        result[i] = new double[numCols];
                    }
                    result[i][j] = _val[i][indexFrom + j];
                }
            }
            return new Matrix(result);
        }

        /// <summary>
        /// Gets average value in Matrix, use with GetRows or GetCols to get the average for a subset
        /// </summary>
        public double Average()
        {
            double sum = 0;
            for (var n = 0; n < Rows; n++)
            {
                for (var m = 0; m < Cols; m++)
                {
                    sum += _val[n][m];
                }
            }
            return sum / (Rows * Cols);
        }

        /// <summary>
        /// Gets maximum value in Matrix, use with GetRows or GetCols to get the max for a subset
        /// </summary>
        public double Max()
        {
            var max = -1 * (Math.Pow(2, 63) +1); // min unigned 64bit int
            //Debug.WriteLine("max initialised to: " + max);
            for (var n = 0; n < Rows; n++)
            {
                for (var m = 0; m < Cols; m++)
                {
                    if (_val[n][m] > max)
                    {
                        max = _val[n][m];
                    }
                }
            }
            return max;
        }

        /// <summary>
        /// Gets minimum value in Matrix, use with GetRows or GetCols to get the min for a subset
        /// </summary>
        public double Min()
        {
            var min = (Math.Pow(2, 63) - 1); // max unsigned 64 bit int
            //Debug.WriteLine("min initialised to: " + min);
            for (var n = 0; n < Rows; n++)
            {
                for (var m = 0; m < Cols; m++)
                {
                    if (_val[n][m] < min)
                    {
                        min = _val[n][m];
                    }
                }
            }
            return min;
        }

        /// <summary>
        /// Gets the Dot Product from the multiplication of two Matrices. Note: the number of columns in the first matrix must be equal to the number of rows in the second matrix.
        /// </summary>
        /// <param name="a">matrix - first matrix for multiplication</param>
        /// <param name="b">matrix - second matrix for multiplication</param>
        /// <remarks>Changed ijk to ikj loop for performance optimisation.</remarks>
        /// <returns>Matrix of double[a.Rows][b.Cols]</returns>
        /// <exception cref="IncompatibleMatrixDimentionsException(MatrixOperators)"></exception>
        public static Matrix DotProduct(Matrix a, Matrix b)
        {
            // check dimentions
            if (a.Cols != b.Rows)
            {
                throw new IncompatibleMatrixDimentionsException(MatrixOperators.Multiply);
            }

            // initialise result matrix
            var res = new Matrix(a.Rows, b.Cols);

            // parallel pLinq query to do multiple rows at once
            var source = Enumerable.Range(0, a.Rows);
            var query = from num in source.AsParallel()
                        select num;
            query.ForAll(i => DotProductRowParallel(a, b, res, i));
            return res;
        }

        private static void DotProductRowParallel(Matrix a, Matrix b, Matrix res, int i)
        {
            double[] ithRowOfA = a[i];
            double[] ithRowOfResult = res[i];
            for (var k = 0; k < a.Cols; k++)
            {
                double[] kthRowOfB = b[k];
                double indexIKOfA = ithRowOfA[k];
                for (var j = 0; j < b.Cols; j++)
                {
                    ithRowOfResult[j] += indexIKOfA * kthRowOfB[j];
                }
            }
        }


        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        // * * * * * * * * * * * * * * * * *   O P E R A T O R   O V E R L O A D S   e t c   * * * * * * * * * * * * * * * * * 
        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 


        /// <summary>Operator overload to allow for easy calculation of dot product of Matrices e.g. A*B.</summary>
        /// <see cref="DotProduct(Matrix,Matrix)"/> for more information.
        /// <exception cref="IncompatibleMatrixDimentionsException(MatrixOperators)"></exception>
        public static Matrix operator *(Matrix a, Matrix b)
        {
            return DotProduct(a, b);
        }

        // allow for indexing directly on Matrix rather than just Matrix.val
        // only need to facilitate indexing on rows, then default array behaviour will allow user to index within this row e.g. this[0][0] to get first row / col
        public double[] this[int rownum]
        {
            get { return _val[rownum]; }
            set { _val[rownum] = value; }
        }

        // allow for comparisons directly on matrix so you don't have to do matrix.Val
        // TODO - allow for null Matrices?
        public static bool operator ==(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Cols != b.Cols) return false;

            for (var n = 0; n < a.Rows; n++)
            {
                for (var m = 0; m < a.Cols; m ++)
                {
                    // loop over rows / cols, if any different return false
                    // used comparison with EPSILON rather than "a[n][m] == b[n][m]" to avoid floating point precision errors
                    if ((Math.Abs(a[n][m] - b[n][m]) > EPSILON)) return false;
                }
            }
            return true;
        }

        // TODO - allow for null Matrices?
        public static bool operator !=(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Cols != b.Cols) return true;

            for (var n = 0; n < a.Rows; n++)
            {
                for (var m = 0; m < a.Cols; m++)
                {
                    // loop over rows/ cols if any different return true (i.e. true that the Matrices are not equal)
                    if ((Math.Abs(a[n][m] - b[n][m]) > EPSILON)) return true;
                }
            }
            return false;
            
        }

        /// <exception cref="IncompatibleMatrixDimentionsException(MatrixOperators)"></exception>
        public static Matrix operator +(Matrix a, Matrix b)
        {
            // check dimentions
            if (a.Cols != b.Cols || a.Rows != b.Rows)
            {
                throw new IncompatibleMatrixDimentionsException(MatrixOperators.Add);
            }

            // initialise result matrix
            var res = new Matrix(a.Rows, b.Cols);

            // TODO - test timings and look for performance optimisations
            for (var i = 0; i < a.Rows; i++)
            {
                for (var j = 0; j < a.Cols; j++)
                {
                    res[i][j] = a[i][j] + b[i][j];
                }
            }

                return res;
        }

        /// <exception cref="IncompatibleMatrixDimentionsException(MatrixOperators)"></exception>
        public static Matrix operator -(Matrix a, Matrix b)
        {
            // check dimentions
            if (a.Cols != b.Cols || a.Rows != b.Rows)
            {
                throw new IncompatibleMatrixDimentionsException(MatrixOperators.Minus);
            }

            // initialise result matrix
            var res = new Matrix(a.Rows, b.Cols);

            // TODO - test timings and look for performance optimisations
            for (var i = 0; i < a.Rows; i++)
            {
                for (var j = 0; j < a.Cols; j++)
                {
                    res[i][j] = a[i][j] - b[i][j];
                }
            }

            return res;
        }

        public override string ToString()
        {
            const int maxRows = 11;
            const int maxCols = 11;

            var str = "";
            var len = 9;
            var max = Max();
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
                if (n == maxRows)
                {
                    for (var m = 0; m < maxCols+2; m++) str += "...".PadLeft(len + 2, ' ');
                }
                else
                {
                    for (var m = 0; m < Cols; m++)
                    {
                        if (m > maxCols && m != Cols - 1) continue;

                        if (m == maxCols)
                        {
                            str += "...".PadLeft(len + 2, ' ');
                        }
                        else
                        {
                            str += _val[n][m].ToString(formatStr).PadLeft(len + 2, ' ');
                        }
                    }
                }

                str += Environment.NewLine;
            }
            return str;
        }

        // ******************************************************
        // system generated to go with equality operators...
        // ******************************************************

        protected bool Equals(Matrix other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Matrix) obj);
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
