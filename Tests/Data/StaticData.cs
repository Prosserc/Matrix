namespace Matrix.Tests.Data
{
    class StaticData
    {
        public Matrix A { get; set; }
        public Matrix B { get; set; }
        public Matrix ATimesBShouldBe { get; set; }
        public Matrix CTransposedShouldBe { get; set; }
        public Matrix D { get; set; }
        public Matrix E { get; set; }
        public Matrix DPlusEShouldBe { get; set; }
        public Matrix DMinusEShouldBe { get; set; }

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

            // used to validate result of A * B in tests
            ATimesBShouldBe = new Matrix(4, 5);
            ATimesBShouldBe[0] = new double[] { -16, -4, -29, 37, -46 };
            ATimesBShouldBe[1] = new double[] { 10, 18, 21, -17, 22 };
            ATimesBShouldBe[2] = new double[] { 6, -22, -2, -10, 12 };
            ATimesBShouldBe[3] = new double[] { -13, -1, -7, 6, -9 };

            CTransposedShouldBe = new Matrix(5, 4);
            CTransposedShouldBe[0] = new double[] { -16, 10, 6, -13 };
            CTransposedShouldBe[1] = new double[] { -4, 18, -22, -1 };
            CTransposedShouldBe[2] = new double[] { -29, 21, -2, -7 };
            CTransposedShouldBe[3] = new double[] { 37, -17, -10, 6 };
            CTransposedShouldBe[4] = new double[] { -46, 22, 12, -9 };

            D = new Matrix(3, 3);
            D[0] = new double[] { 1, 2, 4 };
            D[1] = new double[] { 2, 4, 8 };
            D[2] = new double[] { 4, 8, 16 };

            E = new Matrix(3, 3);
            E[0] = new double[] { 4, 2, 1 };
            E[1] = new double[] { 8, 4, 2 };
            E[2] = new double[] { 16, 8, 4 };

            // used to validate result of D + E in tests
            DPlusEShouldBe = new Matrix(3, 3);
            DPlusEShouldBe[0] = new double[] { 5, 4, 5 };
            DPlusEShouldBe[1] = new double[] { 10, 8, 10 };
            DPlusEShouldBe[2] = new double[] { 20, 16, 20 };

            // used to validate result of D - E in tests
            DMinusEShouldBe = new Matrix(3, 3);
            DMinusEShouldBe[0] = new double[] { -3, 0, 3 };
            DMinusEShouldBe[1] = new double[] { -6, 0, 6 };
            DMinusEShouldBe[2] = new double[] { -12, 0, 12};
        }
    }
}
