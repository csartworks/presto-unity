using UnityEngine;
namespace presto.unity
{
    public class StaffLines : GlyphBehaviour
    {
        private float _thickness;
        private int _lines;
        private RectTransform[] _staffLines;
        public void Init(int lines = 5)
        {
            _thickness = SS(engv["staffLineThickness"]);
            _lines = lines;
            Draw();
        }
        private void Draw()
        {
            _staffLines = new RectTransform[_lines];
            float lineSpacing = GlobalFontSize / (_lines - 1);
            for (int i = 0; i < _lines; i++)
            {
                RectTransform tr = Instantiate(Staff1LinePrefab, transform).GetComponent<RectTransform>();
                tr.anchoredPosition = new(0, i * lineSpacing);
                tr.sizeDelta = new(0, _thickness);
                _staffLines[i] = tr;
            }
        }
    }
}