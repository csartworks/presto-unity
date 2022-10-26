using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace presto.unity
{
    public class Beam : Image
    {
        [SerializeField] private Vector3 _yDiff;
        private readonly List<Note> _notes = new();
        protected override void Awake()
        {
            base.Awake();
        }
        private void Draw()
        {
            Note startNote = _notes[0];
            Note endNote = _notes.Last();

            ShortenEndNoteToMean();
            Stem startStem = startNote.Stem;
            Stem endStem = endNote.Stem;
            Vector3 leftMost = startNote.Stem.Rt.TransformPoint(startStem.rect.xMin, startStem.rect.yMax, 0);
            Vector3 rightMost = endStem.Rt.TransformPoint(endStem.rect.xMax, endStem.rect.yMax, 0);
            float width = rightMost.x - leftMost.x;
            float yDiff = -(leftMost.y - rightMost.y);
            _yDiff = new Vector2(0, yDiff);

            transform.position = leftMost;
            SetBeamScale();
            foreach (var n in _notes)
            {
                ShortenStemToFitBeam(n.Stem.Rt);
            }
            // ShortenNoteToFitBeam(endStem);

            void ShortenEndNoteToMean()
            {
                var pitches = from p in _notes select p.Pitch;
                var av = (float)pitches.Average();
                var mean = GlyphBehaviour.SS((float)pitches.Average() / 2);
                var temp = endNote.Rt.localPosition.y;
                var moreShorten = mean - temp;
                endNote.Stem.Rt.sizeDelta -= new Vector2(0, moreShorten);
            }
            void SetBeamScale()
            {
                var thickness = GlyphBehaviour.SS(GlyphBehaviour.engv["beamThickness"]);
                GetComponent<RectTransform>().sizeDelta = new(width, thickness);
            }
            void ShortenStemToFitBeam(RectTransform stem)
            {
                Vector3 s = stem.TransformPoint(stem.rect.xMin, 0, 0);
                var b = s.x - leftMost.x;
                var a = GetComponent<RectTransform>().sizeDelta.x;
                var t = b / a;
                var lerp = Vector3.Lerp(leftMost, rightMost, t);
                var inv = stem.InverseTransformPoint(lerp);
                print(inv.y);
                // float l = rightMost.x - s.x;
                // float shorten = l * yDiff / width;
                // print(shorten);
                stem.sizeDelta = new(stem.sizeDelta.x, inv.y);
                // stem.sizeDelta -= new Vector2(0, shorten);
            }
        }
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            base.OnPopulateMesh(vh);
            var v = new UIVertex();
            for (int i = 2; i < 4; i++)
            {
                vh.PopulateUIVertex(ref v, i);
                v.position += _yDiff;
                vh.SetUIVertex(v, i);
            }
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
            Draw();
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