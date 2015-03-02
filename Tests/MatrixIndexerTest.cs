using System.Diagnostics;

namespace Matrix.Tests
{
    class MatrixIndexerTest
    {
        public MatrixIndexerTest()
        {
            var staticData = new StaticData();
            var a = staticData.A;

            // allows indexing to get cell directly on Matrix e.g.
            GetViaIndex(a);
            SetViaIndex(a);
        }

        private static void GetViaIndex(Matrix a)
        {
            Debug.WriteLine(string.Format("get a[0][1]: {0:0.######}", a[0][1]));
        }

        private static void SetViaIndex(Matrix a)
        {
            const double val = 3.145;
            Debug.WriteLine("set a[0][1] to 3.145");
            a[0][1] = val;
            Debug.WriteLine(string.Format("get a[0][1]: {0:0.######}", a[0][1]));
            Debug.Assert(a[0][1].Equals(val));
        }
    }
}
