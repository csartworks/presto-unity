using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace presto.unity
{
    public class Beam : GlyphBehaviour
    {
        [SerializeField] private GameObject _beamGraphic;
        private RectTransform _rt;
        private readonly List<Note> _notes = new();
        private readonly List<RectTransform> _beamRts = new();
        private int _beamsCount;
        private void Awake()
        {
            _rt = GetComponent<RectTransform>();
        }
        public void Draw()
        {
            int beam_c = _beamRts.Count;
            if (beam_c > _beamsCount)
            {
                while (_beamRts.Count != _beamsCount)
                {
                    RectTransform last = _beamRts.Last();
                    Destroy(last.gameObject);
                    _beamRts.Remove(last);
                }
            }
            else if (beam_c < _beamsCount)
            {
                while (_beamRts.Count != _beamsCount)
                {
                    AddBeamLine();
                }
            }
            Note startNote = _notes[0];
            Note endNote = _notes.Last();

            ShortenEndNoteToMean();
            Stem startStem = startNote.Stem;
            Stem endStem = endNote.Stem;
            Vector3 leftMost = startNote.Stem.Rt.TransformPoint(startStem.Rect.xMin, startStem.Rect.yMax, 0);
            Vector3 rightMost = endStem.Rt.TransformPoint(endStem.Rect.xMax, endStem.Rect.yMax, 0);
            float width = rightMost.x - leftMost.x;
            float yDiff = -(leftMost.y - rightMost.y);
            var dy = new Vector2(0, yDiff);
            transform.position = leftMost;
            SetBeamScale(_rt);
            for (int i = 0; i < _beamRts.Count; i++)
            {
                RectTransform r = _beamRts[i];
                r.GetComponent<Skew>().YDiff = dy;
                SetBeamScale(r);
                r.position = leftMost;
                r.Translate(i * SS(engv["beamThickness"] + engv["beamSpacing"]) * Vector3.down);
            }
            foreach (var n in _notes)
            {
                ShortenStemToFitBeam(n.Stem.Rt);
            }

            void ShortenEndNoteToMean()
            {
                var pitches = from p in _notes select p.Pitch;
                var av = (float)pitches.Average();
                var mean = GlyphBehaviour.SS((float)pitches.Average() / 2);
                var temp = endNote.Rt.anchoredPosition.y;
                var moreShorten = mean - temp;
                endNote.Stem.Rt.sizeDelta -= new Vector2(0, moreShorten);
            }
            void SetBeamScale(RectTransform rt)
            {
                var thickness = GlyphBehaviour.SS(GlyphBehaviour.engv["beamThickness"]);
                rt.sizeDelta = new(width, thickness);
            }
            void ShortenStemToFitBeam(RectTransform stem)
            {
                Vector3 s = stem.TransformPoint(stem.rect.xMin, 0, 0);
                var b = s.x - leftMost.x;
                var a = GetComponent<RectTransform>().sizeDelta.x;
                var t = b / a;
                var lerp = Vector3.Lerp(leftMost, rightMost, t);
                var inv = stem.InverseTransformPoint(lerp);
                stem.sizeDelta = new(stem.sizeDelta.x, inv.y);
            }
        }
        public void SetLength(int len)
        {
            _beamsCount = len - 2;
            Draw();
        }
        public void Add(params Note[] notes)
        {
            if (notes is null) return;
            _notes.AddRange(notes);
            foreach (var n in notes)
            {
                n.Beam = this;
                n.Flag.gameObject.SetActive(false);
            }
        }
        public void RemoveLast()
        {
            _notes.Remove(_notes.Last());
            if(_notes.Count == 1)
            {
                Destroy(gameObject);
                return;
            }
            // Draw();
        }
        public void AddBeamLine()
        {
            var rt = Instantiate(_beamGraphic, transform).GetComponent<RectTransform>();
            _beamRts.Add(rt);
        }
    }
    public class BeamCreator
    {
        private readonly Beam _beam;
        public BeamCreator(Transform staff, params Note[] notes)
        {
            _beam = GameObject.Instantiate(GlyphBehaviour.BeamPrefab, staff).GetComponent<Beam>();
            if (notes is null) return;
            Add(notes);
        }
        public void Add(params Note[] notes)
        {
            if (notes is null) return;
            _beam.Add(notes);
        }
    }
}