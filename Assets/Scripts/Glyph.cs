using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace presto.unity
{
    public class Glyph : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        public void SetGlyph(char glyph)
        {
            _text.text = glyph.ToString();
        }
    }
}