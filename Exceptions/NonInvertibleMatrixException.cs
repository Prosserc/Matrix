using System;

namespace Matrix.Exceptions
{
    class NonInvertibleMatrixException: Exception
    {
        public NonInvertibleMatrixException(string msg)
            : base(msg)
        {
        }
    }
}
