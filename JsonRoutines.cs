using System;
using System.Text;

namespace DA_JsonLibrary_CS
{
    public static class JsonRoutines
    {

        private const string _dateFormat = "yyyy-MM-dd";
        private const string _timeFormat = "HH:mm:ss";
        private const string _timeMilliFormat = "HH:mm:ss.fff";
        private const string _dateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        private const string _dateTimeMilliFormat = "yyyy-MM-dd HH:mm:ss.fff";
        private const string _dateTimeOffsetFormat = "O";

        private const int _indentSpaceSize = 2;

        internal static string IndentSpace(int indentLevel)
        {
            if (indentLevel < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (indentLevel == 0)
            {
                return "";
            }
            StringBuilder result = new StringBuilder(indentLevel * _indentSpaceSize);
            result.Insert(0, " ", indentLevel * _indentSpaceSize);
            return result.ToString();
        }

        public static string ValueToString(object value)
        {
            // Purpose: Return a value in proper JSON string format
            // Author : Scott Bakker
            // Created: 09/13/2019
            int indentLevel = -1; // don't indent
            return ValueToString(value, ref indentLevel);
        }

        internal static string ValueToString(object value, ref int indentLevel)
        {
            // Purpose: Return a value in proper JSON string format
            // Author : Scott Bakker
            // Created: 09/13/2019
            throw new NotImplementedException();
        }

        internal static string FromJsonString(string value)
        {
            // Purpose: Convert a string with escaped characters into control codes
            // Author : Scott Bakker
            // Created: 09/17/2019
            throw new NotImplementedException();
        }

        internal static string GetToken(ref int pos, string value)
        {
            // Purpose: Get a single token from string value for parsing
            // Author : Scott Bakker
            // Created: 09/13/2019
            // Notes  : Does not do escaped character expansion here, just passes exact value.
            //        : Properly handles \" within strings properly this way, but nothing else.
            throw new NotImplementedException();
        }

        internal static object JsonValueToObject(string value)
        {
            // Purpose: Convert a string representation of a value to an actual object
            // Author : Scott Bakker
            // Created: 09/13/2019
            throw new NotImplementedException();
        }

        internal static void SkipWhitespace(ref int pos, string value)
        {
            // Purpose: Skip over any whitespace characters or any recognized comments
            // Author : Scott Bakker
            // Created: 09/23/2019
            // Notes  : Comments consist of /*...*/ or // to eol (aka line comment)
            //        : An unterminated comment is not an error, it is just all skipped
            throw new NotImplementedException();
        }

        private static string ToJsonChar(char c)
        {
            // Purpose: Return a character in proper JSON format
            // Author : Scott Bakker
            // Created: 09/13/2019
            throw new NotImplementedException();
        }

        private static bool IsWhitespace(char c)
        {
            // Purpose: Check for recognized whitespace characters
            // Author : Scott Bakker
            // Created: 09/13/2019
            throw new NotImplementedException();
        }

        private static bool IsJsonSymbol(char c)
        {
            // Purpose: Check for recognized JSON symbol chars which are tokens by themselves
            // Author : Scott Bakker
            // Created: 09/13/2019
            throw new NotImplementedException();
        }

        private static bool IsJsonValueChar(char c)
        {
            // Purpose: Check for any valid characters in a non-string value
            // Author : Scott Bakker
            // Created: 09/23/2019
            throw new NotImplementedException();
        }
    }
}
