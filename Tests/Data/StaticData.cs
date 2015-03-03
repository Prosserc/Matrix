namespace Matrix.Tests.Data
{
    class StaticData
    {
        public Matrix A { get; set; }
        public Matrix B { get; set; }
        public Matrix CShouldBe { get; set; }
        public Matrix CTransposedShouldBe { get; set; }

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

            CTransposedShouldBe = new Matrix(5, 4);
            CTransposedShouldBe[0] = new double[] { -16, 10, 6, -13 };
            CTransposedShouldBe[1] = new double[] { -4, 18, -22, -1 };
            CTransposedShouldBe[2] = new double[] { -29, 21, -2, -7 };
            CTransposedShouldBe[3] = new double[] { 37, -17, -10, 6 };
            CTransposedShouldBe[4] = new double[] { -46, 22, 12, -9 };
        }
    }
}
