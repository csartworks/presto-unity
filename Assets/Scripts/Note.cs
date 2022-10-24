using TMPro;
using UnityEngine;

namespace presto.unity
{
    public class Note : GlyphBehaviour
    {
        [SerializeField] private RectTransform _stem;
        [SerializeField] private RectTransform _flag;
        [SerializeField] private RectTransform _glyph;
        private RectTransform rt;
        public int Pitch { get; private set; }
        public int Len { get; private set; }
        public void Init(Staff staff, string len, int pitch)
        {
            rt = GetComponent<RectTransform>();
            Pitch = pitch;
            Len = int.Parse(len);

            if (pitch < 0) DrawLeger();
            DrawStem();
            DrawFlag();

            var y = SS(pitch * 0.5f);
            staff.AppendToRts(rt, y);

        }
        public void DrawFlag()
        {
            var flagText = string.Empty;
            if (Len >= 8) flagText = glyphs[$"flag{Len}thUp"].Codepoint.ToString();
            _flag.GetComponent<TMP_Text>().text = flagText;
            _flag.anchoredPosition = new(0, SS(-0.088f));
        }
        public void DrawStem()
        {
            float stemH = SS(3.5f) - SS(0.168f);
            _stem.sizeDelta = new(SS(engv["stemThickness"]), stemH);
            _stem.localPosition = new(SS(1.18f), SS(0.168f));
        }
        public void DrawLeger()
        {
            int iter = Mathf.Abs(Pitch) / 2;
            int mod = Mathf.Abs(Pitch) % 2;
            for (var i = 0; i < iter; i++)
            {
                var leger = Instantiate(Leger, rt).GetComponent<RectTransform>();
                var setx = _glyph.localPosition.x + _glyph.sizeDelta.x / 2;
                var sety = SS(i);
                if (mod != 0) sety += SS(0.5f);
                leger.localPosition = new(setx, sety);
                leger.sizeDelta = new(SS(1.648f), SS(0.16f));
            }
        }
    }
}