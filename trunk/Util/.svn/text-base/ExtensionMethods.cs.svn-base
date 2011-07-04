using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickBooks.Util
{
    public static class ExtensionMethods
    {
        public static string PrependNewLine(this string input)
        {
            
            if (input.StartsWith(Environment.NewLine))
                return input;
            else return Environment.NewLine + input;
        }

        public static string RemoveLineBreaks(this string input)
        {
            input = input.Replace(Environment.NewLine, "")
                .Replace("\n", "");
            return input;
        }

        public static string DecimalToString(this int input)
        {
            if (input == 0)
                return "";
            else return input.ToString();
        }
    }
}
