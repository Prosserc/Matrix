namespace Matrix.Tests
{
    class TestRunner
    {   
        public TestRunner()
        {
            // get static data for tests
            var staticData = new StaticData();
            var a = staticData.A;
            var b = staticData.B;
            var cShouldBe = staticData.CShouldBe;

            // test multiplication result
            MatrixMultiplicationResultTest.Multiply(a, b, cShouldBe);

            // test indexing
            MatrixIndexerTest.GetViaIndex(a, 1, 0, -1);
            MatrixIndexerTest.SetViaIndex(a, 1, 0);

            // try increasing dimentions (of square matricies) by powers of 2 to get approx formula for running time
            MatrixMultiplicationTimeTest.Multiply(128);
            MatrixMultiplicationTimeTest.Multiply(256);
            MatrixMultiplicationTimeTest.Multiply(512);
            MatrixMultiplicationTimeTest.Multiply(1024);
            //new MatrixMultiplicationTimeTest(10000); //3,136,656 ms (52 mins)
            //MatrixMultiplicationTimeTest.Multiply(10000, 200, 200, 10000); // test none square array
        }
    }
}
