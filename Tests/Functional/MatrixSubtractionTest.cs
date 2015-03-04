﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix.Tests.Functional
{
    static class MatrixSubtractionTest
    {
        public static void Subtract(Matrix x, Matrix y, Matrix zShouldBe)
        {
            // - operator has override
            var z = x - y;
            Debug.WriteLine(string.Format("Result of x - y (Matrix z) set to: {0}{1}", Environment.NewLine, z));

            // this is also implicitly testing the overloaded equality operator on Matrix
            Debug.Assert(z == zShouldBe);

            // test equals overload
            Debug.Assert(z.Equals(zShouldBe));

            // test not equal
            z[0][0] = 999;
            Debug.Assert(z != zShouldBe);
        }
    }
}