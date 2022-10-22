using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Dictionary<string, GlyphName> GlyphNames { get; private set; } = new();

    private void Awake()
    {
        var json = Resources.Load<TextAsset>("raw/glyphnames");
        GlyphNames = JsonParser.Parse(json.text);
    }
}

