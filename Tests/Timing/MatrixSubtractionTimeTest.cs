﻿using System.Globalization;
using Matrix.Tests;
using Matrix.Tests.Utils;
using System;
using System.Diagnostics;

namespace Matrix.Tests.Timing
{
    static class MatrixSubtractionTimeTest
    {
        // shorthand for constructor with square Matrices
        public static void Subtract(int squareMatrixDimentions, TablePrinter tab = null)
        {
            Subtract(squareMatrixDimentions, squareMatrixDimentions, tab);
        }

        public static void Subtract(int rows, int cols, TablePrinter tab = null)
        {
            // prep data
            var m1 = new Matrix(rows, cols, initialiseWithRandomValues: true);
            var m2 = new Matrix(rows, cols, initialiseWithRandomValues: true);

            // time the multiplication
            var watch = Stopwatch.StartNew();
            var m3 = m1 - m2;
            var ms = watch.ElapsedMilliseconds;
            watch.Stop();

            // report results
            if (tab != null)
                tab.AddRow(new[] { "Subtraction", StringUtils.FormatDimentions(rows, cols), null, ms.ToString("#,###") });
        }
    }
}
