using presto.parser;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

namespace presto.unity
{
    public class Main : MonoBehaviour
    {
        public static Dictionary<string, GlyphName> GlyphNames { get; private set; } = new();
        public static FontMetaData metaData;

        private void Awake()
        {
            var json = Resources.Load<TextAsset>("raw/glyphnames");
            GlyphNames = JsonParser.Parse(json.text);
            using StreamReader reader = new StreamReader("Assets/Fonts/bravura_metadata.json");
            metaData = MetaDataParser.Parse(reader.ReadToEnd());
            print(metaData.fontName);
        }
    }
}