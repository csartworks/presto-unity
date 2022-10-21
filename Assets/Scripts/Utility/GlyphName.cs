using System;

[Serializable]
public class GlyphName
{
    public readonly char AlternateCodepoint;
    public readonly char Codepoint;
    public readonly string Description;

    public GlyphName(char alternateCodepoint, char codepoint, string description)
    {
        AlternateCodepoint = alternateCodepoint;
        Codepoint = codepoint;
        Description = description;
    }
    public override string ToString()
    {
        return $"{Codepoint} {Description}";
    }
}