using presto.parser;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace presto.unity
{
    public class Staff : GlyphBehaviour
    {
        public float THIN_BARLINE_THICKNESS;
        public float STAFF_LINE_THICKNESS;

        [SerializeField] private int staffLength = 100;

        private readonly List<RectTransform> rts = new();
        private readonly RectTransform[] lines = new RectTransform[5];

        private void Awake()
        {
            THIN_BARLINE_THICKNESS = engv["thinBarlineThickness"];
            STAFF_LINE_THICKNESS = engv["staffLineThickness"];
            GlobalFontSize = 100;
            GetComponent<RectTransform>().sizeDelta = new(staffLength, GlobalFontSize);
            DrawStaff5Line();
            DrawGlyph(Main.GlyphNames["gClef"].Codepoint, SS(0.5f));
            DrawNote("8", 1);
            DrawNote("8", 1);
        }
        public void DrawStaff5Line()
        {
            for (int i = 0; i < 5; i++)
            {
                RectTransform tr = Instantiate(Staff1Line, transform).GetComponent<RectTransform>();
                tr.localPosition = new Vector2(0, i * 0.25f * 100);
                tr.sizeDelta = new(0, SS(STAFF_LINE_THICKNESS));
                lines[i] = tr;
            }
        }
        public void UpdateStaffLength()
        {
            var x = rts.Last().localPosition.x;
            foreach (var l in lines)
            {
                l.sizeDelta = new(x, l.sizeDelta.y);
            }
        }
        public void DrawBarline()
        {
            RectTransform bar = Instantiate(BarlineSingle, transform).GetComponent<RectTransform>();
            bar.localPosition = new Vector2(staffLength, 0);
            bar.sizeDelta = new(SS(THIN_BARLINE_THICKNESS), EM(1));
        }
        public void DrawNote(string len, int pitch)
        {
            var n = Instantiate(Note, transform);
            n.GetComponent<Note>().Init(this, len, pitch);
        }
        public RectTransform DrawGlyph(char glyph, float y = 0)
        {
            RectTransform rt = Instantiate(Glyph, transform).GetComponent<RectTransform>();
            rt.GetComponent<Glyph>().SetGlyph(glyph);
            AppendToRts(rt, y);
            return rt;
        }
        public void AppendToRts(RectTransform rt, float y = 0)
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
            UpdateStaffLength();
        }
    }
}