using System;
using System.Diagnostics;
using Matrix.Tests.Data;
using Matrix.Tests.Functional;
using Matrix.Tests.Timing;

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
            var cShouldBe = staticData.ATimesBShouldBe;

            // test multiplication result
            MatrixMultiplicationTest.Multiply(a, b, cShouldBe);

            // test addition result
            var d = staticData.D;
            var e = staticData.E;
            var fShouldBe = staticData.DPlusEShouldBe;
            MatrixAdditionTest.Add(d, e, fShouldBe);

            // test subtraction result
            var gShouldBe = staticData.DMinusEShouldBe;
            MatrixSubtractionTest.Subtract(d, e, gShouldBe);
            
            // test transpoition result
            var c = a * b;
            var cTransposedShouldBe = staticData.CTransposedShouldBe;
            MatrixTranspositionTest.Transpose(c, cTransposedShouldBe);
            // transpose c again to return it to original
            var c2 = MatrixTranspositionTest.GetTransposedCopy(c);
            MatrixTranspositionTest.TestEquality(c2, cShouldBe);

            // test indexing
            MatrixIndexerTest.GetViaIndex(a, 1, 1, -4);
            MatrixIndexerTest.SetViaIndex(a, 1, 1);

            // test getRows
            MatrixGetRowsTest.GetSingleRow(a);
            MatrixGetRowsTest.GetMultipleRows(a);
            MatrixGetRowsTest.GetAndManipulateRowsToCheckThatChangesPersistInOriginalMatrix(a);

            // test getCols
            MatrixGetColsTest.GetSingleCol(a);
            MatrixGetColsTest.GetMultipleCols(a);

            // test multiplication run time
            MatrixMultiplicationTimeTest.Multiply(1000, 20, 20, 1000); 
            MatrixMultiplicationTimeTest.Multiply(128);
            MatrixMultiplicationTimeTest.Multiply(256);
            MatrixMultiplicationTimeTest.Multiply(512);

            // test transposition run time
            MatrixTranpositionTimeTest.Transpose(256, 256);
            MatrixTranpositionTimeTest.Transpose(512, 512);
            MatrixTranpositionTimeTest.Transpose(1024, 1024);
            MatrixTranpositionTimeTest.Transpose(200, 10000);
            MatrixTranpositionTimeTest.Transpose(10000, 200);

            // test addition run time
            MatrixAdditionTimeTest.Add(256);
            MatrixAdditionTimeTest.Add(512);
            MatrixAdditionTimeTest.Add(1024);
            MatrixAdditionTimeTest.Add(500, 5000);
            //MatrixAdditionTimeTest.Add(10000); // ~2.9 secs on home Laptop (see improvement after parallelisation

            // test subtraction run time
            MatrixSubtractionTimeTest.Subtract(256);
            MatrixSubtractionTimeTest.Subtract(512);
            MatrixSubtractionTimeTest.Subtract(1024);
            MatrixSubtractionTimeTest.Subtract(500, 5000);

            // test subtraction run time
            MatrixEqualityTimeTest.CheckEquality(256);
            MatrixEqualityTimeTest.CheckEquality(512);
            MatrixEqualityTimeTest.CheckEquality(1024);
            MatrixEqualityTimeTest.CheckEquality(500, 5000);

            var secs = ((Double)watch.ElapsedMilliseconds)/1000;
            watch.Stop();
            var msg = String.Format("Test suite took {0:0.###} seconds to run", secs);
            Utils.PrintTitle(msg);
        }
    }
}
