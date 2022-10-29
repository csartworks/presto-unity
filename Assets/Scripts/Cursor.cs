using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace presto.unity
{
    public class Cursor : MonoBehaviour
    {
        private const int NORMAL = 10;
        private const int INSERT = 5;
        private Image _image;
        private RectTransform _rt;
        private bool _mode;
        public float Interval = 0.5f;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _rt = GetComponent<RectTransform>();
            StartCoroutine(Blink());
        }
        private IEnumerator Blink()
        {
            while (true)
            {
                _image.enabled = false;
                yield return new WaitForSeconds(Interval);
                _image.enabled = true;
                yield return new WaitForSeconds(Interval);
                // yield return new WaitUntil(() => _mode);
            }
        }
        public void SetMode(bool v)
        {
            var w = v ? INSERT : NORMAL;
            _rt.sizeDelta = new(w, _rt.sizeDelta.y);
            _mode = v;
        }
    }
}