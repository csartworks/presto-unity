using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Glyph : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    public void Init(char glyph)
    {
        _text.text = glyph.ToString();
    }
}
