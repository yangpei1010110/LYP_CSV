using System;
using System.Collections.Generic;
using System.Linq;

namespace LYP_Utils
{
    public static class LCsv
    {
        /// <summary>
        ///     split the csv string line but not keep the quotation marks
        ///     For example, "a","b","c" will be parsed as a, b, c
        ///     The quotation marks can be represented by "" such as "a""b","c" will be parsed as a"b, c
        /// </summary>
        public static IReadOnlyList<string> ParseIgnoreQuotation(string csv, char key = ',')
        {
            string[] result = Parse(csv, key).ToArray();
            for (int i = 0; i < result.Length; i++)
            {
                string s = result[i];
                if (string.IsNullOrWhiteSpace(s))
                {
                    result[i] = s;
                }
                else
                {
                    char firstChar = s[0];
                    char lastChar = s[s.Length - 1];
                    if (firstChar == '"' && lastChar == '"')
                    {
                        if (s.Length <= 2)
                        {
                            result[i] = string.Empty;
                        }
                        else
                        {
                            result[i] = s.Substring(1, s.Length - 2);
                        }
                    }
                    else
                    {
                        result[i] = s;
                    }
                }
            }

            return result;
        }

        /// <summary>
        ///     split the csv string line but keep the quotation marks
        ///     For example, "a","b","c" will be parsed as "a", "b", "c"
        ///     The quotation marks can be represented by "" such as "a""b","c" will be parsed as "a"b", "c"
        /// </summary>
        public static IReadOnlyList<string> Parse(string csv, char key = ',')
        {
            char[] charArray = csv.ToCharArray();
            List<int> quotationIndexes = new List<int>();
            List<int> keyIndexes = new List<int>();
            for (int i = 0; i < charArray.Length; i++)
            {
                if (charArray[i] == '"')
                {
                    quotationIndexes.Add(i);
                }
                else if (charArray[i] == key)
                {
                    keyIndexes.Add(i);
                }
            }

            if (quotationIndexes.Count % 2 != 0)
            {
                throw new Exception("Invalid CSV format");
            }

            if (keyIndexes.Count <= 0)
            {
                return new[] { csv, };
            }

            List<string> result = new List<string>();
            int beginIndex = 0;
            int quotationIndex = 0;
            bool isInnerString = false;
            string subString;
            for (int i = 0; i < keyIndexes.Count; i++)
            {
                int commaIndex = keyIndexes[i];
                while (quotationIndex < quotationIndexes.Count && quotationIndexes[quotationIndex] < commaIndex)
                {
                    bool isDoubleQuotation = quotationIndex + 1 < quotationIndexes.Count
                        ? quotationIndexes[quotationIndex] == quotationIndexes[quotationIndex + 1]
                        : false;
                    if (isDoubleQuotation)
                    {
                        quotationIndex += 2;
                    }
                    else
                    {
                        isInnerString = !isInnerString;
                        quotationIndex += 1;
                    }
                }

                if (!isInnerString)
                {
                    subString = new string(charArray, beginIndex, commaIndex - beginIndex)
                               .Replace(@"""""", @"""")
                               .Trim();
                    result.Add(subString);
                    beginIndex = commaIndex + 1;
                }
            }

            subString = new string(charArray, beginIndex, charArray.Length - beginIndex)
                       .Replace(@"""""", @"""")
                       .Trim();
            result.Add(subString);
            return result;
        }
    }
}