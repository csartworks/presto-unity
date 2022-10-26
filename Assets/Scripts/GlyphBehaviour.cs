using System.Collections.Generic;
using presto.parser;
using UnityEditor;
using UnityEngine;

namespace presto.unity
{
    public class GlyphBehaviour : MonoBehaviour
    {
        public static float EM(float v) => GlobalFontSize * v;
        public static float SS(float v) => GlobalFontSize * 0.25f * v;
        public static float GlobalFontSize { get; protected set; } = 100;

        public GameObject Staff1LinePrefab => Main.instance.Staff1Line;
        public GameObject LegerPrefab => Main.instance.Leger;
        public GameObject BarlineSinglePrefab => Main.instance.BarlineSingle;
        public GameObject GlyphPrefab => Main.instance.Glyph;
        public GameObject NotePrefab => Main.instance.Note;
        public static GameObject BeamPrefab => Main.instance.Beam;

        public static Dictionary<string, float> engv;
        public static Dictionary<string, GlyphName> glyphs;

    }
}
