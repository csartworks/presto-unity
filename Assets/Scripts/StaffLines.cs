using UnityEngine;
namespace presto.unity
{
    public class StaffLines : GlyphBehaviour
    {
        public static float STAFF_LINE_THICKNESS;
        private int _lines = 5;
        private RectTransform[] _staffLines;
        private void Awake()
        {
            STAFF_LINE_THICKNESS = engv["staffLineThickness"];
            Draw();
        }
        private void Draw()
        {
            _lines = 5;
            print(_lines);
            _staffLines = new RectTransform[_lines];
            for (int i = 0; i < _lines; i++)
            {
                RectTransform tr = Instantiate(Staff1LinePrefab, transform).GetComponent<RectTransform>();
                tr.anchoredPosition = new(0, i * 0.25f * 100);
                tr.sizeDelta = new(0, SS(STAFF_LINE_THICKNESS));
                print(i);
                _staffLines[i] = tr;
            }
        }
        public void SetLength(float len)
        {
            foreach (var l in _staffLines)
            {
                // l.sizeDelta = new(len, l.sizeDelta.y);
            }
        }
        public class StaffLinesCreator
        {
            // public static StaffLines Create(Transform staff)
            // {
            //     StaffLines sl = new GameObject("staffLines", typeof(RectTransform)).AddComponent<StaffLines>();
            //     sl.transform.SetParent(staff);
            //     sl.transform.localPosition = Vector3.zero;
            //     sl._lines = 5;
            //     return sl;
            // }
        }
    }
}