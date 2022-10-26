using UnityEngine.UI;
using UnityEngine;

public class SkewImage : Image
{
    [SerializeField] private float skewY;
    [SerializeField] private float skewX;

    public float SkewX { get => skewX; set => skewX = value; }
    public float SkewY { get => skewY; set => skewY = value; }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        base.OnPopulateMesh(vh);

        var height = rectTransform.rect.height;
        var width = rectTransform.rect.width;
        var xskew = height * Mathf.Tan(Mathf.Deg2Rad * SkewX);
        var yskew = width * Mathf.Tan(Mathf.Deg2Rad * SkewY);

        var y = rectTransform.rect.yMin;
        var x = rectTransform.rect.xMin;
        UIVertex v = new UIVertex();
        for (int i = 0; i < vh.currentVertCount; i++)
        {
            vh.PopulateUIVertex(ref v, i);
            v.position += new Vector3(Mathf.Lerp(0, xskew, (v.position.y - y) / height), Mathf.Lerp(0, yskew, (v.position.x - x) / width), 0);
            vh.SetUIVertex(v, i);
        }

    }
}