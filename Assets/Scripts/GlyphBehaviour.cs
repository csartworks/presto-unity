using presto.parser;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace presto.unity
{
    public class GlyphBehaviour : MonoBehaviour
    {
        public float EM(float v) => GlobalFontSize * v;
        public float SS(float v) => GlobalFontSize * 0.25f * v;
        public static float GlobalFontSize { get; protected set; } = 100;

        public GameObject Staff1Line => Main.instance.Staff1Line;
        public GameObject Leger => Main.instance.Leger;
        public GameObject BarlineSingle => Main.instance.BarlineSingle;
        public GameObject Glyph => Main.instance.Glyph;
        public GameObject Note => Main.instance.Note;

        public static Dictionary<string, float> engv;
        public static Dictionary<string, GlyphName> glyphs;

    }
}
