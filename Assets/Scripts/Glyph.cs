using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Glyph : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    private RectTransform _rectTransform => GetComponentsInChildren<RectTransform>()[2];
    public float X { get => transform.localPosition.x; set => transform.localPosition = new Vector2(value, transform.localPosition.x); }
    public float Width => _rectTransform.sizeDelta.x;
    private void Awake()
    {
        //_rectTransform = GetComponent<RectTransform>();
    }
    public void Init(char glyph)
    {
        _text.text = glyph.ToString();
    }
}
