using System;
using System.Collections.Generic;

namespace Matrix
{
    class IncompatibleMatrixDimentionsException: Exception
    {
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
