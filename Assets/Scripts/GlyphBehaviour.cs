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
        public static readonly char NoteHeadWhole = (char)0xE0A2;
        public static string NoteHead(int len) => (NoteHeadWhole + len).ToString();
        public static readonly int RestWhole = 0xE4E3;
        public static string Rest(int len) => ((char)(RestWhole + len)).ToString();

        public static GameObject Staff1LinePrefab => Main.instance.Staff1Line;
        public static GameObject LegerPrefab => Main.instance.Leger;
        public static GameObject BarlineSinglePrefab => Main.instance.BarlineSingle;
        public static GameObject GlyphPrefab => Main.instance.Glyph;
        public static GameObject NotePrefab => Main.instance.Note;
        public static GameObject BeamPrefab => Main.instance.Beam;

        public static Dictionary<string, float> engv;
        public static Dictionary<string, GlyphName> glyphs;

    }
}
