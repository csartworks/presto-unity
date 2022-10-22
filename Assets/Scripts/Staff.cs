using presto.parser;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace presto.unity
{
    public class Staff : MonoBehaviour
    {
        public float THIN_BARLINE_THICKNESS;
        public float STAFF_LINE_THICKNESS;
        public float EM(float v) => FontSize * v;
        public float SS(float v) => FontSize * 0.25f * v;
        private Dictionary<string, float> engv;
        private Dictionary<string, GlyphName> glyphs;

        [SerializeField] private GameObject _staff1Line;
        [SerializeField] private GameObject _leger;
        [SerializeField] private GameObject _barlineSingle;
        [SerializeField] private GameObject _glyph;
        [SerializeField] private GameObject _note;
        [SerializeField] private int staffLength = 100;
        [SerializeField] public float FontSize { get; private set; } = 100;

        private List<RectTransform> rts = new();

        private void Awake()
        {
            engv = Main.metaData.engravingDefaults;
            glyphs = Main.GlyphNames;
            THIN_BARLINE_THICKNESS = engv["thinBarlineThickness"];
            STAFF_LINE_THICKNESS = engv["staffLineThickness"];
            FontSize = 100;
            GetComponent<RectTransform>().sizeDelta = new(staffLength, FontSize);
            DrawStaff5Line();
            DrawBarline();
            DrawGlyph(Main.GlyphNames["gClef"].Codepoint, SS(0.5f));
            DrawGlyph(Main.GlyphNames["accidentalSharp"].Codepoint);
            DrawNote("16", -2);
            DrawNote("16", -3);
            DrawNote("16", -4);
        }
        public void DrawStaff5Line()
        {
            for (int i = 0; i < 5; i++)
            {
                RectTransform tr = Instantiate(_staff1Line, transform).GetComponent<RectTransform>();
                tr.localPosition = new Vector2(0, i * 0.25f * 100);
                tr.sizeDelta = new(0, SS(STAFF_LINE_THICKNESS));
            }
        }
        public void DrawBarline()
        {
            RectTransform bar = Instantiate(_barlineSingle, transform).GetComponent<RectTransform>();
            bar.localPosition = new Vector2(staffLength, 0);
            bar.sizeDelta = new(SS(THIN_BARLINE_THICKNESS), EM(1));
        }
        public void DrawNote(string len, int pitch)
        {

            RectTransform noteRt = Instantiate(_note, transform).GetComponent<RectTransform>();
            var y = SS(pitch * 0.5f);

            if (pitch < 0)
            {
                int iter = Mathf.Abs(pitch) / 2;
                for (var i = 0; i < iter; i++)
                {
                    var leger = Instantiate(_leger, noteRt).GetComponent<RectTransform>();
                    leger.anchoredPosition = new(0, SS(i));
                    leger.sizeDelta = new(SS(1.648f), SS(0.16f));
                }
            }

            RectTransform stem = noteRt.Find("stem").GetComponent<RectTransform>();
            float stemH = SS(3.5f) - SS(0.168f);
            stem.sizeDelta = new(SS(engv["stemThickness"]), stemH);
            stem.localPosition = new(SS(1.18f), SS(0.168f));

            RectTransform flag = stem.Find("flag").GetComponent<RectTransform>();
            flag.GetComponent<TMP_Text>().text = glyphs[$"flag{len}thUp"].Codepoint.ToString();
            flag.anchoredPosition = new(0, SS(-0.088f));
            AddToRts(noteRt, y);
        }
        public void DrawStem()
        {
            RectTransform bar = Instantiate(_barlineSingle, transform).GetComponent<RectTransform>();
            bar.sizeDelta = new(SS(engv["stemThickness"]), EM(1));
            AddToRts(bar);
        }
        public RectTransform DrawGlyph(char glyph, float y = 0)
        {
            RectTransform rt = Instantiate(_glyph, transform).GetComponent<RectTransform>();
            rt.GetComponent<Glyph>().Init(glyph);
            AddToRts(rt, y);
            return rt;
        }
        private void AddToRts(RectTransform rt, float y = 0)
        {
            float x = 0;
            int i = rts.Count - 1;
            if (i >= 0)
            {
                RectTransform prt = rts[i];
                x = prt.rect.size.x + prt.localPosition.x;
            }
            rt.localPosition = new(x, y);
            rts.Add(rt);
            Canvas.ForceUpdateCanvases();
        }
    }
}