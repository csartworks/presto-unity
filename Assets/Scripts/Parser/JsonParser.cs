using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace presto.parser
{
    public static class JsonParser
    {
        public static Dictionary<string, GlyphName> Parse(string s)
        {
            Dictionary<string, GlyphName> result = new();
            using StringReader reader = new(s);
            if (reader.ReadLine() != "{") throw new Exception("Expected { at the start of json");
            while (reader.Peek() >= 0)
            {
                string name = reader.ReadLine();
                if (name[0] == '}') break;
                name = ReadName(name);

                char altCodepoint = '\0';
                char codepoint;
                string codepointLine = reader.ReadLine();
                if (codepointLine[3] == 'a')
                {
                    altCodepoint = ReadCodePoint(codepointLine);
                    codepointLine = reader.ReadLine();
                }
                codepoint = ReadCodePoint(codepointLine);

                string desc = reader.ReadLine();
                desc = ReadProp(desc);

                result.Add(name, new(altCodepoint, codepoint, desc));
                reader.ReadLine();
            }
            Debug.Log("[JsonParser] Parse successful");
            return result;

            static string ReadName(string s)
            {
                int index = s.LastIndexOf('"');
                return s[2..index];
            }
            static string ReadProp(string s)
            {
                int index = s.LastIndexOf('"');
                int index2 = s.LastIndexOf('"', index - 1) + 1;
                return s[index2..index];
            }
            static char ReadCodePoint(string s)
            {
                s = ReadProp(s)[2..];
                return (char)Convert.ToInt32(s, 16);
            }
        }
    }
}