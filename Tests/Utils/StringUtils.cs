using System;
using System.Diagnostics;
using System.Linq;

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

        /// <summary>Returns the string passed in [msg] with a character [padChar] inserted [padSize] times after each character in [msg].</summary>
        /// <param name="msg">string - the text to be stretched</param>
        /// <param name="padChar">optional char - the character to be inserted after each character in the msg string</param>
        /// <param name="padSize">optional int - the number of times the padChar should be inserted after each character</param>
        /// <param name="convToUpper">optional bool - if set to true the return value will be converted to uppercase</param>
        /// <returns>String stretech in line with parameters passed in.</returns>
        public static string StretchText(string msg, char padChar = ' ', int padSize = 1, bool convToUpper = false)
        {
            var newText = msg.Aggregate("", (current, t) => current + (t + new string(padChar, padSize)));
            return convToUpper ? newText.ToUpper() : newText;
        }

        /// <summary>LeftPads each dimention to right align numbers.</summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <param name="padLength">optional int - required width of each dimention (incl padding chars)</param>
        /// <param name="padChar">optional char - char used for padding</param>
        /// <param name="delimiter">optional string - string used to separate each dimention</param>
        /// <returns>String formatted to right align each dimention.</returns>
        public static string FormatDimentions(int rows, int cols, int padLength = 6, char padChar = ' ', string delimiter = " x ")
        {
            return FormatDimentions(new[] {rows, cols}, padLength, padChar, delimiter);
        }

        /// <summary>LeftPads each dimention to right align numbers.</summary>
        /// <param name="dimentions">int[] - array of integers representing dimentioned e.g. new[] {3, 3} for 3 x 3</param>
        /// <param name="padLength">optional int - required width of each dimention (incl padding chars)</param>
        /// <param name="padChar">optional char - char used for padding</param>
        /// <param name="delimiter">optional string - string used to separate each dimention</param>
        /// <returns>String formatted to right align each dimention.</returns>
        public static string FormatDimentions(int[] dimentions, int padLength = 6, char padChar = ' ', string delimiter = " x ")
        {
            var str = "";
            for (var i = 0; i < dimentions.Length; i++ )
            {
                str += string.Format("{0:#,###}", dimentions[i]).PadLeft(padLength, padChar) + (i == dimentions.Length-1 ? "" : delimiter);
            }
            return str;
        }
    }
}
