using System;
using System.Diagnostics;

namespace Matrix.Tests.Utils
{
    static class StringUtils
    {
        [Conditional("DEBUG")]
        public static void PrintTitle(string msg, char borderChar = '*', int width = 145, char padChar = ' ', int padSize = 1, bool convToUpper = true)
        {
            var nl = Environment.NewLine;
            var spaces = (width - (msg.Length*(1+padSize))) / ((1+padSize)*2);
            var spacedMsg = new string(' ', spaces) + msg + new string(' ', spaces);
            var border = nl + new String(borderChar, spacedMsg.Length) + nl;
            var fullMsg = border + spacedMsg + border;
            Debug.WriteLine(StretchText(fullMsg, padChar:padChar, padSize:padSize, convToUpper:convToUpper));
        }

        public static string StretchText(string msg, char padChar = ' ', int padSize = 1, bool convToUpper = false)
        {
            var newText = "";
            for (var i = 0; i < msg.Length; i++)
            {
                newText += msg[i] + new string(padChar, padSize);
            }

            return convToUpper ? newText.ToUpper() : newText;
        }

        public static string FormatDimentions(int rows, int cols, int padLength = 6)
        {
            return string.Format("{0:#,###}", rows).PadLeft(padLength) + " x " + string.Format("{0:#,###}", cols).PadLeft(padLength);
        }
    }
}
