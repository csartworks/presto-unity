using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace presto.unity
{
    public class Skew : Image
    {
        [SerializeField] public Vector3 YDiff { get; set; }
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            base.OnPopulateMesh(vh);
            var v = new UIVertex();
            for (int i = 2; i < 4; i++)
            {
                vh.PopulateUIVertex(ref v, i);
                v.position += YDiff;
                vh.SetUIVertex(v, i);
            }
        }
    }
}