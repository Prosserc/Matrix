using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    class IncompatibleMatrixDimentionsException: Exception
    {
        public enum MatrixOperators
        {
            Add,
            Minus,
            Multiply//,
            //ElementwiseAdd,
            //ElementwiseMinus,
            //ElementWiseMultiply
        }

        private static readonly Dictionary<MatrixOperators, string> MsgDict = new Dictionary<MatrixOperators, string>()
        {
            { MatrixOperators.Multiply, "For matrix multiplication the number of columns in the first matrix must be equal to the number of rows in the second matrix." },
            { MatrixOperators.Minus, "For Matrix subtraction the dimentions of both matricies must be the same." },
            { MatrixOperators.Add, "For Matrix addition the dimentions of both matricies must be the same." }
        };

        public IncompatibleMatrixDimentionsException(MatrixOperators op)
            : base (MsgDict[op]) {}
    }
}
