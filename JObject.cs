﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DA_JsonLibrary_CS
{
    public class JObject : IEnumerable<string>
    {
        private Dictionary<string, object> _data;

        public JObject()
        {
            // Purpose: Create new JObject object
            // Author : Scott Bakker
            // Created: 09/13/2019
            _data = new Dictionary<string, object>();
        }

        public IEnumerator<string> GetEnumerator()
        {
            // Purpose: Provide IEnumerable access directly to _data.Keys
            // Author : Scott Bakker
            // Created: 09/13/2019
            return _data.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            // Purpose: Provide IEnumerable access directly to _data.Keys
            // Author : Scott Bakker
            // Created: 09/13/2019
            return _data.Keys.GetEnumerator();
        }

        public void Add(string key, object value)
        {
            // Purpose: Adds a new key/value pair to JObject
            // Author : Scott Bakker
            // Created: 09/13/2019
            // Changes: 10/03/2019 Removed extra string processing, was wrong
            // Notes  : Throws an error if the key already exists.
            if (string.IsNullOrEmpty(key))
            {
                throw new SystemException($"JSON Error: Key cannot be null or empty");
            }
            if (_data.ContainsKey(key))
            {
                throw new SystemException($"JSON Error: Key already exists: {key}");
            }
            _data.Add(key, value);
        }

        public void Clear()
        {
            // Purpose: Clears all items from the current JObject
            // Author : Scott Bakker
            // Created: 09/13/2019
            _data.Clear();
        }

        public bool Contains(string key)
        {
            // Purpose: Identifies whether a key exists in the current JObject
            // Author : Scott Bakker
            // Created: 09/13/2019
            return _data.ContainsKey(key);
        }

        public int Count()
        {
            // Purpose: Return the count of items in the JObject
            // Author : Scott Bakker
            // Created: 09/13/2019
            return _data.Count;
        }

        public object this[string key]
        {
            // Purpose: Give access to item values by key
            // Author : Scott Bakker
            // Created: 09/13/2019
            get
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new SystemException($"JSON Error: Key cannot be null or empty");
                }
                if (!_data.ContainsKey(key))
                {
                    throw new SystemException($"JSON Error: Key not found: {key}");
                }
                return _data[key];
            }
            set
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new SystemException($"JSON Error: Key cannot be null or empty");
                }
                if (!_data.ContainsKey(key))
                {
                    throw new SystemException($"JSON Error: Key not found: {key}");
                }
                _data[key] = value;
            }
        }

        public object GetItemOrNull(string key)
        {
            // Purpose: Return item value by key, or return null if missing
            // Author : Scott Bakker
            // Created: 09/20/2019
            if (string.IsNullOrEmpty(key))
            {
                throw new SystemException($"JSON Error: Key cannot be null or empty");
            }
            if (!_data.ContainsKey(key))
            {
                return null;
            }
            return _data[key];
        }

        public void Merge(JObject jo)
        {
            // Purpose: Merge a new JObject onto the current one
            // Author : Scott Bakker
            // Created: 09/17/2019
            // Notes  : If any keys are duplicated, the new value overwrites the current value
            if (jo != null)
            {
                foreach (string key in jo)
                {
                    if (_data.ContainsKey(key))
                    {
                        _data[key] = jo[key];
                    }
                    else
                    {
                        _data.Add(key, jo[key]);
                    }
                }
            }
        }

        public void Remove(string key)
        {
            // Purpose: Remove an item from a JObject
            // Author : Scott Bakker
            // Created: 09/13/2019
            if (string.IsNullOrEmpty(key))
            {
                throw new SystemException($"JSON Error: Key cannot be null or empty");
            }
            if (_data.ContainsKey(key))
            {
                _data.Remove(key);
            }
        }

        public override string ToString()
        {
            // Purpose: Convert a JObject into a string
            // Author : Scott Bakker
            // Created: 09/13/2019
            StringBuilder result = new StringBuilder();
            result.Append("{");
            bool addComma = false;
            foreach (KeyValuePair<string, object> kv in _data)
            {
                if (addComma)
                {
                    result.Append(",");
                }
                else
                {
                    addComma = true;
                }
                result.Append(JsonRoutines.ValueToString(kv.Key));
                result.Append(":");
                result.Append(JsonRoutines.ValueToString(kv.Value));
            }
            result.Append("}");
            return result.ToString();
        }

        public string ToStringSorted()
        {
            // Purpose: Sort the keys before returning as a string
            // Author : Scott Bakker
            // Created: 10/17/2019
            throw new NotImplementedException();
        }

        public string ToStringFormatted()
        {
            // Purpose: Convert this JObject into a string with formatting
            // Author : Scott Bakker
            // Created: 10/17/2019
            int indentLevel = 0;
            return ToStringFormatted(ref indentLevel);
        }

        internal string ToStringFormatted(ref int indentLevel)
        {
            // Purpose: Convert this JObject into a string with formatting
            // Author : Scott Bakker
            // Created: 10/17/2019
            if (_data.Count == 0)
            {
                return "{}";
            }
            StringBuilder result = new StringBuilder();
            result.Append("{");
            if (indentLevel >= 0)
            {
                indentLevel++;
                result.AppendLine();
            }
            bool addComma = false;
            foreach (KeyValuePair<string, object> kv in _data)
            {
                if (addComma)
                {
                    result.Append(",");
                    if (indentLevel >= 0)
                    {
                        result.AppendLine();
                    }
                }
                else
                {
                    addComma = true;
                }
                if (indentLevel >= 0)
                {
                    result.Append(JsonRoutines.IndentSpace(indentLevel));
                }
                result.Append(JsonRoutines.ValueToString(kv.Key));
                result.Append(":");
                if (indentLevel >= 0)
                {
                    result.Append(" ");
                }
                result.Append(JsonRoutines.ValueToString(kv.Value, ref indentLevel));
            }
            if (indentLevel >= 0)
            {
                result.AppendLine();
                if (indentLevel > 0)
                {
                    indentLevel--;
                }
                result.Append(JsonRoutines.IndentSpace(indentLevel));
            }
            result.Append("}");
            if (indentLevel == 0)
            {
                result.AppendLine();  // crlf at end of file
            }
            return result.ToString();
        }

        public static JObject Parse(string value)
        {
            // Purpose: Convert a string into a JObject
            // Author : Scott Bakker
            // Created: 09/13/2019
            int pos = 0;
            return Parse(ref pos, value);
        }

        public static JObject Parse(ref int pos, string value)
        {
            // Purpose: Convert a partial string into a JObject
            // Author : Scott Bakker
            // Created: 09/13/2019
            if (value == null || value.Length == 0)
            {
                return null;
            }
            JObject result = new JObject();
            JsonRoutines.SkipWhitespace(ref pos, value);
            if (value[pos] != '{')
            {
                throw new SystemException($"JSON Error: Unexpected token to start JObject: {value[pos]}");
            }
            pos++;
            do
            {
                JsonRoutines.SkipWhitespace(ref pos, value);
                // check for symbols
                if (value[pos] == '}')
                {
                    pos++;
                    break; // done building
                }
                if (value[pos] == ',')
                {
                    // this logic ignores extra commas, but is ok
                    pos++;
                }
                string tempKey = JsonRoutines.GetToken(ref pos, value);
                if (string.IsNullOrEmpty(tempKey) || tempKey == "\"\"")
                {
                    throw new SystemException($"JSON Error: Null or empty key");
                }
                if (!tempKey.StartsWith("\"") || !tempKey.EndsWith("\""))
                {
                    throw new SystemException($"JSON Error: Invalid key format: {tempKey}");
                }
                // Convert to usable key
                tempKey = JsonRoutines.JsonValueToObject(tempKey).ToString();
                // Check for ":" between key and value
                string tempColon = JsonRoutines.GetToken(ref pos, value);
                if (tempColon != ":")
                {
                    throw new SystemException($"JSON Error: Missing colon: {tempColon}");
                }
                // Get value
                JsonRoutines.SkipWhitespace(ref pos, value);
                if (value[pos] == '{') // JObject
                {
                    JObject jo = JObject.Parse(ref pos, value);
                    result.Add(tempKey, jo);
                }
                else if (value[pos] == '[') // JArray
                {
                    JArray ja = JArray.Parse(ref pos, value);
                    result.Add(tempKey, ja);
                }
                else
                {
                    // Get value as a string, convert to object
                    string tempValue = JsonRoutines.GetToken(ref pos, value);
                    result.Add(tempKey, JsonRoutines.JsonValueToObject(tempValue));
                }
            } while (true);
            return result;
        }

        public static JObject Clone(JObject jo)
        {
            // Purpose: Clones a JObject
            // Author : Scott Bakker
            // Created: 09/20/2019
            JObject result = new JObject();
            if (jo != null && jo._data != null)
            {
                result._data = new Dictionary<string, object>(jo._data);
            }
            return result;
        }
    }
}
