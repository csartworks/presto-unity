using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

namespace presto.parser
{
    public static class MetaDataParser
    {
        public static FontMetaData Parse(string s)
        {
            using StringReader reader = new(s);
            if (reader.ReadLine() != "{") throw new Exception("Expected { at the start of json");
            string fontName = ParseString(reader.ReadLine());
            float fontVersion = ParseFloat(reader.ReadLine());
            Dictionary<string, float> engravingDefaults = new();
            Dictionary<string, float> glyphAdvanceWidths = new();
            List<string> textFontFamily = new();

            //parse engravingDefaults
            string engravHeader = reader.ReadLine();
            if (engravHeader[2] == 'e')
            {
                while (reader.Peek() >= 0)
                {
                    string line = reader.ReadLine();
                    if (line[1] == '}') break;
                    if (line.EndsWith('['))
                    {
                        while (reader.Peek() >= 0)
                        {
                            line = reader.ReadLine();
                            if (line.EndsWith("],")) break;
                            textFontFamily.Add(ParsePropName(line));
                        }
                        line = reader.ReadLine();
                    }
                    engravingDefaults.Add(ParsePropName(line), ParseFloat(line));
                }
            }
            //parse glyphAdvanceWidths
            //parse glyphBBoxes
            //parse glyphWithAlternates
            //parse glyphWithAnchors
            //parse ligatures
            //parse optionalGlyphs
            //parse sets
            return new(fontName, fontVersion, engravingDefaults, textFontFamily.ToArray(), glyphAdvanceWidths);
        }
        static string ParsePropName(string s)
        {
            int index1 = s.IndexOf('"') + 1;
            int index2 = s.LastIndexOf('"');
            return s[index1..index2];
        }
        static string ParseString(string s)
        {
            int index = s.LastIndexOf('"');
            int index2 = s.LastIndexOf('"', index - 1) + 1;
            return s[index2..index];
        }
        static float ParseFloat(string s)
        {
            int index = s.LastIndexOf(':') + 1;
            return float.Parse(s[index..^1]);
        }
    }
}
