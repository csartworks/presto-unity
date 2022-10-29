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
        [SerializeField] private Cursor _cursor;
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
            DrawGlyph(Main.GlyphNames["gClef"].Codepoint, SS(0.5f));
        }
        public void DrawBarline()
        {
            RectTransform bar = Instantiate(BarlineSinglePrefab, transform).GetComponent<RectTransform>();
            float w = SS(THIN_BARLINE_THICKNESS);
            bar.Translate(w / 2 * Vector2.left);
            bar.sizeDelta = new(w, EM(1));
            AppendToRts(bar);
        }
        public Note DrawNote(int pitch, NoteType type = NoteType.Note)
        {
            _cursor.SetMode(true);
            var n = Instantiate(NotePrefab, transform).GetComponent<Note>();
            n.Init(this, pitch, type);
            _noteGroup.Add(n);
            int c = _noteGroup.Count;
            int len = GetLengthFromCount(c);
            if (type == NoteType.Note && _lastNote is not null)
            {
                if (_lastNote.Beam is null)
                {
                    new BeamCreator(transform, _lastNote, n);
                }
                else
                {
                    _lastNote.Beam.Add(n);
                }
                _lastNote.Beam.SetLength(len);
            }
            for (int i = 0; i < _noteGroup.Count; i++)
            {
                Note note = _noteGroup[i];
                if (note.NoteType == NoteType.XNote) continue;
                if (_noteGroup.ElementAtOrDefault(i + 1) is not null && _noteGroup[i + 1].NoteType == NoteType.XNote) len -= 1;
                note.Len = len;
            }
            if (type == NoteType.Note) _lastNote = n;
            return n;
        }
        public int GetLengthFromCount(int count)
        {
            int i = 1;
            int pow = Mathf.NextPowerOfTwo(count);
            bool ispow = Mathf.IsPowerOfTwo(count);
            while (pow > 0)
            {
                pow >>= 1;
                i++;
            }
            if (!ispow) i -= 1;
            return i;

        }
        public void FinishNoteGroup()
        {
            _cursor.SetMode(false);
            _lastNote = null;
            _noteGroup.Clear();
        }
        public void Delete()
        {
            var last = _glyphs.Last();
            _glyphs.Remove(last);
            if (last.TryGetComponent<Note>(out Note n))
            {
                n.Beam?.RemoveLast();
                // n.Beam?.SetLength(_noteGroup.Count);
                _noteGroup.Remove(n);
            }
            _lastNote = null;
            Destroy(last.gameObject);
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