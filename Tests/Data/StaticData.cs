namespace Matrix.Tests.Data
{
    class StaticData
    {
        public Matrix A { get; set; }
        public Matrix B { get; set; }
        public Matrix CShouldBe { get; set; }

        public StaticData()
        {
            A = new Matrix(4, 3);
            A[0] = new double[] { 4, 5, -5 };
            A[1] = new double[] { -1, -4, -2 };
            A[2] = new double[] { -3, 1, 5 };
            A[3] = new double[] { 2, 1, 4 };

            B = new Matrix(3, 5);
            B[0] = new double[] { -4, 4, -1, 3, -4 };
            B[1] = new double[] { -1, -5, -5, 4, -5 };
            B[2] = new double[] { -1, -1, 0, -1, 1 };

            CShouldBe = new Matrix(4, 5);
            CShouldBe[0] = new double[] { -16, -4, -29, 37, -46 };
            CShouldBe[1] = new double[] { 10, 18, 21, -17, 22 };
            CShouldBe[2] = new double[] { 6, -22, -2, -10, 12 };
            CShouldBe[3] = new double[] { -13, -1, -7, 6, -9 };
        }
    }
}
