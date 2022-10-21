using UnityEngine;

public class Staff : MonoBehaviour
{
    private Glyph _lastGlyph;
    public void Add(Glyph glyph)
    {
        print(glyph);
        if (_lastGlyph is not null)
        {
            glyph.X = _lastGlyph.X + _lastGlyph.Width;
            print(_lastGlyph.X + " " + _lastGlyph.Width);
        }
        _lastGlyph = glyph;
    }
}
