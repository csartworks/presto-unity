using TMPro;
using UnityEngine;

namespace presto.unity
{
    public class Note : GlyphBehaviour
    {
        [SerializeField] private Stem _stem;
        [SerializeField] private RectTransform _flag;
        [SerializeField] private RectTransform _glyph;
        [SerializeField] private TMP_Text _glyphText;
        public Stem Stem => _stem;
        public RectTransform Flag => _flag;
        public RectTransform Rt { get; set; }
        public int Pitch { get; private set; }
        private int len = 2;
        public int Len
        {
            get => len; set
            {
                len = value;
                if (NoteType == NoteType.Rest) DrawRest();
                else if (NoteType == NoteType.Note) DrawNote();
            }
        }
        public Beam Beam { get; set; }
        public NoteType NoteType { get; private set; }
        public void Init(Staff staff, int pitch, NoteType type = NoteType.Note)
        {
            Rt = GetComponent<RectTransform>();
            Pitch = pitch;
            NoteType = type;
            Len = len;
            if (type == NoteType.Note)
            {
                DrawLeger();
            }
            if (type == NoteType.XNote)
            {
                gameObject.SetActive(false);
            }

            var y = SS(pitch / 2f);
            staff.AppendToRts(Rt, y);
        }
        public void Init(Staff staff, int len, int pitch, NoteType type = NoteType.Note)
        {
            Init(staff, pitch, type);
            Len = len;
        }
        public void DrawRest()
        {
            _flag.gameObject.SetActive(false);
            _stem.gameObject.SetActive(false);
            _glyphText.text = GetRest(Len);
        }
        public void DrawNote()
        {
            DrawNoteHead(Len);
            DrawFlag(Len);
        }
        public void DrawNoteHead(int index)
        {
            switch (index)
            {
                case 0:
                case 1:
                case 2:
                    _flag.gameObject.SetActive(true);
                    _glyphText.text = GetNoteHead(index);
                    break;
                default:
                    _flag.gameObject.SetActive(false);
                    break;
            }
        }
        public void DrawFlag(int len)
        {
            if (len <= 2)
            {
                _flag.gameObject.SetActive(false);
                return;
            }
            if (Beam is not null) return;
            string flagText = GetFlag(len);
            _flag.GetComponent<TMP_Text>().text = flagText;
            _flag.anchoredPosition = new(0, SS(-0.088f));
            _flag.gameObject.SetActive(true);
        }
        public void DrawLeger()
        {
            if (Pitch > 2) return;
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
    public enum NoteType
    {
        Note, Rest, XNote
    }
}