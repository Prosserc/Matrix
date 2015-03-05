using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Matrix.Tests.Utils
{
    class TablePrinter
    {
        private string[] _heads;
        private int[] _colWidths;
        private Alignment[] _colAlignments;
        private Queue<string> _rows;
        private const char RowBoundary = '-'; // e.g. '-'
        private const string ColBoundary = "     "; // e.g. "  |  "
        private const string RowColBoundary = "-----"; // e.g. "--+--"
        private const Alignment DefaultAlignment = Alignment.Left;
        private const bool CentreOnPage = false;
        private const int RowWidth = 145;

        /// <summary>Object to capture tabular data and output to the debug console.</summary>
        /// <param name="heads">string[] - array of string values for table headings</param>
        /// <param name="colWidths">int - standard width for all cols (use other constructor for sepcific widths)</param>
        /// <param name="alignments">optional alignments[] - array of alignments to be applied to table, leave blank for defaults</param>
        /// <param name="headsAlignments">optional alignments[] - array of alignments to be applied to heads, leave blank for defaults</param>
        public TablePrinter(string[] heads, int colWidths = 16, Alignment[] alignments = null, Alignment[] headsAlignments = null)
        {
            var colWidthsArray = new int[heads.Length];
            for (var i = 0; i < heads.Length; i++) colWidthsArray[i] = colWidths;
            Initialise(heads, colWidthsArray, alignments, headsAlignments);
        }

        /// <summary>Object to capture tabular data and output to the debug console.</summary>
        /// <param name="heads">string[] - array of string values for table headings</param>
        /// <param name="colWidths">int[] - array of integers representing required width for each column</param>
        /// <param name="alignments">optional alignments[] - array of alignments to be applied to table, leave blank for defaults</param>
        /// <param name="headsAlignments">optional alignments[] - array of alignments to be applied to heads, leave blank for defaults</param>
        public TablePrinter(string[] heads, int[] colWidths, Alignment[] alignments = null, Alignment[] headsAlignments = null)
        {
            Initialise(heads, colWidths, alignments, headsAlignments);
        }

        [Conditional("DEBUG")]
        public void Print()
        {
            AddVerticalBoundaryRow(); // close bottom of table
            Debug.Write(Environment.NewLine);
            foreach (var row in _rows) Debug.WriteLine(row);
        }

        /// <summary>Add row to printable table. Exclude alignments to use those already specified in constructor.</summary>
        /// <param name="rowData">string[] - array of string values to be written to table</param>
        /// <param name="alignments">optional alignment[] - only provide if you want to override those set in constructor</param>
        public void AddRow(string[] rowData, Alignment[] alignments = null)
        {
            var n = _heads.Length;
            var str = ColBoundary.TrimStart(' ');
            var useAlignments = alignments ?? _colAlignments; // use specific if provided
            for (var i = 0; i < n; i++)
            {
                var val = rowData[i] ?? "";
                switch (useAlignments[i])
                {
                    case Alignment.Left:
                        str += val.PadRight(_colWidths[i]);
                        break;
                    case Alignment.Right:
                        str += val.PadLeft(_colWidths[i]);
                        break;
                    case Alignment.Centre:
                        var leftPadding = new string(' ', (_colWidths[i] - val.Length) / 2);
                        var rightPadding = new string(' ', _colWidths[i] - (leftPadding.Length + val.Length));
                        str += (leftPadding + val + rightPadding);
                        break;
                }
                str += ColBoundary;
            }
            AddRow(str.TrimEnd(' '));
        }

        private void AddRow(string str)
        {
            var rowPrefix = CentreOnPage ? new string(' ', (RowWidth - str.Length) / 2) : "";
            _rows.Enqueue(rowPrefix + str);
        }

        private void Initialise(string[] heads, int[] colWidths, Alignment[] alignments = null, Alignment[] headsAlignments = null)
        {
            _rows = new Queue<string>();
            _heads = heads;
            _colWidths = colWidths;
            _colAlignments = alignments ?? DefaultAlignments();
            AddHeads(headsAlignments ?? DefaultAlignments());
        }

        public void AddVerticalBoundaryRow()
        {
            var str = RowColBoundary.TrimStart(RowBoundary);
            for (var i = 0; i < _heads.Length; i++) str += new string(RowBoundary, _colWidths[i]) + RowColBoundary;
            AddRow(str.TrimEnd(RowBoundary));
        }

        private void AddHeads(Alignment[] alignments)
        {
            AddVerticalBoundaryRow();
            AddRow(_heads, alignments);
            AddVerticalBoundaryRow();
        }

        private Alignment[] DefaultAlignments()
        {
            var a = new Alignment[_heads.Length];
            for (var i = 0; i < _heads.Length; i++) a[i] = DefaultAlignment;
            return a;
        }
    }
}
