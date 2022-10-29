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
        private Note _lastNote;
        private readonly List<RectTransform> _glyphs = new();
        private readonly List<Note> _noteGroup = new();
        private StaffLines _staffLines;
        private RectTransform _rt;

        private void Awake()
        {
            THIN_BARLINE_THICKNESS = engv["thinBarlineThickness"];
            _rt = GetComponent<RectTransform>();
            _rt.sizeDelta = new(0, GlobalFontSize);
            _staffLines = GetComponentInChildren<StaffLines>();
            _staffLines.Init(5);
            // DrawGlyph(Main.GlyphNames["gClef"].Codepoint, SS(0.5f));
        }
        public void DrawBarline()
        {
            RectTransform bar = Instantiate(BarlineSinglePrefab, transform).GetComponent<RectTransform>();
            bar.localPosition = new Vector2(0, 0);
            bar.sizeDelta = new(SS(THIN_BARLINE_THICKNESS), EM(1));
        }
        public Note DrawNote(string len, int pitch)
        {
            var n = Instantiate(NotePrefab, transform).GetComponent<Note>();
            n.Init(this, len, pitch);
            _noteGroup.Add(n);
            int tempLen = 4;
            if (_noteGroup.Count == 2) tempLen = 8;
            if (_noteGroup.Count == 4) tempLen = 16;
            if (_noteGroup.Count == 8) tempLen = 32;
            foreach (var note in _noteGroup)
            {
                note.SetLength(tempLen);
            }
            if (_lastNote is not null)
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
        public void FinishNoteGroup()
        {
            _lastNote = null;
            _noteGroup.Clear();
        }
        public void DrawRest(int len = 0x00)
        {
            int l = 0xE4E5;
            l += len;
            var n = Instantiate(GlyphPrefab, transform).GetComponent<Glyph>();
            n.SetGlyph((char)l);
            AppendToRts(n.GetComponent<RectTransform>(), SS(1.5f));
        }
        public void Delete()
        {
            var last = _glyphs.Last();
            Destroy(last.gameObject);
            _glyphs.Remove(last);
            if (_glyphs.Last().TryGetComponent<Note>(out Note n))
            {
                // n.Beam?.Draw();
            }
        }
        public RectTransform DrawGlyph(char glyph, float y = 0)
        {
            RectTransform rt = Instantiate(GlyphPrefab, transform).GetComponent<RectTransform>();
            rt.GetComponent<Glyph>().SetGlyph(glyph);
            AppendToRts(rt, y);
            return rt;
        }
        public void AppendToRts(RectTransform v, float y = 0)
        {
            float x = 0;
            int i = _glyphs.Count - 1;
            if (i >= 0)
            {
                RectTransform prt = _glyphs[i];
                x = prt.rect.size.x + prt.localPosition.x;
            }
            v.anchoredPosition = new(x, y);
            _glyphs.Add(v);
            Canvas.ForceUpdateCanvases();

            var newLen = v.anchoredPosition.x + v.sizeDelta.x;
            _rt.sizeDelta = new(newLen, _rt.sizeDelta.y);
        }
    }
}