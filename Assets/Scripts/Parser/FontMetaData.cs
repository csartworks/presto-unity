using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presto.parser
{
    public class FontMetaData
    {
        public readonly string fontName;
        public readonly float fontVersion;
        public readonly Dictionary<string, float> engravingDefaults = new();
        public readonly string[] textFontFamily;
        public readonly Dictionary<string, float> glyphAdvanceWidths = new();

        public FontMetaData(string fontName, float fontVersion, Dictionary<string, float> engravingDefaults, string[] textFontFamily, Dictionary<string, float> glyphAdvanceWidths)
        {
            this.fontName = fontName;
            this.fontVersion = fontVersion;
            this.engravingDefaults = engravingDefaults;
            this.textFontFamily = textFontFamily;
            this.glyphAdvanceWidths = glyphAdvanceWidths;
        }
    }
}
