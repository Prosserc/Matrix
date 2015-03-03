using System;
using System.Diagnostics;
using Matrix.Tests.Data;

namespace Matrix.Tests
{
    class TestRunner
    {   
        public TestRunner()
        {
            var watch = Stopwatch.StartNew();

            // get static data for tests
            var staticData = new StaticData();
            var a = staticData.A;
            var b = staticData.B;
            var cShouldBe = staticData.CShouldBe;

            // test multiplication result
            MatrixMultiplicationResultTest.Multiply(a, b, cShouldBe);

            // test transpoition
            var c = a * b;
            var cTransposedShouldBe = staticData.CTransposedShouldBe;
            MatrixTranspositionTest.Transpose(c, cTransposedShouldBe);
            // transpose c again to return it to original
            var c2 = MatrixTranspositionTest.GetTransposedCopy(c);
            MatrixTranspositionTest.TestEquality(c2, cShouldBe);

            // test indexing
            MatrixIndexerTest.GetViaIndex(a, 1, 0, -1);
            MatrixIndexerTest.SetViaIndex(a, 1, 0); // value only changed in local scope of test

            // test getRows
            MatrixGetRowsTest.GetSingleRow(a);
            MatrixGetRowsTest.GetMultipleRows(a);
            MatrixGetRowsTest.GetAndManipulateRowsToCheckThatChangesPersistInOriginalMatrix(a);

            //test getCols
            MatrixGetColsTest.GetSingleCol(a);
            MatrixGetColsTest.GetMultipleCols(a);

            // test multiplication run time - square matricies
            MatrixMultiplicationTimeTest.Multiply(128);
            MatrixMultiplicationTimeTest.Multiply(256);
            MatrixMultiplicationTimeTest.Multiply(512);
            //MatrixMultiplicationTimeTest.Multiply(1024);
            //MatrixMultiplicationTimeTest.Multiply(10000); //3,136,656 ms (52 mins)

            //MatrixMultiplicationTimeTest.Multiply(10000, 200, 200, 10000); // test none square array

            var secs = ((Double)watch.ElapsedMilliseconds)/1000;
            watch.Stop();
            var msg = String.Format("Test suite took {0:0.###} seconds to run", secs);
            Utils.PrintTitle(msg);
        }
    }
}
