using UnityEngine;
namespace presto.unity
{
    [RequireComponent(typeof(RectTransform))]
    public class Stem : GlyphBehaviour
    {
        public static float stemH = SS(3.5f) - SS(0.168f);
        public RectTransform Rt { get; set; }
        public Rect Rect => Rt.rect;
        private void Awake()
        {
            Rt = GetComponent<RectTransform>();
            Rt.localPosition = new(SS(1.18f), SS(0.168f));
            ResetSize();
        }
        public void ResetSize()
        {
            Rt.sizeDelta = new(SS(engv["stemThickness"]), stemH);
        }
    }
}