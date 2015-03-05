using Matrix.RefData;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Matrix.Utils
{
    class TablePrinter
    {
        private string[] _heads;
        private int[] _colWidths;
        private int _tableWidth;
        private Queue<string> _rows;
        private string[] p1;
        private int p2;

        /// <summary>Object to capture tabular data and output to the debug console.</summary>
        /// <param name="heads">string[] - array of string values for table headings</param>
        /// <param name="colWidths">int - standard width for all cols (use other constructor for sepcific widths)</param>
        public TablePrinter(string[] heads, int colWidths = 20)
        {
            var colWidthsArray = new int[heads.Length];
            for (var i = 0; i < heads.Length; i++) colWidthsArray[i] = colWidths;
            Initialise(heads, colWidthsArray);
        }

        /// <summary>Object to capture tabular data and output to the debug console.</summary>
        /// <param name="heads">string[] - array of string values for table headings</param>
        /// <param name="colWidths">int[] - array of integers representing required width for each column</param>
        public TablePrinter(string[] heads, int[] colWidths)
        {
            Initialise(heads, colWidths);
        }

        public void Print()
        {
            AddVerticalBoundaryRow();
            foreach (var row in _rows) Debug.WriteLine(row);
        }

        public void AddRow(string[] rowData, Alignment alignment = Alignment.Left)
        {
            var alignmentArray = new Alignment[rowData.Length];
            for (var i = 0; i < rowData.Length; i++) alignmentArray[i] = alignment;
            AddRow(rowData, alignmentArray);
        }

        public void AddRow(string[] rowData, Alignment[] alignments)
        {
            var n = _heads.Length;
            var rowBoundary = " | ";
            var str = "| ";
            for (var i = 0; i < n; i++)
            {
                str += (alignments[i] == (Alignment.Left) ? rowData[i].PadRight(_colWidths[i]) : rowData[i].PadLeft(_colWidths[i])) + rowBoundary;
            }
            AddRow(str);
        }

        private void AddRow(string str)
        {
            _rows.Enqueue(str);
        }

        private void Initialise(string[] heads, int[] colWidths)
        {
            _rows = new Queue<string>();
            _heads = heads;
            _colWidths = colWidths;
            _tableWidth = GetTableWidth();
            AddHeads();
        }

        public void AddVerticalBoundaryRow()
        {
            AddRow(string.Format("{0}{1}{2}", "+", new string('-', _tableWidth-2), "+"));
        }

        private void AddHeads()
        {
            AddVerticalBoundaryRow();
            AddRow(_heads);
            AddVerticalBoundaryRow();
        }

        private int GetTableWidth()
        {
            var n = _colWidths.Length;
            var tableWidth = 1; // allow for first col
            for (var i = 0; i < n; i++) tableWidth += (_colWidths[i] + 3);
            return tableWidth;
        }
    }
}
