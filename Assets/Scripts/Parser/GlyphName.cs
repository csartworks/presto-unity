using System;

namespace presto.parser
{
    [Serializable]
    public class GlyphName
    {
        public readonly char AlternateCodepoint;
        public readonly char Codepoint;
        public readonly string Description;
        public readonly string String;

        public GlyphName(char alternateCodepoint, char codepoint, string description)
        {
            AlternateCodepoint = alternateCodepoint;
            Codepoint = codepoint;
            Description = description;
            String = codepoint.ToString();
        }
        public override string ToString()
        {
            return $"{Codepoint} {Description}";
        }
    }
}