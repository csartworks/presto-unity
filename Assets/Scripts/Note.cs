using TMPro;
using UnityEngine;

namespace presto.unity
{
    public class Note : GlyphBehaviour
    {
        [SerializeField] private RectTransform _stem;
        [SerializeField] private RectTransform _flag;
        [SerializeField] private RectTransform _glyph;
        [SerializeField] private TMP_Text _glyphText;
        public RectTransform Stem => _stem;
        public RectTransform Flag => _flag;
        public RectTransform Rt { get; set; }
        public int Pitch { get; private set; }
        public int Len { get; private set; }
        public Beam Beam { get; set; }
        public static float stemH = SS(3.5f) - SS(0.168f);

        public void Init(Staff staff, string len, int pitch, bool appendToRt = true)
        {
            Rt = GetComponent<RectTransform>();
            Pitch = pitch;
            int.TryParse(len, out int lenInt);
            Len = lenInt;

            if (Len == 1) _glyphText.text = glyphs["noteheadWhole"].String;
            if (Len == 2) _glyphText.text = glyphs["noteheadHalf"].String;

            if (pitch < 0) DrawLeger();
            DrawStem();
            DrawFlag();
            // DrawBeam();

            var y = SS(pitch / 2f);
            if (appendToRt) staff.AppendToRts(Rt, y);
        }
        public void DrawFlag()
        {
            var flagText = string.Empty;
            if (Len >= 8) flagText = glyphs[$"flag{Len}thUp"].Codepoint.ToString();
            _flag.GetComponent<TMP_Text>().text = flagText;
            _flag.anchoredPosition = new(0, SS(-0.088f));
        }
        public void DrawBeam()
        {
            var beam = Instantiate(Beam, Rt).GetComponent<RectTransform>();
            float stemH = SS(3.5f);
            beam.localPosition = new(SS(1.18f), stemH);
            // beam.sizeDelta = new()
        }
        public void DrawStem()
        {
            _stem.sizeDelta = new(SS(engv["stemThickness"]), stemH);
            _stem.localPosition = new(SS(1.18f), SS(0.168f));
        }
        public void DrawLeger()
        {
            int iter = Mathf.Abs(Pitch) / 2;
            int mod = Mathf.Abs(Pitch) % 2;
            for (var i = 0; i < iter; i++)
            {
                var leger = Instantiate(LegerPrefab, Rt).GetComponent<RectTransform>();
                var setx = _glyph.localPosition.x + _glyph.sizeDelta.x / 2;
                var sety = SS(i);
                if (mod != 0) sety += SS(0.5f);
                leger.localPosition = new(setx, sety);
                leger.sizeDelta = new(SS(1.648f), SS(0.16f));
            }
        }
    }
}