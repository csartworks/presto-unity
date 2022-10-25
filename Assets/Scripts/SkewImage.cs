using UnityEngine.UI;
using UnityEngine;

public class SkewImage : Image
{
    [SerializeField] private float _skewX;
    [SerializeField] private float _skewY;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        base.OnPopulateMesh(vh);

        var height = rectTransform.rect.height;
        var width = rectTransform.rect.width;
        var xskew = height * Mathf.Tan(Mathf.Deg2Rad * _skewX);
        var yskew = width * Mathf.Tan(Mathf.Deg2Rad * _skewY);

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