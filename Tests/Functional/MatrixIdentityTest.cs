using System;
using System.Diagnostics;

namespace Matrix.Tests.Functional
{
    static class MatrixIdentityTest
    {
        public static void IdentityTests()
        {
            var a = new Matrix(5, 5, true);
            Debug.Print("Identity Matrix for a:{0}{1}", Environment.NewLine, a.GetIdentity());

            // if you want to throw error...
            var b = new Matrix(5, 2, true);
            //Debug.Print("Identity Matrix for b:{0}{1}", Environment.NewLine, b.GetIdentity());

            // test class method for non square matricies
            Debug.Print("b:{0}{1}", Environment.NewLine, b);

            var bColsId = Matrix.GetIdentity(b.Cols);
            Debug.Print("Identity Matrix for bColsId:{0}{1}", Environment.NewLine, bColsId);
            Debug.Print("b * bColsId :{0}{1}", Environment.NewLine, b * bColsId);
            Debug.Assert(b == (b * bColsId));

            var bRowsId = Matrix.GetIdentity(b.Rows);
            Debug.Print("Identity Matrix for bRowsId:{0}{1}", Environment.NewLine, bRowsId);
            Debug.Print("bRowsId * b :{0}{1}", Environment.NewLine, bRowsId * b);
            Debug.Assert(b == (bRowsId * b));
        }
    }
}
