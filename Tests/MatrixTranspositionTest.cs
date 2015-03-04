﻿using System;
using System.Diagnostics;

namespace Matrix.Tests
{
    public static class MatrixTranspositionTest
    {
        public static void Transpose(Matrix m, Matrix mTransposedShouldBe)
        {
            m.T();
            Debug.WriteLine(string.Format("Result of Transposition on Matrix c: {0}{1}", Environment.NewLine, m));
            Debug.Assert(m.Equals(mTransposedShouldBe));
        }

        public static Matrix GetTransposedCopy(Matrix m)
        {
            var mTranposedCopy = m.GetT();
            return mTranposedCopy;
        }

        // done as separate method here, so that we can check that result from above methosa made it back to calling program
        public static void TestEquality(Matrix a, Matrix b)
        {
            Debug.Assert(a.Equals(b));
        }
    }
}