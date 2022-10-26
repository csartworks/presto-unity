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

        var h = rectTransform.rect.height;
        var w = rectTransform.rect.width;
        var y = rectTransform.rect.y;
        var x = rectTransform.rect.x;

        var xskew = h * Mathf.Tan(Mathf.Deg2Rad * SkewX);
        var yskew = w * Mathf.Tan(Mathf.Deg2Rad * SkewY);

        var v = new UIVertex();
        for (int i = 0; i < vh.currentVertCount; i++)
        {
            vh.PopulateUIVertex(ref v, i);
            var tx = (v.position.y - y) / h;
            var ty = (v.position.x - x) / w;
            var vx = Mathf.Lerp(0, xskew, tx);
            var vy = Mathf.Lerp(0, yskew, ty);
            v.position += new Vector3(vx, vy, 0);
            vh.SetUIVertex(v, i);
        }

    }
}