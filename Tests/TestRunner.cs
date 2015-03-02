namespace Matrix.Tests
{
    class TestRunner
    {        public TestRunner()
        {
            new MatrixMultiplicationResultTest();
            new MatrixIndexerTest();

            // test larger multiplication
            new MatrixMultiplicationTimeTest(100);
            new MatrixMultiplicationTimeTest(500);
            //new MatrixMultiplicationTest(10000, 200, 200, 10000); // test none square array
        }
    }
}
