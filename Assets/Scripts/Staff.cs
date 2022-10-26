using System.Collections.Generic;
using System.Linq;
using presto.parser;
using TMPro;
using UnityEngine;

namespace presto.unity
{
    public class Staff : GlyphBehaviour
    {
        public float THIN_BARLINE_THICKNESS;
        public float STAFF_LINE_THICKNESS;
        private const float BEAM_SKEW = 14;
        private const float STEM_ADAPT = 13;

        [SerializeField] private int staffLength = 100;

        private readonly List<RectTransform> rts = new();
        private readonly RectTransform[] lines = new RectTransform[5];
        private Note _lastNote;

        private void Awake()
        {
            THIN_BARLINE_THICKNESS = engv["thinBarlineThickness"];
            STAFF_LINE_THICKNESS = engv["staffLineThickness"];
            GlobalFontSize = 100;
            GetComponent<RectTransform>().sizeDelta = new(staffLength, GlobalFontSize);
            DrawStaff5Line();
            DrawGlyph(Main.GlyphNames["gClef"].Codepoint, SS(0.5f));
            // DrawNote("8", 1);
            // DrawNote("8", 1);
            // DrawBeamGroup(0, 0);
        }
        public void DrawStaff5Line()
        {
            for (int i = 0; i < 5; i++)
            {
                RectTransform tr = Instantiate(Staff1LinePrefab, transform).GetComponent<RectTransform>();
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
            RectTransform bar = Instantiate(BarlineSinglePrefab, transform).GetComponent<RectTransform>();
            bar.localPosition = new Vector2(staffLength, 0);
            bar.sizeDelta = new(SS(THIN_BARLINE_THICKNESS), EM(1));
        }
        private RectTransform CreateBox(Vector3 localPos, Vector2 size)
        {
            RectTransform bar = Instantiate(Staff1LinePrefab, transform).GetComponent<RectTransform>();
            bar.localPosition = localPos;
            bar.sizeDelta = size;
            return bar;
        }
        public Note DrawNote(string len, int pitch, Transform parent = null)
        {
            if (parent is null) parent = transform;
            var n = Instantiate(NotePrefab, parent).GetComponent<Note>();
            n.Init(this, len, pitch);
            if (n.Len <= 8 && _lastNote?.Len <= 8)
            {
                if (_lastNote.Beam is null)
                {
                    new BeamCreator(transform, _lastNote, n);
                }
                else
                {
                    _lastNote.Beam.Add(n);
                }
            }
            _lastNote = n;
            return n;
        }
        public void DrawBeamGroup(int pitch1, int pitch2)
        {
            var n1 = DrawNote("8", pitch1);
            var n2 = DrawNote("8", pitch2);
            // var n3 = DrawNote("4", p3);
            Canvas.ForceUpdateCanvases();
            // n2.Stem.sizeDelta -= new Vector2(0, BEAM_SKEW / 2f/*STEM_ADAPT * mean*/);
            // DrawBeam();
            BeamCreator beams = new(transform, n1, n2);
            // void DrawBeam()
            // {
            //     var beam = Instantiate(Beam, transform).GetComponent<RectTransform>();
            //     beam.GetComponent<Beam>().Init(n1, n2, n3);
            // }
        }
        public RectTransform DrawGlyph(char glyph, float y = 0)
        {
            RectTransform rt = Instantiate(GlyphPrefab, transform).GetComponent<RectTransform>();
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