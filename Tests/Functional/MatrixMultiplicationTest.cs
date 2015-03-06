using System;
using System.Diagnostics;

namespace Matrix.Tests.Functional
{
    static class MatrixMultiplicationTest
    {
        public static void Multiply(Matrix a, Matrix b, Matrix cShouldBe)
        {
            Debug.WriteLine("Matrix a set to: {0}{1}", Environment.NewLine, a);
            Debug.WriteLine("Matrix b set to: {0}{1}", Environment.NewLine, b);

            // * operator has overload to get dot product
            var c = a * b;
            Debug.WriteLine("Result of dot product (Matrix c) set to: {0}{1}", Environment.NewLine, c);

            // this is also implicitly testing the overloaded equality operator on Matrix
            Debug.Assert(c == cShouldBe);

            // test equals overload
            Debug.Assert(c.Equals(cShouldBe));

            // test not equal
            c[0][0] = 999;
            Debug.Assert(c != cShouldBe);
        }

        public static void MultiplyScalar(Matrix a, double scalarValue, Matrix resultShouldBe)
        {
            Debug.Assert(a*scalarValue == resultShouldBe);
            Debug.Assert(scalarValue*a == resultShouldBe);
            Debug.Assert((a * scalarValue) * 3.14592 == resultShouldBe * 3.14592);
        }
    }
}
