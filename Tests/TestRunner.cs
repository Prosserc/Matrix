namespace Matrix.Tests
{
    class TestRunner
    {   
        public TestRunner()
        {
            // test multiplication result
            new MatrixMultiplicationResultTest();

            // test indexing
            new MatrixIndexerTest();

            // test larger multiplication
            new MatrixMultiplicationTimeTest(100);
            new MatrixMultiplicationTimeTest(250);
            new MatrixMultiplicationTimeTest(1000);
            new MatrixMultiplicationTimeTest(10000, 200, 200, 10000); // test none square array
            new MatrixMultiplicationTimeTest(10000);
        }
    }
}
