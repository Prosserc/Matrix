using System;
using System.Diagnostics;
using Matrix.Tests;

namespace Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            //new TestRunner();
            var x = new Matrix(5, 8, true);
            var y = new Matrix(5, 8, false, 10);
            y.T();
            Debug.WriteLine(string.Format("Matrix x set to:{0}{1}", Environment.NewLine, x));
            Debug.WriteLine(string.Format("Matrix y set to:{0}{1}", Environment.NewLine, y));
            
            var z = x*y;
            Debug.WriteLine(string.Format("Matrix x * Matrix y is:{0}{1}", Environment.NewLine, z));
        }
    }
}
