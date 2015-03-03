using System;
using System.Diagnostics;

namespace Matrix.Tests
{
    class MatrixMultiplicationResultTest
    {
        public static void Multiply(Matrix a, Matrix b, Matrix cShouldBe)
        {
            Debug.WriteLine(string.Format("Matrix a set to: {0}{1}", Environment.NewLine, a));
            Debug.WriteLine(string.Format("Matrix b set to: {0}{1}", Environment.NewLine, b));

            // * opperator has override to get dot product
            var c = a * b;
            Debug.WriteLine(string.Format("Result of dot product (Matrix c) set to: {0}{1}", Environment.NewLine, c));

            // this is also implicitly testing the overloaded equality operator on Matrix
            Debug.Assert(c == cShouldBe);

            // test equals overload
            Debug.Assert(c.Equals(cShouldBe));

            // test not equal
            c[0][0] = 999;
            Debug.Assert(c != cShouldBe);
        }
    }
}
