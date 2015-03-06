using System;
using System.Diagnostics;
using Matrix.Tests.Data;
using Matrix.Tests.Functional;
using Matrix.Tests.Timing;
using Matrix.Tests.Utils;

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

            // test identity matrices
            MatrixIdentityTest.IdentityTests();

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


            var tab = new TablePrinter(new[] { "Operation", "Dimentions 1", "Dimentions 2", "Run Time (ms)" }, 
                                       new[] { 18, 18, 18, 18 },
                                       new[] { Alignment.Left, Alignment.Centre, Alignment.Centre, Alignment.Right },
                                       new[] { Alignment.Centre, Alignment.Centre, Alignment.Centre, Alignment.Centre} );

            // test multiplication run time
            MatrixMultiplicationTimeTest.Multiply(1000, 20, 20, 1000, tab);
            MatrixMultiplicationTimeTest.Multiply(20, 1000, 1000, 20, tab);
            MatrixMultiplicationTimeTest.Multiply(128, tab);
            MatrixMultiplicationTimeTest.Multiply(256, tab);
            MatrixMultiplicationTimeTest.Multiply(512, tab);
            tab.AddVerticalBoundaryRow();

            // test transposition run time
            MatrixTranpositionTimeTest.Transpose(256, 256, tab);
            MatrixTranpositionTimeTest.Transpose(512, 512, tab);
            MatrixTranpositionTimeTest.Transpose(1024, 1024, tab);
            MatrixTranpositionTimeTest.Transpose(200, 10000, tab);
            MatrixTranpositionTimeTest.Transpose(10000, 200, tab);
            tab.AddVerticalBoundaryRow();

            // test addition run time
            MatrixAdditionTimeTest.Add(256, tab);
            MatrixAdditionTimeTest.Add(512, tab);
            MatrixAdditionTimeTest.Add(1024, tab);
            MatrixAdditionTimeTest.Add(500, 5000, tab);
            //MatrixAdditionTimeTest.Add(10000); // ~2.9 secs on home Laptop (see improvement after parallelisation
            tab.AddVerticalBoundaryRow();

            // test subtraction run time
            MatrixSubtractionTimeTest.Subtract(256, tab);
            MatrixSubtractionTimeTest.Subtract(512, tab);
            MatrixSubtractionTimeTest.Subtract(1024, tab);
            MatrixSubtractionTimeTest.Subtract(500, 5000, tab);
            tab.AddVerticalBoundaryRow();

            // test subtraction run time
            MatrixEqualityTimeTest.CheckEquality(256, tab);
            MatrixEqualityTimeTest.CheckEquality(512, tab);
            MatrixEqualityTimeTest.CheckEquality(1024, tab);
            MatrixEqualityTimeTest.CheckEquality(500, 5000, tab);

            tab.Print();

            var secs = ((Double)watch.ElapsedMilliseconds)/1000;
            watch.Stop();
            var msg = String.Format("Test suite took {0:0.###} seconds to run", secs);
            StringUtils.PrintTitle(msg);
        }
    }
}
