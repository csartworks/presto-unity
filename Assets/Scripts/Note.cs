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
        public int Len { get; private set; }
        public Beam Beam { get; set; }

        private bool _isRest;
        public void Init(Staff staff, int len, int pitch, bool isRest = false)
        {
            Rt = GetComponent<RectTransform>();
            Pitch = pitch;
            Len = len;
            _isRest = isRest;
            if (!isRest)
            {
                SetLength(Len);
            }

            var y = SS(pitch / 2f);
            staff.AppendToRts(Rt, y);
        }
        public void SetLength(int len)
        {
            if (_isRest) DrawRest();
            else
            {
                DrawNoteHead(len);
                if (Len >= 3) DrawFlag();
            }
        }
        public void DrawRest()
        {
            _flag.gameObject.SetActive(false);
            _stem.gameObject.SetActive(false);
            _glyphText.text = Rest(Len);
        }
        public void DrawNoteHead(int index)
        {
            switch (index)
            {
                case 0:
                case 1:
                case 2:
                    _flag.gameObject.SetActive(true);
                    _glyphText.text = NoteHead(index);
                    break;
                default:
                    _flag.gameObject.SetActive(false);
                    break;
            }
        }
        public void DrawFlag()
        {
            if (Beam is not null) return;
            string flagText = Flag(Len);
            _flag.GetComponent<TMP_Text>().text = flagText;
            _flag.anchoredPosition = new(0, SS(-0.088f));
            _flag.gameObject.SetActive(true);
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