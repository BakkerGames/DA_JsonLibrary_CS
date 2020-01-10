using System;
using System.Collections;
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

        public static string IndentSpace(int indentLevel)
        {
            // Purpose: Return a string with the proper number of space chars
            // Author : Scott Bakker
            // Created: 09/13/2019
            if (indentLevel <= 0)
            {
                return "";
            }
            return new string(' ', indentLevel * _indentSpaceSize);
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

            if (value == null)
            {
                return "null";
            }

            Type t = value.GetType();

            // Check for generic list types
            if (t.IsGenericType)
            {
                StringBuilder result = new StringBuilder();
                result.Append("[");
                if (indentLevel >= 0)
                {
                    indentLevel++;
                }
                bool addComma = false;
                foreach (object obj in (IEnumerable)value)
                {
                    if (addComma)
                    {
                        result.Append(",");
                    }
                    else
                    {
                        addComma = true;
                    }
                    if (indentLevel >= 0)
                    {
                        result.AppendLine();
                        result.Append(IndentSpace(indentLevel));
                    }
                    result.Append(ValueToString(obj));
                }
                if (indentLevel > 0)
                {
                    indentLevel--;
                    result.AppendLine();
                    result.Append(IndentSpace(indentLevel));
                }
                result.Append("]");
                return result.ToString();
            }

            // Check for byte array, return as hex string "0x00..."
            if (t.IsArray && t == typeof(byte[]))
            {
                StringBuilder result = new StringBuilder();
                result.Append("0x");
                foreach (byte b in (byte[])value)
                {
                    result.Append(b.ToString("x2", null));
                }
                return result.ToString();
            }

            // Check for array, return in JArray format
            if (t.IsArray)
            {
                StringBuilder result = new StringBuilder();
                result.Append("[");
                if (indentLevel >= 0)
                {
                    indentLevel++;
                }
                bool addComma = false;
                for (int i = 0; i < ((Array)value).Length; i++)
                {
                    if (addComma)
                    {
                        result.Append(",");
                    }
                    else
                    {
                        addComma = true;
                    }
                    if (indentLevel >= 0)
                    {
                        result.AppendLine();
                        result.Append(IndentSpace(indentLevel));
                    }
                    object obj = ((Array)value).GetValue(i);
                    result.Append(ValueToString(obj));
                }
                if (indentLevel > 0)
                {
                    indentLevel--;
                    result.AppendLine();
                    result.Append(IndentSpace(indentLevel));
                }
                result.Append("]");
                return result.ToString();
            }

            // Check for individual types
            if (t == typeof(string))
            {
                StringBuilder result = new StringBuilder();
                result.Append("\"");
                foreach (char c in (string)value)
                {
                    result.Append(ToJsonChar(c));
                }
                result.Append("\"");
                return result.ToString();
            }
            if (t == typeof(char))
            {
                StringBuilder result = new StringBuilder();
                result.Append("\"");
                result.Append(ToJsonChar((char)value));
                result.Append("\"");
                return result.ToString();
            }
            if (t == typeof(Guid))
            {
                return $"\"{value.ToString()}\"";
            }
            if (t == typeof(bool))
            {
                if ((bool)value)
                {
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
            if (t == typeof(DateTime))
            {
                DateTime d = (DateTime)value;
                if (d.Hour + d.Minute + d.Second + d.Millisecond == 0)
                {
                    return $"\"{d.ToString(_dateFormat, null)}\"";
                }
                if (d.Year + d.Month + d.Day == 0)
                {
                    if (d.Millisecond == 0)
                    {
                        return $"\"{d.ToString(_timeFormat, null)}\"";
                    }
                    else
                    {
                        return $"\"{d.ToString(_timeMilliFormat, null)}\"";
                    }
                }
                if (d.Millisecond == 0)
                {
                    return $"\"{d.ToString(_dateTimeFormat, null)}\"";
                }
                else
                {
                    return $"\"{d.ToString(_dateTimeMilliFormat, null)}\"";
                }
            }
            if (t == typeof(DateTimeOffset))
            {
                return $"\"{((DateTimeOffset)value).ToString(_dateTimeOffsetFormat, null)}\"";
            }
            if (t == typeof(JObject))
            {
                return ((JObject)value).ToStringFormatted(ref indentLevel);
            }
            if (t == typeof(JArray))
            {
                return ((JArray)value).ToStringFormatted(ref indentLevel);
            }
            if (t == typeof(byte) ||
                t == typeof(sbyte) ||
                t == typeof(short) ||
                t == typeof(int) ||
                t == typeof(long) ||
                t == typeof(ushort) ||
                t == typeof(uint) ||
                t == typeof(ulong) ||
                t == typeof(float) ||
                t == typeof(double) ||
                t == typeof(decimal))
            {
                // Let ToString do all the work
                return value.ToString();
            }

            throw new SystemException($"JSON Error: Unknown object type: {t.ToString()}");
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
