using UnityEngine;
using UnityEngine.UI;

namespace presto.unity
{
    public class Beam : Image
    {
        [SerializeField] private Vector3 _yDiff;
        protected override void Awake()
        {
            base.Awake();
        }
        public void Init(Note startNote, Note endNote)
        {
            ShortenEndNoteToMean();
            RectTransform startStem = startNote.Stem;
            RectTransform endStem = endNote.Stem;
            Vector3 leftMost = startNote.Stem.TransformPoint(startStem.rect.xMin, startStem.rect.yMax, 0);
            Vector3 rightMost = endStem.TransformPoint(endStem.rect.xMax, endStem.rect.yMax, 0);
            float width = rightMost.x - leftMost.x;
            float yDiff = -(leftMost.y - rightMost.y);
            _yDiff = new Vector2(0, yDiff);

            transform.position = leftMost;
            SetBeamScale();
            ShortenNoteToFitBeam();

            void ShortenEndNoteToMean()
            {
                var mean = GlyphBehaviour.SS((startNote.Pitch + endNote.Pitch) / 2f);
                var temp = endNote.Rt.localPosition.y;
                var moreShorten = mean - temp;
                endNote.Stem.sizeDelta -= new Vector2(0, moreShorten);
            }
            void SetBeamScale()
            {
                var thickness = GlyphBehaviour.SS(GlyphBehaviour.engv["beamThickness"]);
                GetComponent<RectTransform>().sizeDelta = new(width, thickness);
            }
            void ShortenNoteToFitBeam()
            {
                float shorten = endNote.Stem.sizeDelta.x * yDiff / width;
                endNote.Stem.sizeDelta -= new Vector2(0, shorten);
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
    }
}