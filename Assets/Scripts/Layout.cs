using NUnit.Framework;
using UnityEngine;

public class Layout : MonoBehaviour
{
    private void OnTransformChildrenChanged()
    {
        float nextChildX = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            var pos = child.position;
            child.position = new Vector2(nextChildX, pos.y);
            nextChildX += child.GetChild(0).GetComponent<RectTransform>()?.sizeDelta.x ?? 0;
        }
    }
}
