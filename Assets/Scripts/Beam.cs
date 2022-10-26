using UnityEngine;
using UnityEngine.UI;

namespace presto.unity
{
    public class Beam : Image
    {
        [SerializeField] private Vector3 _goUp;
        protected override void Awake()
        {
            base.Awake();
        }
        public void Init(Note startNote, Note endNote)
        {
            var mean = GlyphBehaviour.SS((startNote.Pitch + endNote.Pitch) / 2f);
            var temp = endNote.Rt.localPosition.y;
            var moreShorten = mean - temp;
            endNote.Stem.sizeDelta -= new Vector2(0, moreShorten);

            var r1 = startNote.Stem.rect;
            var r2 = endNote.Stem.rect;
            var p1 = startNote.Stem.TransformPoint(r1.xMin, r1.yMax, 0);
            var p2 = endNote.Stem.TransformPoint(r2.xMax, r2.yMax, 0);
            transform.position = p1;
            var thickness = GlyphBehaviour.SS(GlyphBehaviour.engv["beamThickness"]);
            var dx = p2.x - p1.x;
            GetComponent<RectTransform>().sizeDelta = new(dx, thickness);
            var up = -(p1.y - p2.y);
            _goUp = new Vector2(0, up);
            float shorten = endNote.Stem.sizeDelta.x * up / dx;
            endNote.Stem.sizeDelta -= new Vector2(0, shorten);
        }
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            base.OnPopulateMesh(vh);
            var v = new UIVertex();
            vh.PopulateUIVertex(ref v, 2);
            v.position += _goUp;
            vh.SetUIVertex(v, 2);

            vh.PopulateUIVertex(ref v, 3);
            v.position += _goUp;
            vh.SetUIVertex(v, 3);
        }
    }
}